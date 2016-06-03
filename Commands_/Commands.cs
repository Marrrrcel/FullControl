using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace Database {
    public class Commands {
        private SQLiteConnection _sqliteConnection;
        private SQLiteCommand _sqliteCommand;

        //Generic commands
        const string AddUptime = "insert into GenericCommands (enabled, command, result) values ('1', '!uptime', 'CURRENT UPTIME');";
        const string AddTime = "insert into GenericCommands (enabled, command, result) values ('1', '!time', 'CURRENT TIME');";
        string[] GC = new string[] { AddUptime, AddTime };

        //Disable log by default
        const string EnableLog = "insert into Log (enabled) values ('0');";

        //Constructor
        public Commands() {
            Initialize();
        }

        #region Public methods
        //Just for testing
        public string getResult(string command) {
            _sqliteConnection.Open();
            string temp = "select result from GenericCommands where command='" + command + "' and enabled = '1';";
            string result = "";
            _sqliteCommand = new SQLiteCommand(temp, _sqliteConnection);
            SQLiteDataReader _sqliteDataReader = _sqliteCommand.ExecuteReader();
            while(_sqliteDataReader.Read()) {
                result = _sqliteDataReader.GetString(0);
            }
            _sqliteConnection.Close();
            return result;
        }

        //Create default database
        public void CreateDB() {
            string CreateGenericCommandTable = "create table if not exists GenericCommands (enabled varchar(1), command varchat(50), result varchar(255))";
            string CreateCustomCommandTable = "create table if not exists CustomCommands (enabled varchar(1), command varchat(50), result varchar(255))";
            string CreateLogTable = "create table if not exists Log (enabled varchar(1))";
            ExecuteQuery(CreateGenericCommandTable);
            ExecuteQuery(CreateCustomCommandTable);
            ExecuteQuery(CreateLogTable);
            AddGenericCommands();
        }

        //Return true if log is enabled; Return false if log is disabled
        public bool getLog() {
            _sqliteConnection.Open();
            string temp = "select enabled from Log;";
            string result = "";
            _sqliteCommand = new SQLiteCommand(temp, _sqliteConnection);
            SQLiteDataReader _sqliteDataReader = _sqliteCommand.ExecuteReader();
            while(_sqliteDataReader.Read()) {
                result = _sqliteDataReader.GetString(0);
            }
            _sqliteConnection.Close();
            if(result == "1") {
                return true;
            } else {
                return false;
            }
        }

        //Enable/Disable log to database
        public void setLog(bool enable) {
            string _newValue = "";
            string _oldValue = "";
            if(enable) {
                _newValue = "1";
                _oldValue = "0";
            } else {
                _newValue = "0";
                _oldValue = "1";
            }
            ExecuteQuery("update Log set enabled = '" + _newValue + "' where enabled='" + _oldValue + "';");
        }

        //Enable/Disable generic command to database
        public void EnableGenericCommand(bool Enable, string Command) {
            _sqliteConnection.Open();
            string _enable = "1";
            if(!Enable) {
                _enable = "0";
            }
            _sqliteCommand = new SQLiteCommand(_sqliteConnection);
            _sqliteCommand.CommandText = "update GenericCommands set enabled = '" + _enable + "' where command='" + Command + "';";
            _sqliteCommand.ExecuteNonQuery();
            _sqliteConnection.Close();
        }
        #endregion

        #region Private methods
        //Initialize
        private void Initialize() {
            CreateDBFile();
            _sqliteConnection = new SQLiteConnection(@"Data Source=Commands.db;Version=3");
            CreateDB();
        }

        //Create database file
        private void CreateDBFile() {
            if(!System.IO.File.Exists(@"Commands.db")) {
                SQLiteConnection.CreateFile(@"Commands.db");
            }
        }

        //Add generic command to database
        private void AddGenericCommands() {
            if(TableIsEmpty("SELECT * FROM GenericCommands where command='!uptime'")) {
                foreach(string item in GC) {
                    ExecuteQuery(item);
                }
            }
            if(TableIsEmpty("SELECT * FROM Log;")) {
                ExecuteQuery(EnableLog);
            }
        }

        //Default executer to database
        private void ExecuteQuery(string Query) {
            _sqliteConnection.Open();
            _sqliteCommand = new SQLiteCommand(Query, _sqliteConnection);
            _sqliteCommand.ExecuteNonQuery();
            _sqliteCommand = null;
            _sqliteConnection.Close();
        }

        //Return true if specified table is empty; Return false if specified table is not empty
        private bool TableIsEmpty(string select) {
            _sqliteConnection.Open();
            _sqliteCommand = new SQLiteCommand(select, _sqliteConnection);
            SQLiteDataAdapter _sqliteDataAdapter = new SQLiteDataAdapter(_sqliteCommand);
            DataTable _dataTable = new DataTable();
            _sqliteDataAdapter.Fill(_dataTable);
            DataRow _dataRow = _dataTable.NewRow();
            if(_dataTable == null || _dataTable.Rows.Count == 0) {
                _sqliteConnection.Close();
                return true;
            } else {
                _sqliteConnection.Close();
                return false;
            }
        }
        #endregion
    }
}
