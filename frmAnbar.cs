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
    public partial class frmAnbar : Form
    {
        public frmAnbar()
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
            adp.SelectCommand.CommandText = "select * from Anbar";
            adp.Fill(ds, "Anbar");
            dgvAnbar.DataSource = ds;
            dgvAnbar.DataMember = "Anbar";
        }
        private void frmAnbar_Load(object sender, EventArgs e)
        {
            display();
            dgvAnbar.Columns[0].HeaderText = "کد";
            dgvAnbar.Columns[1].HeaderText = "نام انبار";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into Anbar (NameAnbar) values (@a)";
                cmd.Parameters.AddWithValue("@a", txtAnbar.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                display();
                MessageBoxFarsi.Show("عملیات با موفقیت انجام شد.","پیغام",MessageBoxFarsiButtons.OK,MessageBoxFarsiIcon.Information,MessageBoxFarsiDefaultButton.Button1);
            }
            catch (Exception)
            {
                MessageBoxFarsi.Show("خطا در انجام عملیات!!","پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
         
            try
            {
                int x = (int)dgvAnbar.SelectedCells[0].Value;
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "delete from Anbar where IdAnbar=@i";
                cmd.Parameters.AddWithValue("@i", txtId.Text);
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

        private void dgvAnbar_MouseUp(object sender, MouseEventArgs e)
        {
            txtId.Text = dgvAnbar[0, dgvAnbar.CurrentRow.Index].Value.ToString();
            txtAnbar.Text = dgvAnbar[1, dgvAnbar.CurrentRow.Index].Value.ToString();
        }
    }
}
