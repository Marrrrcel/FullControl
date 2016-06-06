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
            if(TBotCore.Database.SpotifyDatabase.IsSpotifyEnabled) {
                if(!Spotify.SpotifyRunning) {
                    DialogResult result = MessageBox.Show("Starting Spotify...", "Starting Spotify...", MessageBoxButtons.OKCancel);
                    if(result == DialogResult.OK) {
                        TBotCore.SpotifyCore.SpotifyStart();
                        Application.Run(new TBot());
                    } else if(result == DialogResult.Cancel) {
                        Application.Exit();
                    }
                } else {
                    Application.Run(new TBot());
                }
            } else {
                Application.Run(new TBot(false));
            }
        }
    }
}