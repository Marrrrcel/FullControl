using System;
using System.Windows.Forms;
using nSpotify;

namespace TBot {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            TBotCore.IrcInfo ircInfo = new TBotCore.IrcInfo();
            if(System.IO.File.Exists(@"bin/data.txt")) {
                var _ircInfo = System.IO.File.ReadAllLines(@"bin/data.txt");
                if(_ircInfo.Length == 3) {
                var _ircInfoChannel = _ircInfo[0].Split(',');
                var _ircInfoUsername = _ircInfo[1].Split(',');
                var _ircoInfoOAuth = _ircInfo[2].Split(',');
                
                    ircInfo.Channel = _ircInfoChannel[1];
                    ircInfo.Username = _ircInfoUsername[1];
                    ircInfo.OAuth = _ircoInfoOAuth[1];
                }
            }
            if(String.IsNullOrEmpty(ircInfo.Username) && String.IsNullOrEmpty(ircInfo.Channel) &&
                String.IsNullOrEmpty(ircInfo.OAuth)) {
                //TODO: Maybe store in database instead of file?
                Application.Run(new TwitchLogin());
                if(System.IO.File.Exists(@"bin/data.txt")) {
                    var _ircInfo = System.IO.File.ReadAllLines(@"bin/data.txt");
                    if(_ircInfo.Length == 3) {
                        var _ircInfoChannel = _ircInfo[0].Split(',');
                        var _ircInfoUsername = _ircInfo[1].Split(',');
                        var _ircoInfoOAuth = _ircInfo[2].Split(',');

                        ircInfo.Channel = _ircInfoChannel[1];
                        ircInfo.Username = _ircInfoUsername[1];
                        ircInfo.OAuth = _ircoInfoOAuth[1];
                    }
                }
            }

            if(TBotCore.Database.SpotifyDatabase.IsSpotifyEnabled) {
                if(!Spotify.SpotifyRunning) {
                    DialogResult result = MessageBox.Show("Starting Spotify...", "Starting Spotify...", MessageBoxButtons.OKCancel);
                    if(result == DialogResult.OK) {
                        TBotCore.SpotifyCore.SpotifyStart();
                        Application.Run(new TBot(ircInfo, true));
                    } else if(result == DialogResult.Cancel) {
                        Application.Exit();
                    }
                } else {
                    Application.Run(new TBot(ircInfo, true));
                }
            } else {
                Application.Run(new TBot(ircInfo, false));
            }
        }
    }
}