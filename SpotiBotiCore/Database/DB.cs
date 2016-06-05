using System.Data.SQLite;
using System.Data;
using System.IO;

namespace SpotiBotiCore { namespace Database {
        public class DB {
            private SQLiteConnection _sqliteConnection;
            private SQLiteCommand _sqliteCommand;

            //Runtime Datatable to search in.
            // TODO: Maybe to edit aswell
            public DataTable _currentCustomCommandsDatatable = new DataTable();

            //Runtime SQLiteDataAdapter/SQLiteDataReader to work with
            SQLiteDataAdapter _sqliteDataAdapter;
            SQLiteDataReader _sqliteDataReader;

            //Constructor
            public DB() {
                    Initialize();
            }

            #region Public methods
            //Return settings datatable
            public DataTable getSettingsTable() {
                //TODO: Create Settings table (enabled, setting) and move log to it and insert spotify enabler
                _sqliteConnection.Open();
                _sqliteCommand = new SQLiteCommand("select enabled Enable, setting Setting FROM Settings;", _sqliteConnection);
                _sqliteDataAdapter = new SQLiteDataAdapter(_sqliteCommand);
                DataTable result = new DataTable();
                _sqliteDataAdapter.Fill(result);
                _sqliteConnection.Close();
                return result;
            }

            //Return custom commands datatable
            public DataTable getCustomCommandTable() {
                _sqliteConnection.Open();
                _sqliteCommand = new SQLiteCommand("select enabled Enable, command Command, result Result FROM CustomCommands;", _sqliteConnection);
                _sqliteDataAdapter = new SQLiteDataAdapter(_sqliteCommand);
                DataTable result = new DataTable();
                _sqliteDataAdapter.Fill(result);
                _sqliteConnection.Close();
                return result;
            }

            //Return generic command result based on command
            public string getGenericCommandResult(string command) {
                string result = "";
                _sqliteConnection.Open();
                _sqliteCommand = new SQLiteCommand("select result from GenericCommands where enabled = 1 and command = '" + command + "';", _sqliteConnection);
                _sqliteDataReader = _sqliteCommand.ExecuteReader();
                while(_sqliteDataReader.Read()) {
                    result = _sqliteDataReader.GetString(0);
                }
                _sqliteConnection.Close();
                return result;
            }

            //Return custom command result based on command
            public string getCustomCommandResult(string command) {
                string result = "";
                _sqliteConnection.Open();
                _sqliteCommand = new SQLiteCommand("select result from CustomCommands where enabled = 1 and command = '" + command + "';", _sqliteConnection);
                _sqliteDataReader = _sqliteCommand.ExecuteReader();
                while(_sqliteDataReader.Read()) {
                    result = _sqliteDataReader.GetString(0);
                }
                _sqliteConnection.Close();
                return result;
            }

            //Just for testing
            public string getResult(string command) {
                _sqliteConnection.Open();
                string temp = "select result from GenericCommands where command='" + command + "' and enabled = 1;";
                string result = "";
                _sqliteCommand = new SQLiteCommand(temp, _sqliteConnection);
                _sqliteDataReader = _sqliteCommand.ExecuteReader();
                while(_sqliteDataReader.Read()) {
                    result = _sqliteDataReader.GetString(0);
                }
                _sqliteConnection.Close();
                return result;
            }


            //Return true if log is enabled; Return false if log is disabled
            public bool getLogEnabled() {
                _sqliteConnection.Open();
                string temp = "select enabled from Settings where setting = 'Log';";
                string result = "";
                _sqliteCommand = new SQLiteCommand(temp, _sqliteConnection);
                _sqliteDataReader = _sqliteCommand.ExecuteReader();
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
                ExecuteInsertUpdateQuery("update Settings set enabled = '" + _newValue + "' where enabled='" + _oldValue + "' and setting='Log';");
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
                CreateDBFile(StaticDBStrings.Databasefolder, StaticDBStrings.Databasename);
                _sqliteConnection = new SQLiteConnection(@"Data Source=" + StaticDBStrings.Databasefolder + StaticDBStrings.Databasename + ";Version=3");
                if(!File.Exists(StaticDBStrings.Databasefolder + "/" + StaticDBStrings.Databasename))
                    CreateDB();

                //_sqliteConnection.Open();
                //_sqliteCommand = new SQLiteCommand("SELECT Command,Result FROM CustomCommands WHERE enabled = '1';", _sqliteConnection);
                //_sqliteDataAdapter = new SQLiteDataAdapter(_sqliteCommand);
                //_sqliteDataAdapter.Fill(_currentCustomCommandsDatatable);
                //_sqliteConnection.Close();
            }

            //Create default database
            private void CreateDB() {
                ExecuteInsertUpdateQuery(StaticDBStrings.CreateGenericCommandTable);
                ExecuteInsertUpdateQuery(StaticDBStrings.CreateCustomCommandTable);
                ExecuteInsertUpdateQuery(StaticDBStrings.CreateSettingTable);
                AddGenericCommands();
                AddSetting();
            }

            //Create database file
            private void CreateDBFile(string path, string name) {
                if(!Directory.Exists(path)) {
                    Directory.CreateDirectory(path);
                }
                if(!System.IO.File.Exists(path + name)) {
                    SQLiteConnection.CreateFile(path + name);
                }
            }

            //Add generic command to database
            private void AddGenericCommands() {
                if(TableIsEmpty("GenericCommands")) {
                    foreach(string item in StaticDBStrings.GC) {
                        ExecuteInsertUpdateQuery(item);
                    }
                }
            }

            //Add settings to database
            private void AddSetting() {
                if(TableIsEmpty("Settings")) {
                    ExecuteInsertUpdateQuery(StaticDBStrings.InsertLogSetting);
                    ExecuteInsertUpdateQuery(StaticDBStrings.InsertSpotifyAutoSongChangeSetting);
                }
            }

            //Default executer to database
            private void ExecuteInsertUpdateQuery(string Query) {
                _sqliteConnection.Open();
                _sqliteCommand = new SQLiteCommand(Query, _sqliteConnection);
                _sqliteCommand.ExecuteNonQuery();
                _sqliteCommand = null;
                _sqliteConnection.Close();
            }

            //Return true if specified table is empty; Return false if specified table is not empty
            private bool TableIsEmpty(string Table) {
                _sqliteConnection.Open();
                _sqliteCommand = new SQLiteCommand("SELECT * FROM " + Table, _sqliteConnection);
                _sqliteDataAdapter = new SQLiteDataAdapter(_sqliteCommand);
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
}
