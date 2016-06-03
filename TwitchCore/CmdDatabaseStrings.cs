using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpotiBotiCore {
    namespace Strings {
        public static class CmdStaticStrings {

            public static string[] GenericAndCustomExampleCommands {
                get {
                    return new string[] { AddUptime, AddTime, AddCustom1, AddCustom2 };
                }
            }

            public static string Databasefolder {
                get {
                    return @"bin/database/";
                }
            }

            public static string Databasename {
                get {
                    return "commands.db";
                }
            }

            public static string CreateGenericCommandTable {
                get {
                    return "create table if not exists GenericCommands (enabled int, command varchat(50), result varchar(255))";
                }
            }

            public static string CreateCustomCommandTable {
                get {
                    return "create table if not exists CustomCommands (enabled int, command varchat(50), result varchar(255))";
                }
            }

            public static string CreateLogTable {
                get {
                    return "create table if not exists Log (enabled varchar(1))";
                }
            }

            public static string DefaultInsertEnableLog {
                get {
                    return "insert into Log (enabled) values ('0');";
                }
            }

            private static string AddUptime { //Generic
                get {
                    return "insert into GenericCommands (enabled, command, result) values (1, '!uptime', 'CURRENT UPTIME');";
                }
            }

            private static string AddTime { //Generic
                get {
                    return "insert into GenericCommands (enabled, command, result) values (1, '!time', 'CURRENT TIME');";
                }
            }

            private static string AddCustom1 { //Example custom command #1
                get {
                    return "insert into CustomCommands (enabled, command, result) values (1, '!currentsong', 'CURRENT Song: $track - $artist');";
                }
            }

            private static string AddCustom2 { //Example custom command #2
                get {
                    return "insert into CustomCommands (enabled, command, result) values (0, '!currentalbum', 'CURRENT Song Album: $album');";
                }
            }
        }
    }
}