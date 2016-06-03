using System;
using System.Drawing;
using System.Windows.Forms;
using nSpotify;
using SpotiBotiCore;
using SpotiBotiCore.Database;
using System.Data;

//Using for testingmethods
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace SpotiBoti
{
    public partial class SpotiBoti : Form
    {
        private Twitch _twitch;
        private Commands _commands;

        IrcInfo ircInfo = new IrcInfo();

        //TODO: Move to Twitch.cs etc...
        SpotifyCore spCore = new SpotifyCore();

        //Constructor
        public SpotiBoti() {
            InitializeComponent();
#if true
            Initilize();

#else
            DoTest();
#endif
        }

        #region Testing methods
        private void DoTest() {
#if false
            string commandgotbychatline = "!currentsong";
            DataRow[] result = _commands._currentCustomCommandsDatatable.Select("Command = '" + commandgotbychatline + "'");
            if(result.Length == 1)
                txtChat.AppendLine(result[0][0] + " - " + result[0][1]);
#else
            //https://api.twitch.tv/kraken/users/mrrrrcl/follows/channels/hardlydifficult
            txtChat.Text = isFollowingMyStream("marceld89");
#endif
        }
        private string isFollowingMyStream(string username) {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(@"https://api.twitch.tv/kraken/users/mrrrrcl/follows/channels/hardlydifficult");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Accept = "*/*";
            httpWebRequest.Method = "GET";

            try {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                var streamReader = new StreamReader(httpResponse.GetResponseStream());
                return streamReader.ReadToEnd();
            } catch(Exception) {
                return "User is not following the stream!";
            }
        }
        #endregion

        #region Custom methods
        #region Public methods
        //Write to Connect-tab
        public void LogToConnect(string message) {
            Invoke(new Action(() => {
                txtLog.AppendLine(DateTime.Now.ToShortTimeString() + " " + message);
            }));
        }

        //Write to Chat-tab
        public void LogToChat(string nick, string message) {
            Invoke(new Action(() => {
                txtChat.AppendText("[" + DateTime.Now.ToShortTimeString() + "] ");
                txtChat.SelectionFont = new Font(txtChat.Font, FontStyle.Bold);
                //Todo: Maybe add custom coloring to this
                if(nick == "BOT") {
                    txtChat.AppendText(nick + ": ", Color.Green);
                } else if(nick != ircInfo.Channel) {
                    txtChat.AppendText(nick + ": ", Color.Gray);
                } else {
                    txtChat.AppendText(nick + ": ", Color.Red);
                }
                txtChat.SelectionFont = new Font(txtChat.Font, FontStyle.Regular);
                txtChat.ForeColor = Color.Black;
                txtChat.AppendLine(message);
            }));
        }

        //Write to logfile
        public void LogToLog(bool enabled, string Path, string message) {
            if(enabled)
                System.IO.File.AppendAllText(Path, DateTime.Now.ToShortTimeString() + " " + message + "\r\n");
        }

        //Write to formtitle
        public void UpdateFormText(string text) {
            Invoke(new Action(() => {
                this.Text = text;
            }));
        }
        #endregion

        #region Private methods
        private void Initilize() {
            _commands = new Commands();

            //TODO: Get this info of User...
            ircInfo.Username = "spotiboti";
            ircInfo.Channel = "mrrrrcl";
            ircInfo.OAuth = "oauth:9rvxhrc2fg4iiu153yh09uj02ekezl";

            bool EnableLog = _commands.getLog();
            _twitch = new Twitch(this, ircInfo, EnableLog, _commands);
            enableLogToolStripMenuItem.Checked = EnableLog;
        }
        #endregion
        #endregion

        #region Form and Control actions
        //FormClose events
        private void SpotiBoti_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e) {
            try {
                if(!_twitch.ircClient.IrcCloseConnection()) {
                    _twitch.twitchThread.Abort();
                    _twitch.spotifyThread.Abort();
                }
            } catch(Exception) {
            }
        }

        //PlayToolStrip events
        private void playToolStripMenuItem_Click(object sender, EventArgs e) {
            Spotify.SendPlayRequest();
            //Spotify.SendPlayRequest("https://open.spotify.com/album/6KR3nUwkoVBcwMGEduOEIx");
        }

        //PauseToolStrip events
        private void pauseToolStripMenuItem_Click(object sender, EventArgs e) {
            Spotify.SendPauseRequest();
        }

        //CustomCommandToolStrip events
        private void customCommandsToolStripMenuItem_Click(object sender, EventArgs e) {
            new Settings_Commands().ShowDialog();
        }

        //CloseToolStrip events
        private void closeToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }

        //TabControl events
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) {
            if(tabControl1.SelectedIndex == 2) {
                UpdateFormText("SpotiBoti");
            }
        }

        //EnableLogToolStrip events
        private void enableLogToolStripMenuItem_CheckedChanged(object sender, EventArgs e) {
            _twitch.enableLog = enableLogToolStripMenuItem.Checked;
        }
        private void enableLogToolStripMenuItem_Click(object sender, EventArgs e) {
            if(enableLogToolStripMenuItem.Checked) {
                enableLogToolStripMenuItem.Checked = false;
                enableLogToolStripMenuItem.CheckState = CheckState.Unchecked;
                _commands.setLog(enableLogToolStripMenuItem.Checked);
            } else {
                enableLogToolStripMenuItem.Checked = true;
                enableLogToolStripMenuItem.CheckState = CheckState.Checked;
                _commands.setLog(enableLogToolStripMenuItem.Checked);
            }
        }
        #endregion
    }
}