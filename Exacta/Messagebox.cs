using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exacta
{
    public partial class Messagebox : Form
    {
        public Messagebox()
        {
            InitializeComponent();
        }

        private void btn_yes_Click(object sender, EventArgs e)
        {   
           
            this.Close();
           //Exacta.Menu.Expiration();
        }

        private void btn_no_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
    }
}
