using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2.View
{
    public partial class DoctorForm : Form
    {
        private string connectDB =
            "Data Source=DESKTOP-432U1GM\\SQLEXPRESS;Initial Catalog=Kojina;Integrated Security=True;MultipleActiveResultSets=True"; //Data Source=DESKTOP-432U1GM\\SQLEXPRESS;Initial Catalog=Sportsmans_BD_Kucherenko;Integrated Security=True;MultipleActiveResultSets=True

        private string timed = ""; //private List<string> timed = new List<string>();
        private string timeActive = ""; //private List<string> timeActive = new List<string>();

        private string[] time =
        {
            "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00",
        };

        public DoctorForm(string message)
        {
            InitializeComponent();

            GetFIO(int.Parse(message));
            timed += GetTime(int.Parse(message));
            timeActive += GetTimeActive(int.Parse(message));
            TableDb(int.Parse(message));

            textBox1.Text += timed; //GetTime(int.Parse(message))
        }

        private void TableDb(int id)
        {
            //string query = @"SELECT id, time_num as 'time' FROM Doctor_kojina where id = @id";
            //string query = @"SELECT id FROM Doctor_kojina where id = @id";
            try
            {
                dataGridView2.Columns.AddRange(new DataGridViewColumn[]
                {
                    new DataGridViewTextBoxColumn() { Name = "Time" + id },
                    new DataGridViewButtonColumn() { Name = "Btn" },
                    new DataGridViewTextBoxColumn()
                        { Name = "Combo" } // new DataGridViewComboBoxColumn() { Name = "Combo", Items = { timed }}
                });
                //dataGridView2.Columns["" + id].Name = "/" + id;
                GetTable(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "CellContent DoctorForm|error", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void GetTable(int id)
        {
            string active = "";
            char[] charArrayTime = timed.ToCharArray();
            char[] charArrayActive = timeActive.ToCharArray();
            /*
            for (int i = 0; i < charArrayTime.Length; i++)
            {
                textBox1.Text += charArrayTime[i] + "|";//GetTime(int.Parse(message))
            }
            textBox1.Text += "\r | \r";
            for (int i = 0; i < charArrayActime.Length; i++)
            {
                textBox1.Text += charArrayActime[i] + "|";//GetTime(int.Parse(message))
            }*/

            //textBox1.Text += "\r | \r";

            for (int i = 0; i < charArrayActive.Length; i++)
            {
                for (int j = 0; j < charArrayTime.Length; j++)
                {
                    if (charArrayActive[j] == charArrayTime[i])
                    {
                        //textBox1.Text += "Активн - " +charArrayActime[i] + "| \r";
                        active += charArrayActive[i];
                        textBox1.Text += "active - " + active + "*";
                    }
                }
            }

            // active cacl
            //char[] charArrayActive = active.ToCharArray();
            List<string> timeset = new List<string>();
            int inc = 0;
            inc++;
            for (int i = 0; i < charArrayActive.Length; i++) 
            {
                textBox1.Text += "charArrayActive - " + charArrayActive[i] + "|*|";

                if (charArrayActive[i] == '1')
                {
                    timeset[i] = "8:30";
                    textBox1.Text += "timeset -" + timeset[i] + "|t|";
                    inc++;
                }
                /*switch (charArrayActive[i])
                 {
                     case '1':
                         timeset[i] += "8:30";
                         textBox1.Text += "timeset -" + timeset[i] + "|t|";
                         inc++;
                         break;
                     case '2':
                         timeset[i] += "9:00";  textBox1.Text += "timeset -" + timeset[i] + "|t|";
                         inc++;
                         break;
                     case '3':
                         timeset[i] += "9:30";  textBox1.Text += "timeset -" + timeset[i] + "|t|";
                         inc++;
                         break;
                     case '4':
                         timeset[i] += "9:30";  textBox1.Text += "timeset -" + timeset[i] + "|t|";
                         inc++;
                         break;
                     case '5':
                         timeset[i] += "10:00";  textBox1.Text += "timeset -" + timeset[i] + "|t|";
                         inc++;
                         break;
                     case '6':
                         timeset[i] += "10:30";  textBox1.Text += "timeset -" + timeset[i] + "|t|";
                         inc++;
                         break;
                     default:
                         timeset[i] += "14:00";  textBox1.Text += "timeset -" + timeset[i] + "|t|";
                         inc++;
                         break;
                 }*/
            }

            for (int i = 0; i < inc; i++)
            {
                textBox1.Text += timeset[i] + "|" + time[i];
                if (timeset[i] == time[i])
                {
                    dataGridView2.Rows.Add(time[i], timeActive, "Свободно");
                }
            }
            //dataGridView2.Rows.Add(time[i], timeActive);
        }

        private void GetFIO(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectDB))
            {
                connection.Open();

                SqlCommand command2 = new SqlCommand("Select fio from Doctor_kojina where id = @id", connection);
                command2.Parameters.Add("@id", SqlDbType.NVarChar, 50);
                command2.Parameters["@id"].Value = id;

                object fio = command2.ExecuteScalar(); //int c = (int)count + 1;
                label1.Text = fio + ".";

                connection.Close();
            }
        }

        private string GetTime(int id)
        {
            string result = "";
            using (SqlConnection connection = new SqlConnection(connectDB))
            {
                connection.Open();

                string query = @"SELECT id, time_num as 'time' FROM Doctor_kojina where id = @id";
                SqlCommand command2 = new SqlCommand(query, connection);
                command2.Parameters.Add("@id", SqlDbType.NVarChar, 50);
                command2.Parameters["@id"].Value = id;

                object dbid = command2.ExecuteScalar();

                SqlDataReader reader = command2.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        string time = reader.GetString(1);
                        result += time;
                    }
                }

                connection.Close();
            }

            return result;
        }

        private string GetTimeActive(int id)
        {
            string result = "";
            using (SqlConnection connection = new SqlConnection(connectDB))
            {
                connection.Open();

                string query = @"SELECT id, active FROM Doctor_kojina where id = @id";
                SqlCommand command2 = new SqlCommand(query, connection);
                command2.Parameters.Add("@id", SqlDbType.NVarChar, 50);
                command2.Parameters["@id"].Value = id;

                object dbid = command2.ExecuteScalar();

                SqlDataReader reader = command2.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        string time = reader.GetString(1);
                        result += time;
                    }
                }

                connection.Close();
            }

            return result;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Columns[dataGridView2.CurrentCell.ColumnIndex].HeaderText.ToString() == "Записаться")
            {
                //RegistrForm registrForm = new RegistrForm();
                //registrForm.ShowDialog();
            }
            else if (dataGridView2.Columns[dataGridView2.CurrentCell.ColumnIndex].HeaderText.ToString() == "Число")
            {
            }
            else if (dataGridView2.Columns[dataGridView2.CurrentCell.ColumnIndex].HeaderText.ToString() == "Время")
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

