

using OOP.Course31._1.Forms;
using System.Data;
using System.Data.SqlClient;

namespace OOP.Course31._1
{
    public partial class Form1 : Form
    {

        public string dbconnect = "Data Source=DESKTOP-432U1GM\\SQLEXPRESS;Initial Catalog=Sportsmans_BD_Kucherenko;Integrated Security=True;MultipleActiveResultSets=True\r\n";
        public Form1()
        {
            InitializeComponent();
        }
        protected override void OnResizeBegin(EventArgs e)
        {
            SuspendLayout();
            base.OnResizeBegin(e);
        }
        protected override void OnResizeEnd(EventArgs e)
        {
            ResumeLayout();
            base.OnResizeEnd(e);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //dataGridView1.Rows.Add(1, 128, "Гранит", 4);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void frmSport_Shown(object sender, EventArgs e)
        {
            string squery = "select rtrim(country) as country, id_country from strana union all (select '' as country, 0 as id_country) order by country";
            try
            {
                SqlConnection sconn = new SqlConnection(dbconnect);
                SqlCommand scomm = new SqlCommand(squery, sconn);
                DataTable stable = new DataTable();
                SqlDataAdapter sadapter = new SqlDataAdapter(scomm);
                sadapter.Fill(stable);
                //comboBox1.DataSource = stable;
                //comboBox1.DisplayMember = "country";
                //comboBox1.ValueMember = "id_country";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            string query = "select id_sportsman, rtrim(s.surname) as Фамилия, rtrim(s.kind_sport) as Вид_спорта, s.place as Место, rtrim(c.country) as Страна from sport s, strana c where (s.id_country=c.id_country)";
            try
            {
                SqlConnection conn = new SqlConnection(dbconnect);
                SqlCommand comm = new SqlCommand(query, conn);
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(comm);
                adapter.Fill(table);
                dataGridView4.DataSource = table;
                dataGridView4.Columns[0].Visible = false;
                dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {

            String query = "";
            if (comboBox1.Text == "")
                query = "select id_sportsman, rtrim(s.surname) as Фамилия, rtrim(s.kind_sport) as Вид_спорта, s.place as Место, rtrim(c.country) as Страна from sport s, strana c where (s.id_country=c.id_country)";
            else
                query = "select id_sportsman, rtrim(s.surname) as Фамилия, rtrim(s.kind_sport) as Вид_спорта, s.place as Место, rtrim(c.country) as Страна from sport s, strana c where (s.id_country=c.id_country) and (c.country='" + comboBox1.Text + "')";
            try
            {
                SqlConnection conn = new SqlConnection(dbconnect);
                SqlCommand comm = new SqlCommand(query, conn);
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(comm);
                adapter.Fill(table);
                dataGridView4.DataSource = table;
                dataGridView4.Columns[0].Visible = false;

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button_cansel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3addrandom form3Addrandom = new Form3addrandom();
            form3Addrandom.ShowDialog();
        }
    }
}