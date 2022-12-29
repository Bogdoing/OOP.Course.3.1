using OOP.Course31._1.Forms;
using System.Data;
using System.Data.SqlClient;

namespace OOP.Course31._1
{
    public partial class Form1 : Form
    {
        public string dbconnect =
            "Data Source=DESKTOP-432U1GM\\SQLEXPRESS;Initial Catalog=OOP.Course31;Integrated Security=True;MultipleActiveResultSets=True\r\n";

        private LocomotivData ld = new LocomotivData();
        
        private string infoLoc = "";

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

        private void frmSport_Shown(object sender, EventArgs e)
        {
            string squery = @"select distinct Num_lok from OOPLocomotive
                                union all (select '' as Num_lok)
                                order by Num_lok";
            try
            {
                SqlConnection sconn = new SqlConnection(dbconnect);
                SqlCommand scomm = new SqlCommand(squery, sconn);
                DataTable stable = new DataTable();
                SqlDataAdapter sadapter = new SqlDataAdapter(scomm);
                sadapter.Fill(stable);
                comboBox1.DataSource = stable;
                comboBox1.DisplayMember = "Num_lok";
                sconn.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "|error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            squery = @"select distinct Type_ore from OOPLocomotive
                        union all (select '' as Type_ore)
                        order by Type_ore";
            try
            {
                SqlConnection sconn = new SqlConnection(dbconnect);
                SqlCommand scomm = new SqlCommand(squery, sconn);
                DataTable stable = new DataTable();
                SqlDataAdapter sadapter = new SqlDataAdapter(scomm);
                sadapter.Fill(stable);
                comboBox2.DataSource = stable;
                comboBox2.DisplayMember = "Type_ore";
                sconn.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "|error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            squery = @"select * from OOPLocomotive";
            try
            {
                SqlConnection sconn = new SqlConnection(dbconnect);
                SqlCommand scomm = new SqlCommand(squery, sconn);
                DataTable stable = new DataTable();
                SqlDataAdapter sadapter = new SqlDataAdapter(scomm);
                sadapter.Fill(stable);
                dataGridView1.DataSource = stable;
                //dataGridView1.Columns[0].Visible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.LightBlue;
                // dataGridView1.Columns[3].DefaultCellStyle.BackColor = Color.LightBlue;
                sconn.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "|error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            squery = @"select * from OOPLocomotive l WHERE (l.EndTime > GETDATE())";
            try
            {
                SqlConnection sconn = new SqlConnection(dbconnect);
                SqlCommand scomm = new SqlCommand(squery, sconn);
                DataTable stable = new DataTable();
                SqlDataAdapter sadapter = new SqlDataAdapter(scomm);
                sadapter.Fill(stable);
                dataGridView2.DataSource = stable;
                //dataGridView1.Columns[0].Visible = false;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.Columns[1].DefaultCellStyle.BackColor = Color.LightBlue;
                //dataGridView2.Columns[3].DefaultCellStyle.BackColor = Color.LightBlue;

                sconn.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "|error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            squery = @"select * from OOPLocomotive l WHERE (l.EndTime < GETDATE())";
            try
            {
                SqlConnection sconn = new SqlConnection(dbconnect);
                SqlCommand scomm = new SqlCommand(squery, sconn);
                DataTable stable = new DataTable();
                SqlDataAdapter sadapter = new SqlDataAdapter(scomm);
                sadapter.Fill(stable);
                dataGridView3.DataSource = stable;
                //dataGridView1.Columns[0].Visible = false;
                dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView3.Columns[1].DefaultCellStyle.BackColor = Color.LightBlue;
                //dataGridView3.Columns[3].DefaultCellStyle.BackColor = Color.LightBlue;
                sconn.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "|error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2add form2Add = new Form2add();
            form2Add.ShowDialog();
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

        private void button4_Click(object sender, EventArgs e)
        {
            string query = "";
            if (comboBox1.Text == "") // - 
            {
                if (comboBox2.Text == "")
                    query = @"select * from OOPLocomotive";
                else
                    query = @"select * from OOPLocomotive where (Type_ore = '" + comboBox2.Text + "')";
            }
            else if (comboBox2.Text == "")
            {
                if (comboBox1.Text == "")
                    query = @"select * from OOPLocomotive";
                else
                    query = @"select * from OOPLocomotive where (Num_lok = '" + comboBox1.Text + "')";
            }
            else frmSport_Shown(sender, e); // Show default

            try
            {
                SqlConnection conn = new SqlConnection(dbconnect);
                SqlCommand comm = new SqlCommand(query, conn);
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(comm);
                adapter.Fill(table);

                if (tabControl1.SelectedIndex == 0)
                    dataGridView1.DataSource = table;
                else if (tabControl1.Text == "Ожидание")
                    dataGridView2.DataSource = table;
                else
                    dataGridView3.DataSource = table;

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e) // Просмотр инф.о локомотиве
        {
            try
            {
                FormInfo formInfo = new FormInfo(ld);
                formInfo.ShowDialog();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            button3.Text = "Просмотр информации";
            try
            {
                if (dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].HeaderText.ToString() ==
                    "Num_lok")
                {
                    ld.nld.NLD = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    //infoLoc = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    button3.Text = "Просмотр информации\r | " + ld.nld.NLD + " |";

                    ld.Id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    ld.Type_ore = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    ld.Count_ore = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    ld.StartTime = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    ld.EndTime = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    ld.Price = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    ld.Сlient = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    MessageBox.Show("cell content exp | " + ld.Id + " | " + ld.Type_ore + " | " + ld.Count_ore
                                    + " | " + ld.StartTime  + " | " + ld.EndTime  + " | " + ld.Price  + " | " + ld.Сlient);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("cell content exp | " + exception);
                throw;
            }
        }
    }
}