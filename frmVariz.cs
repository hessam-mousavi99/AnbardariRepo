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
using BehComponents;

namespace Anbardari
{
    public partial class frmVariz : Form
    {
        public frmVariz()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=HesabdariDB;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into VarizBHesab (ShomareHesab,NameHesab,NameMoshtari,Mablagh,TarikhVariz,Tozih) values (@a,@b,@c,@d,@e,@F)";
                cmd.Parameters.AddWithValue("@a", txtShomareHesab.Text);
                cmd.Parameters.AddWithValue("@b", txtNameHesab.Text);
                cmd.Parameters.AddWithValue("@c", txtNameVarizkonnde.Text);
                cmd.Parameters.AddWithValue("@d", txtMablagh.Text);
                cmd.Parameters.AddWithValue("@e", txtTarikh.Text);
                cmd.Parameters.AddWithValue("@f", txtTozih.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                //con.Close();
                string str;
                int x;
                SqlCommand sc = new SqlCommand("select Mojodi from Hesabha where ShomareHesab='" + txtShomareHesab.Text + "'", con);
                str = Convert.ToString((sc.ExecuteScalar()));
                x = Convert.ToInt32(txtMablagh.Text);
                int sum = Int32.Parse(str) + x;
                string UpdateQuery = "Update Hesabha set Mojodi='" + sum + "' where ShomareHesab='" + txtShomareHesab.Text + "'";
                SqlCommand com = new SqlCommand(UpdateQuery, con);
                com.ExecuteNonQuery();
                con.Close();
                MessageBoxFarsi.Show("عملیات با موفقیت انجام شد.", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
            catch (Exception)
            {
                MessageBoxFarsi.Show("خطا در انجام عملیات!!", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
}

        private void frmVariz_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            new frmGozareshPD().ShowDialog();
        }
    }
}
