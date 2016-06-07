using System;

namespace TBotCore {
    namespace Log {
        public static class Logging {

            static string logDir = @"bin/log";

            public enum Loglevel {
                Info,
                Warning,
                Error
            }

            public static void Log(string Message, Loglevel Severity, string Method="") {
                string _method = "";
                string CurrentDate = DateTime.Now.ToShortDateString();
                string CurrentTime = "[" + DateTime.Now.ToShortTimeString() + _method + "] ";
                string LogPath = logDir +"/" + CurrentDate + "TBot_";
                bool LogEnabled = new TBotCore.Database.DB().getLogEnabled();

                if(Method != "") {
                    _method = ", " + Method;
                }
                string _message = CurrentTime + Message;
                
                switch(Severity) {
                    case Loglevel.Info:
                        LogPath += "Info.log";
                        break;
                    case Loglevel.Warning:
                        LogPath += "Warning.log";
                        break;
                    case Loglevel.Error:
                        LogPath += "Error.log";
                        LogEnabled = true;
                        //Just for Debug
                        System.Windows.Forms.MessageBox.Show("ups, there was an error!");
                        break;
                    default:
                        LogPath += "Debug.log";
                        LogEnabled = true;
                        break;
                }
                WriteToLog(LogEnabled, LogPath, _message);
            }

            private static void WriteToLog(bool LogEnabled, string LogPath, string Message) {
                if(LogEnabled) {
                    if(!System.IO.Directory.Exists(logDir))
                        System.IO.Directory.CreateDirectory(logDir);

                    System.IO.File.AppendAllText(LogPath, Message + "\r\n");
                }
            }
        }
    }
}