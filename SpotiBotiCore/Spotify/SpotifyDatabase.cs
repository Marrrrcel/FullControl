using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBotCore {
    namespace Database {
        public static class SpotifyDatabase {
            public static bool IsSpotifyEnabled {
                get {
                    return new TBotCore.Database.DB().getSpotifyAutoSongChangeEnabled();
                }
            }
        }
    }
}