/*
             SqlConnection conn = new SqlConnection(connectDB);
            SqlCommand comm = new SqlCommand(query, conn);
            comm.Parameters.Add("@id", SqlDbType.NVarChar, 50);
            comm.Parameters["@id"].Value = id;
            
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            adapter.Fill(table);
            dataGridView2.DataSource = table;
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            * //conn.Close();
 * 
*/


/*DataGridViewButtonColumn uninstallButtonColumn = new DataGridViewButtonColumn();
uninstallButtonColumn.Name = "Записаться";
uninstallButtonColumn.Text = "Uninstall";
int columnIndex = 1;
if (dataGridView2.Columns["Записаться"] == null)
{
    dataGridView2.Columns.Insert(columnIndex, uninstallButtonColumn);
    //dataGridView2.Columns.Insert(3, uninstallButtonColumn);
}*/

/*
 switch (charArrayActive[i])
            {
                case '1':
                    textBox1.Text += "8:30" + charArrayActive[i] + "| \r";
                    dataGridView2.Rows.Add("8:30", timeActive);
                    break;
                case '2':
                    textBox1.Text += "9:30" + charArrayActive[i] + "| \r";
                    dataGridView2.Rows.Add("9:30", timeActive);
                    break;
                case '3':
                    textBox1.Text += "10:00" + charArrayActive[i] + "| \r";
                    dataGridView2.Rows.Add("10:00", timeActive);
                    break;
                case '4':
                    textBox1.Text += "10:30" + charArrayActive[i] + "| \r";
                    dataGridView2.Rows.Add("10:30", timeActive);
                    break;
                case '5':
                    textBox1.Text += "11:00" + charArrayActive[i] + "| \r";
                    break;
                case '6':
                    textBox1.Text += "11:30" + charArrayActive[i] + "| \r";
                    break;
                case '7':
                    textBox1.Text += "12:00" + charArrayActive[i] + "| \r";
                    break;
            }
 */
}