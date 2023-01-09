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
    public partial class frmMoshtari : Form
    {
        public frmMoshtari()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=HesabdariDB;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        private void frmMoshtari_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into Moshtari (NameMoshtari,NoeMoshtari,Tel,Mobile,Bedehkar,Bestankar) values (@a,@b,@c,@d,@e,@f)";
                cmd.Parameters.AddWithValue("@a", txtNameMoshtari.Text);
                cmd.Parameters.AddWithValue("@b", txtNoeMoshtari.Text);
                cmd.Parameters.AddWithValue("@c", txtTel.Text);
                cmd.Parameters.AddWithValue("@d", txtMobile.Text);
                cmd.Parameters.AddWithValue("@e", txtBedehkar.Text);
                cmd.Parameters.AddWithValue("@f", txtBestankar.Text);
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
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "update Moshtari set NameMoshtari=N'" + txtNameMoshtari.Text + "',NoeMoshtari=N'" + txtNoeMoshtari.Text + "',Tel=N'" + txtTel.Text + "',Mobile=N'" + txtMobile.Text + "',Bedehkar=N'" + txtBedehkar.Text + "',Bestankar=N'" + txtBestankar.Text + "' where IdMoshtari=" + txtId.Text;
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

        private void btnListmoshtari_Click(object sender, EventArgs e)
        {
            new frmListMoshtari().ShowDialog();
        }
    }
}
