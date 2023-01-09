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
    public partial class frmKala : Form
    {
        public frmKala()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=HesabdariDB;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
       
        private void frmKala_Load(object sender, EventArgs e)
        {
           
            DataTable dt = new DataTable();
            DataBase db = new DataBase();
            dt = db.MySelect("select * from Grooh");
            comboGroohKala.DataSource = dt;
            comboGroohKala.DisplayMember = "NameGrooh";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            { cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "insert into Kala (NameGrooh,NameKala,GheymatKharid,GheymatForosh,Tedad,Vahed) Values (@a,@b,@c,@d,@e,@f)";
            cmd.Parameters.AddWithValue("@a",comboGroohKala.Text);
            cmd.Parameters.AddWithValue("@b",txtNameKala.Text);
            cmd.Parameters.AddWithValue("@c",txtKharid.Text);
            cmd.Parameters.AddWithValue("@d",txtForosh.Text);
            cmd.Parameters.AddWithValue("@e",txtTedad.Text);
            cmd.Parameters.AddWithValue("@f",txtVahed.Text);
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

        private void btnListKala_Click(object sender, EventArgs e)
        {
            new frmListKala().ShowDialog();
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            try
            { cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "update Kala set NameGrooh=N'"+comboGroohKala.Text+"',NameKala=N'"+txtNameKala.Text+"',GheymatKharid=N'"+txtKharid.Text+"',GheymatForosh=N'"+txtForosh.Text+"',Tedad=N'"+txtTedad.Text+"',Vahed=N'"+txtVahed.Text+"' where IdKala="+txtID.Text;
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

        private void btnControlKala_Click(object sender, EventArgs e)
        {
            new frmControlKala().ShowDialog();
        }
    }
}
