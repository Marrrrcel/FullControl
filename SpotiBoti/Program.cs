using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using nSpotify;

namespace SpotiBoti
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
            if(SpotiBotiCore.Database.SpotifyDatabase.IsSpotifyEnabled) {
                if(!Spotify.SpotifyRunning) {
                    DialogResult result = MessageBox.Show("Starting Spotify...", "Starting Spotify...", MessageBoxButtons.OKCancel);
                    if(result == DialogResult.OK) {
                        Spotify.StartSpotifyWebHelper();
                        Spotify.StartSpotify();
                        Application.Run(new SpotiBoti());
                    } else if(result == DialogResult.Cancel) {
                        Application.Exit();
                    }
                } else {
                    Application.Run(new SpotiBoti());
                }
            } else {
                Application.Run(new SpotiBoti(false));
            }
        }
    }
}
