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
    public partial class frmGroup : Form
    {
        public frmGroup()
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
            adp.SelectCommand.CommandText = "select * from Grooh";
            adp.Fill(ds, "Grooh");
            dgvGroup.DataSource = ds;
            dgvGroup.DataMember = "Grooh";
        }
        private void frmGroup_Load(object sender, EventArgs e)
        {
            display();
            dgvGroup.Columns[0].HeaderText = "کد";
            dgvGroup.Columns[1].HeaderText = "نام گروه";
        }

        private void dataGridViewX1_MouseUp(object sender, MouseEventArgs e)
        {
            txtId.Text = dgvGroup[0, dgvGroup.CurrentRow.Index].Value.ToString();
            txtgroup.Text = dgvGroup[1, dgvGroup.CurrentRow.Index].Value.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into Grooh (NameGrooh) values (@a)";
                cmd.Parameters.AddWithValue("@a", txtgroup.Text);
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

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {int x = (int)dgvGroup.SelectedCells[0].Value;
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "delete from Grooh where IdGrooh=@i";
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
    }
}
