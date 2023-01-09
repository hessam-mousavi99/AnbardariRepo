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
    public partial class frmDarAmad : Form
    {
        public frmDarAmad()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=HesabdariDB;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string s;
                int x;
                long sum = 0;
                con.Open();
                SqlCommand sc = new SqlCommand("select Mojodi from Hesabha where ShomareHesab='" + txtShomareHesab.Text + "'", con);
                s = Convert.ToString(sc.ExecuteScalar());
                x = Convert.ToInt32(txtMablagh.Text);
                sum += Convert.ToInt32(s) + x;
                string UpdateQuery = "Update Hesabha set Mojodi='" + sum + "' where ShomareHesab='" + txtShomareHesab.Text + "'";
                SqlCommand com = new SqlCommand(UpdateQuery, con);
                com.ExecuteNonQuery();
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into DarAmad (NameDarAmad,ShomareHesab,NameHesab,TarikhSabt,Mablagh,Tozih) values (@a,@b,@c,@d,@e,@f)";
                cmd.Parameters.AddWithValue("@a", txtNameDarAmad.Text);
                cmd.Parameters.AddWithValue("@b", txtShomareHesab.Text);
                cmd.Parameters.AddWithValue("@c", txtNameHesab.Text);
                cmd.Parameters.AddWithValue("@d", txtTarikhSabt.Text);
                cmd.Parameters.AddWithValue("@e", txtMablagh.Text);
                cmd.Parameters.AddWithValue("@f", txtTozih.Text);
                cmd.ExecuteNonQuery();
                MessageBoxFarsi.Show("عملیات با موفقیت انجام شد.", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
                con.Close();
            }
            catch (Exception)
            {
                MessageBoxFarsi.Show("خطا در انجام عملیات!!", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
        }

        private void frmDarAmad_Load(object sender, EventArgs e)
        {

        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "Update DarAmad set NameDarAmad=N'" + txtNameDarAmad.Text + "',ShomareHesab='" + txtShomareHesab.Text + "',NameHesab=N'" + txtNameHesab.Text + "',TarikhSabt='" + txtTarikhSabt.Text + "',Mablagh='" + txtMablagh.Text + "',Tozih=N'" + txtTozih.Text + "' where IdDarAmad=" + txtID.Text;
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

        private void btnListDarAmad_Click(object sender, EventArgs e)
        {
            new frmListDarAmad().ShowDialog();
        }
    }
}
