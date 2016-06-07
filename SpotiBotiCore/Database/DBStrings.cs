using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBotCore { namespace Database {
        public static class StaticDBStrings {
            //Create DB strings
            public static string Databasefolder = @"bin/database/";
            public static string Databasename = "commands.db";
            public static string CreateGenericCommandTable = "CREATE TABLE IF NOT EXISTS [GenericCommands] ([id] INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                                                                "[enabled] int  NULL, [command] varchat(50)  NULL, [result] varchar(255)  NULL)";
            public static string CreateCustomCommandTable = "CREATE TABLE IF NOT EXISTS [CustomCommands] ([id] INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                                                                "[enabled] int  NULL, [command] varchat(50)  NULL, [result] varchar(255)  NULL)";
            public static string CreateSettingTable = "CREATE TABLE IF NOT EXISTS [Settings] ([id] INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                                                        "[enabled] varchar(1)  NULL, [setting] varchar(255)  NULL)";

            //Generic commands
            private static string AddUptime = "insert into GenericCommands (enabled, command, result) values (1, '!uptime', 'CURRENT UPTIME');";
            private static string AddTime = "insert into GenericCommands (enabled, command, result) values (1, '!time', 'CURRENT TIME');";
            private static string AddCustom1 = "insert into CustomCommands (enabled, command, result) values (1, '!currentsong', 'CURRENT Song: $track - $artist');";
            private static string AddCustom2 = "insert into CustomCommands (enabled, command, result) values (0, '!currentalbum', 'CURRENT Song Album: $album');";
            public static string[] GC = new string[] { AddUptime, AddTime, AddCustom1, AddCustom2 };

            //Settings
            //Default Log off
            public static string InsertLogSetting = "insert into Settings (enabled, setting) values ('0', 'Log');";
            //Default SpotifySongchange On
            public static string InsertSpotifyAutoSongChangeSetting = "insert into Settings (enabled, setting) values ('1', 'SpotifyAutoSongChange');";


            //SELECT Strings
            public static string SelectCustomCommandTable = "select id ID, enabled Enable, command Command, result Result FROM CustomCommands;";
            public static string SelectGenericCommandTable = "select id ID, enabled Enable, command Command, result Result FROM GenericCommands;";
        }
    }
}