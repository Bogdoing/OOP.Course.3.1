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
    public partial class FormAppointment : Form
    {
        private string connectDB =
            "Data Source=DESKTOP-432U1GM\\SQLEXPRESS;Initial Catalog=Kojina;Integrated Security=True;MultipleActiveResultSets=True"; 

        private string clickCellTableStart = "";
        private string clickCellTableEnd = "";
        private string clickCellTableDoctorID = "";
        private string clickCellTableUserID = "";

        public FormAppointment()
        {
            InitializeComponent();

            comboBox2.Items.Add("5");
            comboBox2.Items.Add("10");
            comboBox2.Items.Add("20");
            comboBox2.Items.Add("30");
            comboBox2.Items.Add("40");
            comboBox2.Items.Add("50");
            comboBox2.SelectedIndex = 0;
        }

        private void FormAppointment_Load(object sender, EventArgs e)
        {
            string squery = @$"
                DECLARE @doctorID char(1) = '{comboBox1.Text}';
                DECLARE @length tinyint = 20;
                WITH Slots AS (
                    SELECT StartTime = DATEADD(MINUTE, ((DATEPART(MINUTE, GETDATE())/10)+1+Number)*10, DATEADD(HOUR, DATEPART(HOUR, GETDATE()), CONVERT(smalldatetime, CONVERT(date, GETDATE())))),
                           EndTime = DATEADD(MINUTE, @length, DATEADD(MINUTE, ((DATEPART(MINUTE, GETDATE())/10)+1+Number)*10, DATEADD(HOUR, DATEPART(HOUR, GETDATE()), CONVERT(smalldatetime, CONVERT(date, GETDATE())))))
                      FROM Numbers)
                SELECT TOP {comboBox2.Text} DoctorID  = @doctorID,
                    s.StartTime as 'Начало приёма',
                    s.EndTime as 'Конец приёма'
                  FROM Slots AS s
                  WHERE NOT EXISTS (SELECT 1 
                                      FROM Appointment AS a
                                      WHERE (CONVERT(time(0), s.StartTime) < a.EndTime AND CONVERT(time(0), s.EndTime) > a.StartTime)
                                        AND a.DoctorID = @doctorID
                                        AND a.[Date] = CONVERT(date, s.StartTime))
                    AND DATEPART(HOUR, s.StartTime) >= 9
                    AND DATEPART(HOUR, DATEADD(MINUTE, -1, s.EndTime)) <= 16
                ORDER BY s.StartTime;
                ";
            try
            {
                SqlConnection sconn = new SqlConnection(connectDB);
                SqlCommand scomm = new SqlCommand(squery, sconn);
                DataTable stable = new DataTable();
                SqlDataAdapter sadapter = new SqlDataAdapter(scomm);
                sadapter.Fill(stable);
                dataGridView1.DataSource = stable;
                //dataGridView1.Columns[0].Visible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.Beige;
                dataGridView1.Columns[2].DefaultCellStyle.BackColor = Color.Beige;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "|error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw;
            }

            squery = @"select DISTINCT DoctorID from Appointment";
            try
            {
                SqlConnection sconn = new SqlConnection(connectDB);
                SqlCommand scomm = new SqlCommand(squery, sconn);
                DataTable stable = new DataTable();
                SqlDataAdapter sadapter = new SqlDataAdapter(scomm);
                sadapter.Fill(stable);

                comboBox1.DataSource = stable;
                comboBox1.DisplayMember = "DoctorID";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "|error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string squery = @"select DoctorID as 'id доктора', [Date] as 'Дата', StartTime as 'Начало приёма', 
                                EndTime as 'Конец приёма', Status as 'Статус',
                                UserID as 'id пользователя', Price as 'Стоимость'
                                from Appointment";
            try
            {
                SqlConnection sconn = new SqlConnection(connectDB);
                SqlCommand scomm = new SqlCommand(squery, sconn);
                DataTable stable = new DataTable();
                SqlDataAdapter sadapter = new SqlDataAdapter(scomm);
                sadapter.Fill(stable);
                dataGridView1.DataSource = stable;
                //dataGridView1.Columns[0].Visible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Columns[2].DefaultCellStyle.BackColor = Color.Beige;
                dataGridView1.Columns[3].DefaultCellStyle.BackColor = Color.Beige;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "|error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].HeaderText.ToString() ==
                    "Начало приёма")
                {
                    clickCellTableStart = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    clickCellTableEnd = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString();
                    clickCellTableDoctorID = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                    textBox1.Text += "S| " + dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()
                                           + dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString()
                                           + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()
                                           + "\r \n";
                }
                else if (dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].HeaderText.ToString() ==
                         "Конец приёма")
                {
                    clickCellTableEnd = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    clickCellTableStart = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString();
                    clickCellTableDoctorID = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                    textBox1.Text += "E| " + dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString()
                                           + dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()
                                           + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()
                                           + "\r \n";
                }
                else{}
            }
            catch (Exception exception)
            {
                MessageBox.Show("cell content exp | " + exception);
                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (clickCellTableStart != null)
            {
                textBox1.Text += "BTN| " + clickCellTableStart + " | " + clickCellTableEnd + "\r \n";
                RegistrForm registrForm = 
                    new RegistrForm(clickCellTableStart, clickCellTableEnd, clickCellTableDoctorID);
                registrForm.ShowDialog();
            }
            else
            {
                textBox1.Text += "NULL \r";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormAppointment_Load(sender, e);
        }
    }
}