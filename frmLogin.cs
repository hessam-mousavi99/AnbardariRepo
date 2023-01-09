using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Anbardari
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=HesabdariDB;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        private void btnSignin_Click(object sender, EventArgs e)
        {
            int i = 0;
            cmd = new SqlCommand("select  count(*) from Karbar where UserName=@u and Password=@p ",con);
            cmd.Parameters.AddWithValue("@u", txtUser.Text);
            cmd.Parameters.AddWithValue("@p", txtPassword.Text);
            con.Open();
            i = (int)cmd.ExecuteScalar();
            con.Close();
            if (i>0)
            {
                new Form1().ShowDialog();
            }
            else
            {
                MessageBox.Show("کاربری با این مشخصات یافت نشد.");
            }

        }

        private void btnSignout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
