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
    public partial class ChekPardakhti : Form
    {
        public ChekPardakhti()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=HesabdariDB;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        private void ChekPardakhti_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            txtTarikhSabt.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
            txtTarikhsarresidChek.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into ChekPardakhti (ShomareHesab,NameHesab,ShomareSanad,Mablagh,TarikhSabt,SarResid,NameMoshtari,Vaziat,Tozih) values (@a,@b,@c,@d,@e,@f,@g,@h,@i)";
                cmd.Parameters.AddWithValue("@a", txtShomareHesab.Text);
                cmd.Parameters.AddWithValue("@b", txtNameHesab.Text);
                cmd.Parameters.AddWithValue("@c", txtShomareSanad.Text);
                cmd.Parameters.AddWithValue("@d", txtMablagh.Text);
                cmd.Parameters.AddWithValue("@e", txtTarikhSabt.Text);
                cmd.Parameters.AddWithValue("@f", txtTarikhsarresidChek.Text);
                cmd.Parameters.AddWithValue("@g", txtPardakhtBe.Text);
                cmd.Parameters.AddWithValue("@h", cmbVaziat.Text);
                cmd.Parameters.AddWithValue("@i", txtTozih.Text);
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

        private void btnListChekha_Click(object sender, EventArgs e)
        {
            new frmListChekP().ShowDialog();
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "update ChekPardakhti set ShomareHesab='" + txtShomareHesab.Text + "',NameHesab=N'" + txtNameHesab.Text + "',ShomareSanad='" + txtShomareSanad.Text + "',Mablagh='" + txtMablagh.Text + "',TarikhSabt='" + txtTarikhSabt.Text + "',SarResid='" + txtTarikhsarresidChek.Text + "',NameMoshtari=N'" + txtPardakhtBe.Text + "',Vaziat=N'" + cmbVaziat.Text + "',Tozih=N'" + txtTozih.Text + "' where IdChek=" + txtId.Text;
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
    }
}
