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
    public partial class frmHesab : Form
    {
        public frmHesab()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=HesabdariDB;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        private void frmHesab_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into Hesabha (SahebHesab,NameHesab,ShomareHesab,NameBank,Mojodi,Tozih) Values (@a,@b,@c,@d,@e,@f)";
                cmd.Parameters.AddWithValue("@a", txtSahebHesab.Text);
                cmd.Parameters.AddWithValue("@b", txtNameHesab.Text);
                cmd.Parameters.AddWithValue("@c", txtShomarehesab.Text);
                cmd.Parameters.AddWithValue("@d", txtNameBank.Text);
                cmd.Parameters.AddWithValue("@e", txtMojodi.Text);
                cmd.Parameters.AddWithValue("@f", txtTozih.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBoxFarsi.Show("عملیات با موفقیت انجام شد.", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
             }
             catch (Exception)
             {
                MessageBoxFarsi.Show("خطا در انجام عملیات!!", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
             }
}

        private void btnEdite_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "Update Hesabha set SahebHesab=N'"+txtSahebHesab.Text+"',NameHesab=N'"+txtNameHesab.Text+"',ShomareHesab='"+txtShomarehesab.Text+"',NameBank=N'"+txtNameBank.Text+"',Mojodi='"+txtMojodi.Text+"',Tozih=N'"+txtTozih.Text+"' where IdHesab="+txtId.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBoxFarsi.Show("عملیات با موفقیت انجام شد.", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
            catch (Exception)
            {
                MessageBoxFarsi.Show("خطا در انجام عملیات!!", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
        }

        private void btnListHesab_Click(object sender, EventArgs e)
        {
            new frmListHesabha().ShowDialog();
        }
    }
}
