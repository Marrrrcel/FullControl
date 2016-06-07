using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SQLite;

namespace TBot
{
    public partial class CustomCommands : Form
    {
        private SQLiteConnection sqlConnection = null;
        private SQLiteDataAdapter sqlDataAdapter = null;
        private SQLiteCommandBuilder sqlCommandBuilder = null;
        private DataTable dataTable = null;
        private BindingSource bindingSource = null;

        public CustomCommands()
        {
            InitializeComponent();
            dataGridView1.Rows.Clear();
            txt_helpLeft.Text =
                    " $artist = Current songs artist\r\n" +
                    " $track = Current songs tracknamer\r\n" +
                    " $album = Current songs album\r\n" +
                    " $time = Current time HH:MM\r\n" +
                    " $uptime = Bot/Stream is running for HH:MM\r\n";
            txt_helpRight.Text =
                    "more to follow up\r\n";
        }

        private void Settings_Commands_Load(object sender, EventArgs e)
        {
            try {
                sqlConnection = new SQLiteConnection(@"Data Source=" + TBotCore.Database.StaticDBStrings.Databasefolder + TBotCore.Database.StaticDBStrings.Databasename + ";Version=3");

                sqlConnection.Open();

                sqlDataAdapter = new SQLiteDataAdapter(TBotCore.Database.StaticDBStrings.SelectCustomCommandTable, sqlConnection);
                sqlCommandBuilder = new SQLiteCommandBuilder(sqlDataAdapter);

                dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                bindingSource = new BindingSource();
                bindingSource.DataSource = dataTable;

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns["Enable"].DataPropertyName = "Enable";
                dataGridView1.Columns["Command"].DataPropertyName = "Command";
                dataGridView1.Columns["Result"].DataPropertyName = "Result";
                dataGridView1.DataSource = bindingSource;
            } catch(Exception ex) {
                TBotCore.Log.Logging.Log(ex.Message, TBotCore.Log.Logging.Loglevel.Error, "Settings_Commands_Load");
            }
        }

        private void Settings_Commands_FormClosing(object sender, FormClosingEventArgs e)
        {
            try {
                dataGridView1.EndEdit();
                sqlDataAdapter.Update(dataTable);
            } catch(Exception ex) {
                TBotCore.Log.Logging.Log(ex.Message, TBotCore.Log.Logging.Loglevel.Error, "Settings_Commands_FormClosing");
            } finally {
                sqlConnection.Close();
                this.Dispose();
            }
        }
    }
}