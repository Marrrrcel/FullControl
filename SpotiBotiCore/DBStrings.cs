using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotiBotiCore {
    public static class StaticDBStrings {
        //Create DB strings
        public static string Databasefolder = @"bin/database/";
        public static string Databasename = "commands.db";
        public static string CreateGenericCommandTable = "create table if not exists GenericCommands (enabled int, command varchat(50), result varchar(255))";
        public static string CreateCustomCommandTable = "create table if not exists CustomCommands (enabled int, command varchat(50), result varchar(255))";
        public static string CreateLogTable = "create table if not exists Log (enabled varchar(1))";

        //Generic commands
        private static string AddUptime = "insert into GenericCommands (enabled, command, result) values (1, '!uptime', 'CURRENT UPTIME');";
        private static string AddTime = "insert into GenericCommands (enabled, command, result) values (1, '!time', 'CURRENT TIME');";
        private static string AddCustom1 = "insert into CustomCommands (enabled, command, result) values (1, '!currentsong', 'CURRENT Song: $track - $artist');";
        private static string AddCustom2 = "insert into CustomCommands (enabled, command, result) values (0, '!currentalbum', 'CURRENT Song Album: $album');";
        public static string[] GC = new string[] { AddUptime, AddTime, AddCustom1, AddCustom2 };

        //Disable log by default
        public static string DefaultInsertEnableLog = "insert into Log (enabled) values ('0');";
    }
}
