using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBot {
    static class Songrequest {

        internal static void SearchYoutube(ListBox txtSongrequest, string v) {
            //throw new NotImplementedException();
        }

    }

    public class SongrequestListBoxItem {
        public SongrequestListBoxItem(string m) {
            Message = m;
        }

        public Color ItemColor {
            get { 
                return Color.Black;
            }
        }
        public Color BackColor {
            get {
                return Color.LightSkyBlue;
            }
        }
        public string Message { get; set; }
    }
}