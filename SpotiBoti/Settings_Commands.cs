using System;
using System.Windows.Forms;
using System.IO;
using SpotiBotiCore.Database;

namespace SpotiBoti
{
    public partial class Settings_Commands : Form
    {
        public Settings_Commands()
        {
            InitializeComponent();
            dataGridView1.Rows.Clear();
            /*using (StreamReader reader = new StreamReader(@"commands.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var fields = reader.ReadLine().Split('|');
                    dataGridView1.Rows.Add(fields);
                }
            }*/
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
                dataGridView1.DataSource = null;
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns["Enable"].DataPropertyName = "Enable";
                dataGridView1.Columns["Command"].DataPropertyName = "Command";
                dataGridView1.Columns["Result"].DataPropertyName = "Result";
                dataGridView1.DataSource = new DB().getCustomCommandTable();
            } catch(Exception ex) {
                SpotiBotiCore.Log.Logging.Log(ex.Message, SpotiBotiCore.Log.Logging.Loglevel.Error, "Settings_Commands_Load");
            }
        }

        private void Settings_Commands_FormClosing(object sender, FormClosingEventArgs e)
        {
            //ExportGridToCSV();
            //new Commands().updateCustomCommandTable((DataTable)dataGridView1.DataSource);
        }
        private void ExportCustomCommandsGridToCSV()
        {
            string CsvFpath = @"commands.txt";
            try
            {
                System.IO.StreamWriter csvFileWriter = new StreamWriter(CsvFpath, false);

                string columnHeaderText = "";

                int countColumn = dataGridView1.ColumnCount - 1;

                if (countColumn >= 0)
                {
                    columnHeaderText = dataGridView1.Columns[0].HeaderText;
                }

                for (int i = 1; i <= countColumn; i++)
                {
                    columnHeaderText = columnHeaderText + '|' + dataGridView1.Columns[i].HeaderText;
                }


                //csvFileWriter.WriteLine(columnHeaderText);

                foreach (DataGridViewRow dataRowObject in dataGridView1.Rows)
                {
                    if (!dataRowObject.IsNewRow)
                    {
                        string dataFromGrid = "";

                        dataFromGrid = dataRowObject.Cells[0].Value.ToString();

                        for (int i = 1; i <= countColumn; i++)
                        {
                            dataFromGrid = dataFromGrid + '|' + dataRowObject.Cells[i].Value.ToString();

                            csvFileWriter.WriteLine(dataFromGrid);
                        }
                    }
                }


                csvFileWriter.Flush();
                csvFileWriter.Close();
            }
            catch (Exception ex)
            {
                SpotiBotiCore.Log.Logging.Log(ex.Message, SpotiBotiCore.Log.Logging.Loglevel.Error, "ExportCustomCommandsGridToCSV");
            }

        }

    }
}