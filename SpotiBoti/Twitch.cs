using System;
using System.Threading;
using TBotCore;
using nSpotify;
using TBotCore.Database;
using TBotCore.Log;
using System.Data;
using System.Drawing;

namespace TBot {
    public class Twitch {


        private string Lastsong = "";
        private TBot spotiBoti;
        private IrcInfo ircInfo;
        private DateTime TimeOfBotStarted = DateTime.Now;
        public IrcClient ircClient;
        public Thread twitchThread, spotifyThread;
        public DB _dataBase;

        //Constructor
        public Twitch(TBot _spotiBoti, IrcInfo _ircInfo, DB _dataBase, bool SpotifyEnabled) { // //
            this.spotiBoti = _spotiBoti;
            this.ircInfo = _ircInfo;
            this._dataBase = _dataBase;
            Initialize(SpotifyEnabled);
        }

        #region Private methods
        //Initialize
        private void Initialize(bool SpotifyEnabled) {
            this.ircClient = new IrcClient(ircInfo);
            this.twitchThread = new Thread(RunTwitch);
            this.twitchThread.Start();
            if(SpotifyEnabled) {
                this.spotifyThread = new Thread(RunSpotify);
                this.spotifyThread.Start();
            }
        }

        //Main Twitch method
        private void RunTwitch() {
            ircClient.IrcConnect();

            try {
                while(ircClient.IrcIsConnected()) {
                    string message = ircClient.IrcreadChatMessage();
                    string username = "";
                    string messageonly = "";
                    
                    if(message.Contains("PRIVMSG")) {
                        username = getUsername(message);
                        messageonly = getChatline(message);
                    }
                    Logging.Log(message, Logging.Loglevel.Info);
                    spotiBoti.LogToConnect(message);
                    if(!String.IsNullOrEmpty(username)) {
                        spotiBoti.LogToChat(username, messageonly);
                    }
                    string command = getCommand(messageonly.ToLower());
                    if(messageonly.StartsWith("!")) {
                        if(command.Contains("!help")) {
                            ProcessHelpCommand(username);
                        } else if(command.Contains("!songrequest")) {
                            ProcessSongrequest(messageonly, username);
                            spotiBoti.LogToCommand(username, command);
                            spotiBoti.UpdateFormText("SpotiBoti - NEW SONGREQUEST!!!");
                        } else {
                            ProcessMessage(ReturnCustomCommand(), command, username);
                            ProcessMessage(ReturnGenericCommand(), command, username);
                        }
                    }
                }
            } catch(Exception ex) {
                if(!ex.Message.ToLower().Contains("thread")) {
                    TBotCore.Log.Logging.Log(ex.Message, TBotCore.Log.Logging.Loglevel.Error);
                }
            }
        }

        //Return Username
        private string getUsername(string Message) {
            return Message.Substring(1, Message.IndexOf('!') - 1);
        }

        //Return first word of chatline
        private string getCommand(string Message) {
            string chatline = getChatline(Message);
            string temp = chatline;
            temp = temp.Substring(temp.IndexOf(':') + 1).Split(' ')[0];
            if(String.IsNullOrEmpty(temp)) {
               temp = temp.Substring(temp.IndexOf(':') + 1, chatline.Length);
            }
            return temp;
        }

        //Return chatline only
        private string getChatline(string Message) {
            string temp = Message.Substring(Message.IndexOf(':') + 1);
            return temp.Substring(temp.IndexOf(':') + 1);
        }

        //Process songrequest
        private void ProcessSongrequest(string chatline, string username) {
            if(chatline.Length > 12) {
                string temp = chatline.Substring(13);
                if(!String.IsNullOrEmpty(temp)) {
                    
                    spotiBoti.txtSongrequest.Items.Add(new SongrequestListBoxItem($"{username}: {temp}"));
                    //spotiBoti.txtSongrequest.Items.Add($"{username}: {temp}");
                }
            }
            //Songrequest.SearchYoutube(spotiBoti.txtSongrequest, "Actual Request");
        }

        //Detect commands in chat messages
        private void ProcessMessage(DataRow[] dataRow, string command, string username) {
            foreach(var msg in dataRow) {
                if(command.Contains(msg[1].ToString())) {
                    spotiBoti.LogToCommand(username, command);
                    Status status = Spotify.DataProviderInstance.UpdateStatus();
                    TimeSpan Uptime = DateTime.Parse(DateTime.Now.ToLongTimeString()).Subtract(TimeOfBotStarted);
                    string result = msg[2].ToString()
                    .Replace("$artist", status.Track.Artist)
                    .Replace("$track", status.Track.Name)
                    .Replace("$album", status.Track.Album)
                    .Replace("$time", DateTime.Now.ToShortTimeString())
                    .Replace("$uptime", Uptime.ToString());
                    ircClient.IrcSendChatMessage(result);
                    Thread.Sleep(2000);
                    Logging.Log(result, Logging.Loglevel.Info);
                    spotiBoti.LogToChat("BOT", result);
                    return;
                }
            }
        }

        //Processes !help command
        private void ProcessHelpCommand(string username) {
            spotiBoti.LogToCommand(username, "!help");
            ircClient.IrcSendChatMessage("You can use following Commands. Each Command starts with !");
            spotiBoti.LogToChat("BOT", "You can use following Commands. Each Command starts with !");
            Thread.Sleep(2000);

            foreach(var msg in ReturnGenericCommand()) {
                ircClient.IrcSendChatMessage(msg[1].ToString());
                Logging.Log(msg[1].ToString(), Logging.Loglevel.Info);
                spotiBoti.LogToChat("BOT", msg[1].ToString());
                Thread.Sleep(1000);
            }
            foreach(var msg in ReturnCustomCommand()) {
                ircClient.IrcSendChatMessage(msg[1].ToString());
                Logging.Log(msg[1].ToString(), Logging.Loglevel.Info);
                spotiBoti.LogToChat("BOT", msg[1].ToString());
                Thread.Sleep(1000);
            }
            return;
        }

        //Returns DataRow[] of Curstomcommands out of DB
        private DataRow[] ReturnCustomCommand() {
            DataTable dt = _dataBase.getCustomCommandTable();
            DataRow[] result = dt.Select("enable = 1");
            return result;
        }

        //Returns DataRow[] of Genericcommands out of DB
        private DataRow[] ReturnGenericCommand() {
            DataTable dt = _dataBase.getGenericCommandTable();
            DataRow[] result = dt.Select("enable = 1");
            return result;
        }

        //Method for Spotify Songchange detection
        private void RunSpotify() {
            Thread.Sleep(2000);
            eventProvider = new EventProvider();
            eventProvider.EventSynchronizingObject = spotiBoti;
            eventProvider.TrackChanged += eventProvider_TrackChanged;
            eventProvider.Start();
        }

        //Event for Spotify Songchange detection
        protected EventProvider eventProvider;
        private void eventProvider_TrackChanged(object sender, TrackChangedEventArgs e) {
            string song = "Now playing: " + e.CurrentTrack.Name + " by " + e.CurrentTrack.Artist;
            Lastsong = e.LastTrack.Name + " by " + e.LastTrack.Artist;
            ircClient.IrcSendChatMessage(song);
            //TODO: Add LogToLog (need to check how whole irc-message looks like)
            spotiBoti.LogToChat("BOT", song);
        }
        #endregion
    }
}