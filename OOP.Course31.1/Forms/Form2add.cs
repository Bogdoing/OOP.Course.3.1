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

namespace OOP.Course31._1.Forms
{
    public partial class Form2add : Form
    {
        public string dbconnect =
            "Data Source=DESKTOP-432U1GM\\SQLEXPRESS;Initial Catalog=OOP.Course31;Integrated Security=True;MultipleActiveResultSets=True\r\n";

        public Form2add()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = textBox1.Text;
            try
            {
                SqlConnection sconn = new SqlConnection(dbconnect);
                sconn.Open();
                SqlCommand command = new SqlCommand(query, sconn);

                int result = command.ExecuteNonQuery();

                if (result < 0)
                    MessageBox.Show("Ошибка добавления строки в базу данных! " + result.ToString());

                sconn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }
    }
}
