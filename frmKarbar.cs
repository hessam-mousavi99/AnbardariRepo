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
    public partial class frmKarbar : Form
    {
        public frmKarbar()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=HesabdariDB;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        void display()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Karbar";
            adp.Fill(ds, "Karbar");
            dgvkarbar.DataSource = ds;
            dgvkarbar.DataMember = "Karbar";
        }
        private void frmKarbar_Load(object sender, EventArgs e)
        {
            display();
            dgvkarbar.Columns[0].HeaderText = "کد";
            dgvkarbar.Columns[1].HeaderText = "نام کاربری";
            dgvkarbar.Columns[2].HeaderText = "رمز عبور";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            { cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "insert into Karbar (UserName,Password) values(@a,@b)";
            cmd.Parameters.AddWithValue("@a", txtUser.Text);
            cmd.Parameters.AddWithValue("@b",txtPassword.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            
            display();
                MessageBoxFarsi.Show("عملیات با موفقیت انجام شد.", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
            catch (Exception)
            {
                MessageBoxFarsi.Show("خطا در انجام عملیات!!", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
           
            try
            { int x = Convert.ToInt32(dgvkarbar.SelectedCells[0].Value);
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "delete from Karbar where IdKarbar=@n";
            cmd.Parameters.AddWithValue("@n", txtID.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            display();
                MessageBoxFarsi.Show("عملیات با موفقیت انجام شد.", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
            catch (Exception)
            {
                MessageBoxFarsi.Show("خطا در انجام عملیات!!", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }

        }

        private void dgvkarbar_MouseUp(object sender, MouseEventArgs e)
        {
            txtID.Text = dgvkarbar[0, dgvkarbar.CurrentRow.Index].Value.ToString();
            txtUser.Text = dgvkarbar[1, dgvkarbar.CurrentRow.Index].Value.ToString();
            txtPassword.Text = dgvkarbar[2, dgvkarbar.CurrentRow.Index].Value.ToString();
        }
    }
}
