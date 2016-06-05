using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SpotiBotiCore
{
    public class IrcInfo
    {
        public string IP
        {
            get { return "irc.chat.twitch.tv"; }
        }

        public string Username;

        public string OAuth;

        public string Botname
        {
            get { return OAuth; }
        }

        public string Channel;

        public int Port
        {
            get { return 6667; }
        }
    }
}