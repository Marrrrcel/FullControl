using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TBotCore;
using System.Drawing;
using nSpotify;
using System.Windows.Forms;
using TBotCore.Database;
using TBotCore.Log;
using System.Data;

namespace TBot
{
    public class Twitch
    {

        
        private string Lastsong = "";
        private TBot spotiBoti;
        private IrcInfo ircInfo;
        private DateTime TimeOfBotStarted = DateTime.Now;
        public IrcClient ircClient;
        public Thread twitchThread, spotifyThread;
        public DB _dataBase;

        //Constructor
        public Twitch(TBot _spotiBoti, IrcInfo _ircInfo, DB _dataBase, bool SpotifyEnabled)
        {
            this.spotiBoti = _spotiBoti;
            this.ircInfo = _ircInfo;
            this._dataBase = _dataBase;
            Initialize(SpotifyEnabled);
        }

        #region Private methods
        //Initialize
        private void Initialize(bool SpotifyEnabled)
        {
            this.ircClient = new IrcClient(ircInfo);
            this.twitchThread = new Thread(RunTwitch);
            this.twitchThread.Start();
            if (SpotifyEnabled)
            {
                this.spotifyThread = new Thread(RunSpotify);
                this.spotifyThread.Start();
            }
        }

        //Main Twitch method
        private void RunTwitch()
        {
            ircClient.IrcConnect();

            try
            {
                while (ircClient.IrcIsConnected())
                {
                    string message = ircClient.IrcreadChatMessage();
                    string username = "";
                    string messageonly = "";
                    if (message.Contains("PRIVMSG"))
                    {
                        username = getUsername(message);
                        messageonly = getChatline(message);
                    }
                    Logging.Log(message, Logging.Loglevel.Info);
                    spotiBoti.LogToConnect(message);
                    if (!String.IsNullOrEmpty(username))
                    {
                        spotiBoti.LogToChat(username, messageonly);
                    }
                    ProcessMessage(username, messageonly);
                }
            } catch(Exception ex) {
                if(!ex.Message.ToLower().Contains("thread")) {
                    TBotCore.Log.Logging.Log(ex.Message, TBotCore.Log.Logging.Loglevel.Warning);
                }
            }
        }

        //Return Username
        private string getUsername(string Message)
        {
            return Message.Substring(1, Message.IndexOf('!') - 1);
        }

        //Return first word of chatline
        private string getCommand(string Message) {
            string temp = getChatline(Message);
            return temp.Substring(temp.IndexOf(':') + 1).Split(' ')[0];
        }

        //Return chatline only
        private string getChatline(string Message)
        {
            string temp = Message.Substring(Message.IndexOf(':') + 1);
            return temp.Substring(temp.IndexOf(':') + 1);
        }

        //Detect commands in chat messages
        private void ProcessMessage(string username, string Chatline) {
            if (Chatline.StartsWith("!"))
            {
                string command = getCommand(Chatline.ToLower());
                if (command.Contains("!help"))
                {
                    spotiBoti.LogToCommand(username, command);
                    ircClient.IrcSendChatMessage("You can use following Commands. Each Command starts with !");
                    spotiBoti.LogToChat("BOT", "You can use following Commands. Each Command starts with !");
                Thread.Sleep(2000);

                    foreach (var msg in ReturnCustomCommand())
                    {
                        ircClient.IrcSendChatMessage(msg[1].ToString());
                        Logging.Log(msg[1].ToString(), Logging.Loglevel.Info);
                        spotiBoti.LogToChat("BOT", msg[1].ToString());
                    Thread.Sleep(1000);
                }
            }

            //TODO: Create Songrequestmethod etc.
                if (command.Contains("!songrequest"))
                {
                    spotiBoti.LogToCommand(username, command);
                    ircClient.IrcSendChatMessage("/w mrrrrcl test");
                spotiBoti.UpdateFormText("SpotiBoti - NEW SONGREQUEST!!!");
            }

                foreach (var msg in ReturnCustomCommand())
                {
                    if (command.Contains(msg[1].ToString()))
                    {
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
                }
            }
        }
        }

        //Returns DataRow[] of CurstomCommands
        //TODO: get these out of datbase
        private DataRow[] ReturnCustomCommand() {
            DataTable dt = _dataBase.getCustomCommandTable();
            DataRow[] result = dt.Select("enable = 1");
            return result;
        }

        //Method for Spotify Songchange detection
        private void RunSpotify()
        {
            Thread.Sleep(2000);
            eventProvider = new EventProvider();
            eventProvider.EventSynchronizingObject = spotiBoti;
            eventProvider.TrackChanged += eventProvider_TrackChanged;
            eventProvider.Start();
        }

        //Event for Spotify Songchange detection
        protected EventProvider eventProvider;
        private void eventProvider_TrackChanged(object sender, TrackChangedEventArgs e)
        {
            string song = "Now playing: " + e.CurrentTrack.Name + " by " + e.CurrentTrack.Artist;
            Lastsong = e.LastTrack.Name + " by " + e.LastTrack.Artist;
            ircClient.IrcSendChatMessage(song);
            //TODO: Add LogToLog (need to check how whole irc-message looks like)
            spotiBoti.LogToChat("BOT", song);
        }
        #endregion
    }
}
