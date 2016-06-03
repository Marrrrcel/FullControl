using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpotiBoti
{
    public partial class TwitchLogin : Form
    {
        public TwitchLogin()
        {
            InitializeComponent();
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string url = webBrowser1.Url.ToString();
            if (url.StartsWith(@"http://localhost/"))
            {
                webBrowser1.Visible = false;
            }
        }
    }
}