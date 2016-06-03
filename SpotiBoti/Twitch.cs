﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SpotiBotiCore;
using System.Drawing;
using nSpotify;
using System.Windows.Forms;
using SpotiBotiCore.Database;

namespace SpotiBoti {
    public class Twitch {

        public  bool enableLog = false;
        private const string LogPath = @"twitch.log";
        private string Lastsong = "";
        
        private SpotiBoti spotiBoti;
        private IrcInfo ircInfo;
        private DateTime TimeOfBotStarted = DateTime.Now;
        public IrcClient ircClient;
        public Thread twitchThread, spotifyThread;
        public CmdDatabase commands;

        //Constructor
        public Twitch(SpotiBoti _spotiBoti, IrcInfo _ircInfo, bool _enableLog, CmdDatabase _commands) {
            this.enableLog = _enableLog;
            this.spotiBoti = _spotiBoti;
            this.ircInfo = _ircInfo;
            this.commands = _commands;
            Initialize();
        }

        #region Private methods
        //Initialize
        private void Initialize() {
            this.ircClient = new IrcClient(ircInfo);
            this.twitchThread = new Thread(RunTwitch);
            this.spotifyThread = new Thread(RunSpotify);

            this.twitchThread.Start();
            this.spotifyThread.Start();
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
                    spotiBoti.LogToLog(enableLog, LogPath, message);
                    spotiBoti.LogToConnect(message);
                    ProcessMessage(message, username, messageonly);
                }
            } catch(Exception ex) {
                if(!ex.Message.Contains("thread") && !ex.Message.Contains("Thread"))
                    MessageBox.Show(ex.Message);
            }
        }

        //Return Username
        private string getUsername(string Message) {
            return Message.Substring(1, Message.IndexOf('!') - 1);
        }

        //Return first word of chatline
        private string getCommand(string Message) {
            string temp = getChatline(Message);
            return temp.Substring(temp.IndexOf(':') + 1).Split(' ')[0];
        }

        //Return chatline only
        private string getChatline(string Message) {
            string temp = Message.Substring(Message.IndexOf(':') + 1);
            return temp.Substring(temp.IndexOf(':') + 1);
        }

        //Detect commands in chat messages
        private void ProcessMessage(string Command, string Username, string Chatline) {
            Command = Command.ToLower();

            if(!String.IsNullOrEmpty(Username)) {
                spotiBoti.LogToChat(Username, Chatline);
            }
            if(Command.Contains("!help")) {
                string helpmessage = "You can use following Commands. Each Command starts with !";
                ircClient.IrcSendChatMessage(helpmessage);
                spotiBoti.LogToChat("BOT", helpmessage);
                Thread.Sleep(2000);

                foreach(string[] msg in ReturnCustomCommands()) {
                    ircClient.IrcSendChatMessage(msg[0]);
                    spotiBoti.LogToLog(enableLog, LogPath, msg[0]);
                    spotiBoti.LogToChat("BOT", msg[0]);
                    Thread.Sleep(1000);
                }
            }

            //TODO: Create Songrequestmethod etc.
            if(Command.Contains("!songrequest")) {
                spotiBoti.UpdateFormText("SpotiBoti - NEW SONGREQUEST!!!");
            }

            foreach(string[] msg in ReturnCustomCommands()) {
                if(Command.Contains(msg[0])) {
                    Status status = Spotify.DataProviderInstance.UpdateStatus();
                    TimeSpan Uptime = DateTime.Parse(DateTime.Now.ToLongTimeString()).Subtract(TimeOfBotStarted);
                    string result = msg[1]
                        .Replace("$artist", status.Track.Artist)
                        .Replace("$track", status.Track.Name)
                        .Replace("$album", status.Track.Album)
                        .Replace("$time", DateTime.Now.ToShortTimeString())
                        .Replace("$uptime", Uptime.ToString());
                    ircClient.IrcSendChatMessage(result);
                    Thread.Sleep(2000);
                    spotiBoti.LogToLog(enableLog, LogPath, result);
                    spotiBoti.LogToChat("BOT", result);
                }
            }
        }

        //Returns string[][] of CurstomCommands
        //TODO: get these out of datbase
        private string[][] ReturnCustomCommands() {
            return System.IO.File.ReadLines("commands.txt").Select(s => s.Split('|')).ToArray();
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
            spotiBoti.LogToChat("BOT", song);
        }
        #endregion
    }
}