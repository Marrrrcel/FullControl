using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SpotiBotiCore
{
    public class IrcClient
    {
        private TcpClient tcpClient;
        private IrcInfo _irc;
        private StreamReader inputStream;
        private StreamWriter outputStream;

        public IrcClient(IrcInfo irc)
        {
            this._irc = irc;
        }

        public void IrcConnect()
        {
            tcpClient = new TcpClient(_irc.IP, _irc.Port);
            inputStream = new StreamReader(tcpClient.GetStream());
            outputStream = new StreamWriter(tcpClient.GetStream());
            outputStream.WriteLine("Pass " + _irc.OAuth);
            outputStream.WriteLine("NICK " + _irc.Username);
            outputStream.WriteLine("USER " + _irc.Username + " 8 * :" + _irc.Username);
            outputStream.Flush();
            IrcJoinRoom();
        }

        private void IrcJoinRoom()
        {
            outputStream.WriteLine("JOIN #" + _irc.Channel);
            outputStream.Flush();
        }

        public void IrcSendIrcMessage(string message)
        {
            outputStream.WriteLine(message);
            outputStream.Flush();
        }

        public void IrcSendChatMessage(string message)
        {
            IrcSendIrcMessage(":" + _irc.Username + "!" + _irc.Username + "@" + _irc.Username
                + ".tmi.twitch.tv PRIVMSG #" + _irc.Channel + " :" + message);
        }

        public string IrcreadChatMessage()
        {
            return inputStream.ReadLine();
        }

        public bool IrcIsConnected()
        {
            return tcpClient.Connected;
        }

        public bool IrcCloseConnection()
        {
            tcpClient.Close();
            return tcpClient.Connected;
        }
    }
}
