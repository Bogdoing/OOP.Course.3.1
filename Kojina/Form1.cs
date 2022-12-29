using System.Data;
using System.Data.SqlClient;
using WinFormsApp2.View;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        private string connectDB =
            "Data Source=DESKTOP-432U1GM\\SQLEXPRESS;Initial Catalog=Kojina;Integrated Security=True;MultipleActiveResultSets=True"; //Data Source=DESKTOP-432U1GM\\SQLEXPRESS;Initial Catalog=Sportsmans_BD_Kucherenko;Integrated Security=True;MultipleActiveResultSets=True

        public Form1()
        {
            InitializeComponent();

            string squery = @"select * from Doctor_kojina";
            try
            {
                SqlConnection sconn = new SqlConnection(connectDB);
                SqlCommand scomm = new SqlCommand(squery, sconn);
                DataTable stable = new DataTable();
                SqlDataAdapter sadapter = new SqlDataAdapter(scomm);
                sadapter.Fill(stable);
                comboBox1.DataSource = stable;
                //comboBox1.DisplayMember = "city";
                comboBox1.ValueMember = "id";

                string query = @"SELECT id, fio, locate_doctor, prise FROM Doctor_kojina";
                try
                {
                    SqlConnection conn = new SqlConnection(connectDB);
                    SqlCommand comm = new SqlCommand(query, conn);
                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(comm);
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                    dataGridView1.Columns[0].Visible = false;
                    //dataGridView1.SortedColumn = 
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    DataGridViewButtonColumn uninstallButtonColumn = new DataGridViewButtonColumn();
                    uninstallButtonColumn.Name = "Записаться к врачу";
                    uninstallButtonColumn.Text = "Uninstall";
                    int columnIndex = 4;
                    if (dataGridView1.Columns["Записаться к врачу"] == null)
                    {
                        dataGridView1.Columns.Insert(columnIndex, uninstallButtonColumn);
                    }
                    /*
                    DataGridViewTextBoxColumn preTimeColumn = new DataGridViewTextBoxColumn();
                    preTimeColumn.Name = "Ближайшее время";
                    columnIndex = 5;
                    if (dataGridView1.Columns["Ближайшее время"] == null)
                    {
                        using (SqlConnection connection = new SqlConnection(connectDB))
                        {
                            connection.Open();

                            SqlCommand command2 = new SqlCommand("Select active from Doctor_kojina", connection);
                            string time = "";
                            object activetime = command2.ExecuteScalar(); //int c = (int)count + 1;
                            
                            SqlDataReader reader = command2.ExecuteReader();
                            if (reader.HasRows) // если есть данные
                            {
                                while (reader.Read()) // построчно считываем данные
                                {
                                    time = reader.GetString(0);
                                }
                            }
                            
                            char[] charArrayTime = time.ToCharArray();
                            int[] intArrayTime = Array.ConvertAll(charArrayTime, c => (int)Char.GetNumericValue(c));
                            int mintime = intArrayTime.Min();
                            dataGridView1.Rows.Insert(columnIndex, mintime);
                            
                            connection.Close();
                        }
                        
                    }

                    conn.Close();*/
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "|error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "|error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormAppointment formAppointment = new FormAppointment();
            formAppointment.ShowDialog();
            /*try
            {
                textBox1.Text = "";
                bdsetvar();
                dbvision();
            }
            catch (Exception exception)
            {
                MessageBox.Show("|" + exception);
                throw;
            }*/
        }

        private void bdsetvar()
        {
            using (SqlConnection connection = new SqlConnection(connectDB))
            {
                connection.Open();

                SqlCommand command2 = new SqlCommand("Select id from Doctor_kojina where id = @id", connection);
                command2.Parameters.Add("@id", SqlDbType.NVarChar, 50);
                command2.Parameters["@id"].Value = 12;

                object count = command2.ExecuteScalar();
                int c = (int)count + 1;
                textBox1.Text += c + "|||";

                connection.Close();
            }
        }

        private void dbvision()
        {
            string sqlExpression = "SELECT * FROM Doctor_kojina";

            using (SqlConnection connection = new SqlConnection(connectDB))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    // выводим названия столбцов
                    textBox1.Text += reader.GetName(0) + "\t" + reader.GetName(1) + "\t" + reader.GetName(2)
                                     + "\t" + reader.GetName(3) + "\t" + reader.GetName(4) + "\t" + reader.GetName(5);
                    textBox1.Text += "\n\r";

                    while (reader.Read()) // построчно считываем данные
                    {
                        object id = reader.GetValue(0);
                        object fio = reader.GetValue(1);
                        object locate_doctor = reader.GetValue(2);
                        object prise = reader.GetValue(3);
                        object time_num = reader.GetValue(4);
                        object active = reader.GetValue(5);

                        textBox1.Text += id + "\t" + fio + "\t" + locate_doctor + "\t" +
                                         prise + "\t" + time_num + "\t" + active;
                        textBox1.Text += "\r";
                    }
                }

                reader.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].HeaderText.ToString() == "Записаться к врачу")
                {
                    string message = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString();
                    MessageBox.Show(message);
                    DoctorForm doctorForm = new DoctorForm(message);
                    doctorForm.ShowDialog();
                }
                /*
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)//== "Записаться к врачу"
                {
                    string message = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString();
                    //MessageBox.Show(message);
                    DoctorForm doctorForm = new DoctorForm(message);
                    doctorForm.ShowDialog();
                }
                */
            }
            catch (Exception exception)
            {
                MessageBox.Show("cell content | " + exception);
                throw;
            }
        }
    }
}