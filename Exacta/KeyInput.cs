using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Exacta
{
    
    public partial class KeyInput : Form
    {
       // public static string conn = "data source=localhost\\SERVEREXACTA;initial catalog = Licence; User ID = sa; password=ExactaServer1122?";//Client inco zegna
        public static string conn = "data source=82.77.36.121,1433\\sqlexpress;initial catalog=Licence; User ID = sa; password=ExactaServer1122?;";//main sv licenta
        public KeyInput()
        {
            InitializeComponent();
        }
        public static string d1;
        public static string o1;
        public static string d2 = DateTime.Now.ToString();
        public static bool stop = false;
        public static int nr_luni;
        private void btn_Proceed_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Insert a product key !");
            }
            else
            {
                d1 = (textBox1.Text).ToString();
                var con = new SqlConnection(conn);
                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select N_Luni From Info where L_Key='" + d1 + "'";
                cmd.CommandType = CommandType.Text;
                con.Open();
                var dr = cmd.ExecuteReader();
                dr.Read();
                if(dr.HasRows)
                {  nr_luni = Convert.ToInt32(dr[0].ToString());
                    o1 = Exacta.Menu.Encrypt(d1);
                    Exacta.Properties.Settings.Default.o1 = o1;
                    Exacta.Properties.Settings.Default.d2 = d2;
                    Exacta.Properties.Settings.Default.o2 = nr_luni;
                    Exacta.Properties.Settings.Default.Save();
                    MessageBox.Show("Licence activated !");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("The Key is not valid !");
                    stop = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            stop = true;
            Application.ExitThread();
        }
    }
}
