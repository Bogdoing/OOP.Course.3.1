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
    public partial class FormInfo : Form
    {
        private LocomotivData ld = new LocomotivData();
        private DateTime dtstart;
        private DateTime dtend;
        public FormInfo(LocomotivData ld) // string infoLoc
        {
            InitializeComponent();

            textBox1.Text = ld.nld.NLD;
            char[] b = ld.nld.NLD.ToCharArray();

            string typeLoc = ld.nld.GetType(b);
            string rodSl = ld.nld.GetRodActiv(b);
            string numLoc = ld.nld.GetNum(b);
            
            ld.nld.TypeLoc = typeLoc;
            ld.nld.Pod_active = rodSl;
            ld.nld.Num_Loc = numLoc;
            
            textBox3.Text += b[1] + ld.nld.TypeLoc; //" Тип локомотива - " 
            textBox4.Text += b[2] + ld.nld.Pod_active; //" Род службы - " 
            textBox5.Text += ld.nld.Num_Loc; 
            
            //
            
            string[] start = ld.StartTime.Split(new char[] { ' ' }); // 0 - date / 1 - timestart
            string[] end = ld.EndTime.Split(new char[] { ' ' }); // 0 - date / 1 - timeend
            string startTime = start[1];
            string endTime = end[1];
            

            var Sstart = start[0].Split(new char[] { '.' });
            var Send = end[0].Split(new char[] { '.' });
            string dateccS = Sstart[2] + "-" + Sstart[1] + "-" + Sstart[0];
            string dateccE = Send[2] + "-" + Send[1] + "-" + Send[0];

            try
            {
                // "2022-12-17 14:40:52", "yyyy-MM-dd HH:mm:ss"
                dtstart = DateTime.ParseExact(dateccS + " 14:40:52", "yyyy-MM-dd HH:mm:ss",
                    System.Globalization.CultureInfo.InvariantCulture);
                dtend = DateTime.ParseExact(dateccE + " 14:40:52", "yyyy-MM-dd HH:mm:ss",
                    System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            textBox2.Text = ld.StartTime;
            textBox6.Text = ld.EndTime;
            textBox7.Text = dtend.Subtract(dtstart).ToString();

        }
    }
}
