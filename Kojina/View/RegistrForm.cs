using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2.View
{
    public partial class RegistrForm : Form
    {
        private string connectDB =
            "Data Source=DESKTOP-432U1GM\\SQLEXPRESS;Initial Catalog=Kojina;Integrated Security=True;MultipleActiveResultSets=True"; 

        private DoctorData doctorData = new DoctorData();

        private string clickCellTableStart = "";
        private string clickCellTableEnd = "";
        private string clickCellTableDoctorID = "";
        private string datecc = "";
        private string dccstart = "";
        private string dccend = "";
        private DateTime dt;

        public RegistrForm(string cCTS, string cCTE, string cCTDid)
        {
            InitializeComponent();

            clickCellTableStart = cCTS;
            clickCellTableEnd = cCTE;
            clickCellTableDoctorID = cCTDid;

            string[] ccstart = clickCellTableStart.Split(new char[] { ' ' }); // 0 - date / 1 - timestart
            string[] ccend = clickCellTableEnd.Split(new char[] { ' ' }); // 0 - date / 1 - timeend
            dccstart = ccstart[1];
            dccend = ccend[1];

            var test = ccstart[0].Split(new char[] { '.' });
            datecc = test[2] + "-" + test[1] + "-" + test[0];

            try
            {
                // "2022-12-17 14:40:52", "yyyy-MM-dd HH:mm:ss"
                dt = DateTime.ParseExact(datecc + " 14:40:52", "yyyy-MM-dd HH:mm:ss",
                    System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            label1.Text += " " + ccstart[1] + " | " + datecc;
            label2.Text += clickCellTableDoctorID;

            toolTip1.SetToolTip(comboBox1, @"Выбирите цену в зависимости от ваших условий
                                                        *Взрослый - 1500
                                                        *Детский - 1000 
                                                        *Для пожилых - 1200");
            // id 1500 => 'S'(standatr), 'C'(children), 'P'(Old)
            comboBox1.Items.Add("Взрослый - 1500");
            comboBox1.Items.Add("Детский - 1000");
            comboBox1.Items.Add("Для пожилых - 1200");
            comboBox1.SelectedIndex = 0;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 2;
            char status = 'S'; // id 1500 => 'S'(standatr), 'C'(children), 'P'(Old)
            int prise = 0;
            if (comboBox1.Text == "Детский - 1000")
            {
                status = 'C';
                prise = 1000;
            }
            else if (comboBox1.Text == "Для пожмлых - 1200")
            {
                status = 'P';
                prise = 1200;
            }
            else
            {
                status = 'S';
                prise = 1500;
            }

            //INSERT INTO Appointment VALUES ('A', '20221217', '09:00:00', '09:10:00', 'P', '1', '1500');
            //"INSERT INTO Appointment (DoctorID, [Date], StartTime, EndTime, Status, UserID, Price) VALUES (@surname,@kind_sport,@place, @id_country)";

            string query1 =
                "INSERT INTO Appointment (DoctorID, [Date], StartTime, EndTime, Status, UserID, Price) VALUES (@DoctorID, @Date, @StartTime, @EndTime, @Status, @UserID, @Price)";
            try
            {
                SqlConnection sconn = new SqlConnection(connectDB);
                sconn.Open();
                SqlCommand command = new SqlCommand(query1, sconn);

                doctorData.DoctorID = clickCellTableDoctorID;
                doctorData.Date = dt;
                doctorData.StartTime = dccstart;
                doctorData.EndTime = dccend;
                doctorData.Status = status;
                doctorData.UserID = char.Parse(i.ToString());
                doctorData.Price = prise;

                command.Parameters.AddWithValue("@DoctorID", doctorData.DoctorID);
                command.Parameters.AddWithValue("@Date", doctorData.Date);
                command.Parameters.AddWithValue("@StartTime", doctorData.StartTime);
                command.Parameters.AddWithValue("@EndTime", doctorData.EndTime);
                command.Parameters.AddWithValue("@Status", doctorData.Status);
                command.Parameters.AddWithValue("@UserID", doctorData.UserID);
                command.Parameters.AddWithValue("@Price", doctorData.Price);


                int result = command.ExecuteNonQuery();

                if (result < 0)
                    MessageBox.Show("Ошибка добавления строки в базу данных! " + result.ToString());

                sconn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error*", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            i++;

            this.Close();
        }
    }
}