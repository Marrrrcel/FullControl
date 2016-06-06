using System;
using System.Windows.Forms;
using nSpotify;

namespace TBot
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            TBotCore.IrcInfo ircInfo = new TBotCore.IrcInfo();
            ircInfo.Username = "spotiboti";
            ircInfo.Channel = "mrrrrcl";
            ircInfo.OAuth = "oauth:9rvxhrc2fg4iiu153yh09uj02ekezI";
            if(String.IsNullOrEmpty(ircInfo.Username) && String.IsNullOrEmpty(ircInfo.Channel) &&
                String.IsNullOrEmpty(ircInfo.OAuth)) {
                //TODO: Get IrcInfo-data of user and store in file/database and read after
                    Application.Run(new TwitchLogin());
            }

            if(TBotCore.Database.SpotifyDatabase.IsSpotifyEnabled) {
                if(!Spotify.SpotifyRunning) {
                    DialogResult result = MessageBox.Show("Starting Spotify...", "Starting Spotify...", MessageBoxButtons.OKCancel);
                    if(result == DialogResult.OK) {
                        TBotCore.SpotifyCore.SpotifyStart();
                        Application.Run(new TBot(ircInfo));
                    } else if(result == DialogResult.Cancel) {
                        Application.Exit();
                    }
                } else {
                    Application.Run(new TBot(ircInfo));
                }
            } else {
                Application.Run(new TBot(ircInfo, false));
            }
        }
    }
}