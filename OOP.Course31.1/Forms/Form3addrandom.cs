using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP.Course31._1.Forms
{
    public partial class Form3addrandom : Form
    {
        public Form3addrandom()
        {
            InitializeComponent();
        }

        Random random = new Random();

        private void button1_Click(object sender, EventArgs e)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const string chars_num = "0123456789";
            
            int id = (int)numericUpDown1.Value;
            string num_lok = "";
//            string type_ore = "";
            string[] type_ore = { "Гранит", "Базальт", "Железная руда", "Марганцевая руда",
                            "Асбестовая руда", "Апатитовая руда", "Медная руда", "Никелевая руда"};
            int count_ore = 0;
            string time_start = "";
            string time_end = "";

            for (int i = 0; i < id; i++)
            {
                num_lok = new string(Enumerable.Repeat(chars_num, 6)
                        .Select(s => s[random.Next(s.Length)]).ToArray());
                num_lok = "1" + num_lok;

                int type_ore_index = random.Next(0, 7);
                count_ore = random.Next(0, 300);
                
                int mount = random.Next(1, 12);
                string mount_str_start = "";
                string mount_str_end = "";

                if (mount < 9) 
                { 
                    mount_str_start = "0" + mount; 
                    int mount_end = (int.Parse(mount_str_start) + 1);
                    mount_str_end = "0" + mount_end;
                }
                else if (mount == 9) 
                { 
                    mount_str_start = "" + mount;
                    int mount_end = (int.Parse(mount_str_start) + 1);
                    mount_str_end = ""+mount_end;
                }
                else 
                { 
                    mount_str_start = "" + mount;
                    mount_str_end = mount_str_start;
                }
                //SMALLDATETIME: хранит даты и время в диапазоне от 01/01/1900 до 06/06/2079, то есть ближайшие даты. Занимает от 4 байта.
                int day = random.Next(10, 31);
                if (day > 28)
                {
                    if ((int.Parse(mount_str_start) == 2) )
                    {
                        mount_str_start = ""+((int.Parse(mount_str_start) + 1));
                    }
                    else if ((int.Parse(mount_str_end) == 2))
                    {
                        mount_str_end= "" + ((int.Parse(mount_str_end) + 1));
                    }
                }

                int time = random.Next(2020, 2030);
                int tend = time + random.Next(1, 5);
                time_start = "" + day + "/" + mount_str_start + "/" + time;
                time_end = "" + day + "/" + mount_str_end + "/" + tend;


                textBox1.Text += "INSERT INTO dbo.OOPLocomotive VALUES( '" + num_lok + "', '" + type_ore[type_ore_index] + "', '" +     
                    count_ore + "', '" + time_start + "', '" + time_end + "', '" + random.Next(50000, 7000000) + "', '" + "TestClient" +"')\r\n";

                //INSERT INTO dbo.lokomotive VALUES('1230', 'QWE', 123.1, 01/01/1905, 01/01/2000)
            }
        }
    }
}
