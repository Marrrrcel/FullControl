using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotiBotiCore {
    namespace Database {
        public static class SpotifyDatabase {
            public static bool IsSpotifyEnabled {
                get {
                    Commands cmd = new Commands();
                    DataTable dt = cmd.getSettingsTable();
                    return true; 
                }
                set { IsSpotifyEnabled = value; }
            }
        }
    }
}
