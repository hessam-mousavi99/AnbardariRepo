using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anbardari
{
    public partial class FrmLoading : Form
    {
        public FrmLoading()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CP.Value += 10;
            if (CP.Value==100)
            {
                timer1.Stop();
                new frmLogin().ShowDialog();
                this.Close();
            }
           
        }
    }
}
