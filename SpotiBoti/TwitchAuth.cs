using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBot
{
    public partial class TwitchAuth : Form
    {
        public TwitchAuth()
        {
            InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e) {
            this.Dispose();
            Application.Exit();
        }

        private void btn_OK_Click(object sender, EventArgs e) {
            if(Test()) {
                SaveData();
                this.Close();
            } else {
                MessageBox.Show("Something went wrong! \r\n Please try again!");
            }
        }

        private bool Test() {
            TBotCore.IrcInfo ircInfo = new TBotCore.IrcInfo();
            ircInfo.Channel = txt_Channel.Text;
            ircInfo.Username = txt_Username.Text;
            ircInfo.OAuth = txt_OAuth.Text;
            TBotCore.IrcClient ircClient = new TBotCore.IrcClient(ircInfo);
            ircClient.IrcConnect();
            bool result = false;
            try {
                do {
                    string message = ircClient.IrcreadChatMessage();
                    if(message.Contains("Welcome, GLHF!")) {
                        ircClient.IrcCloseConnection();
                        result = true;
                    }
                } while(ircClient.IrcIsConnected());
            } catch(Exception ex) {
                TBotCore.Log.Logging.Log(ex.Message, TBotCore.Log.Logging.Loglevel.Error);
            }

            return result;
        }

        private void SaveData() {
            if(!System.IO.Directory.Exists(@"bin")) {
                System.IO.Directory.CreateDirectory(@"bin");
            }
            System.IO.File.AppendAllText(@"bin/data.txt", "channel," + txt_Channel.Text + "\r\n");
            System.IO.File.AppendAllText(@"bin/data.txt", "username," + txt_Username.Text + "\r\n");
            System.IO.File.AppendAllText(@"bin/data.txt", "oauth," + txt_OAuth.Text + "\r\n");
        }

        /*private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //https://api.twitch.tv/kraken/oauth2/authorize?response_type=token&client_id=iv2gg0qsw415h49t8b7qczj6cio34q3&redirect_uri=http%3A%2F%2Flocalhost&scope=user_blocks_rea//+channel_read+channel_editor+channel_subscriptions+channel_check_subscription+chat_login
            string url = webBrowser1.Url.ToString();
            if (url.StartsWith(@"http://localhost/"))
            {
                webBrowser1.Visible = false;
            }
        }*/
    }
}