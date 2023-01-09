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
    public partial class frmPardakht : Form
    {
        public frmPardakht()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=HesabdariDB;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        private void frmPardakht_Load(object sender, EventArgs e)
        {

        }

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
                if (txtMablagh.Value>Convert.ToInt32(s))
                {
                    MessageBoxFarsi.Show("مقدار پرداختی  بدهی از موجودی شما بیشتر است.", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
                }
                else
                {
                    sum += Convert.ToInt32(s) - x;
                    string UpdateQuery = "Update Hesabha set Mojodi='" + sum + "' where ShomareHesab='" + txtShomareHesab.Text + "'";
                    SqlCommand com = new SqlCommand(UpdateQuery, con);
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.CommandText = "insert into PardakhtAzHesab (ShomareHesab,NameHesab,NameMoshtari,Mablagh,TarikhPardakht,Tozih) values(@a,@b,@c,@d,@e,@f)";
                    cmd.Parameters.AddWithValue("@a", txtShomareHesab.Text);
                    cmd.Parameters.AddWithValue("@b", txtNameHesab.Text);
                    cmd.Parameters.AddWithValue("@c", txtNamePardakhtkonnde.Text);
                    cmd.Parameters.AddWithValue("@d", txtMablagh.Text);
                    cmd.Parameters.AddWithValue("@e", txtTarikh.Text);
                    cmd.Parameters.AddWithValue("@f", txtTozih.Text);
                    cmd.ExecuteNonQuery();
                    com.ExecuteNonQuery();
                    MessageBoxFarsi.Show("عملیات با موفقیت انجام شد.", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
                }
                con.Close();
            }
            catch (Exception)
            {
                MessageBoxFarsi.Show("خطا در انجام عملیات!!", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
        }

        private void btnGozaresh_Click(object sender, EventArgs e)
        {
            new frmGozareshPD().ShowDialog();
        }
    }
}
