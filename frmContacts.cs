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
    public partial class frmContacts : Form
    {
        public frmContacts()
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
            adp.SelectCommand.CommandText = "select * from Contacts";
            adp.Fill(ds, "Contacts");
            dgvContacts.DataSource = ds;
            dgvContacts.DataMember = "Contacts";
        }
        void displayContactsbyName()
        {
            con.Open();
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = con;
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandText = "FilterContactsByName";
                da.SelectCommand.Parameters.AddWithValue("@s", txtFilterName.Text + "%");
                da.Fill(ds, "Contacts");
                dgvContacts.DataSource = ds;
                dgvContacts.DataMember = "Contacts";
            }
            con.Close();
        }
        void displayContactsbyMobile()
        {
            con.Open();
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = con;
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandText = "FilterContactsByMobile";
                da.SelectCommand.Parameters.AddWithValue("@s", txtFilterMobile.Text + "%");
                da.Fill(ds, "Contacts");
                dgvContacts.DataSource = ds;
                dgvContacts.DataMember = "Contacts";
            }
            con.Close();
        }
        private void frmContacts_Load(object sender, EventArgs e)
        {
            display();
            dgvContacts.Columns[0].HeaderText = "کد";
            dgvContacts.Columns[1].HeaderText = "نام و نام خانوادگی";
            dgvContacts.Columns[2].HeaderText = "تلفن ثابت";
            dgvContacts.Columns[3].HeaderText = "تلفن همراه";
            dgvContacts.Columns[4].HeaderText = "آدرس";
            dgvContacts.Columns[4].Width = 220;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into Contacts (NameContact,Tel,Mobile,Address) values (@a,@b,@c,@d)";
                cmd.Parameters.AddWithValue("@a", txtName.Text);
                cmd.Parameters.AddWithValue("@b", txtTel.Text);
                cmd.Parameters.AddWithValue("@c", txtMobile.Text);
                cmd.Parameters.AddWithValue("@d", txtAddress.Text);
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

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                {
                    int x = (int)dgvContacts.SelectedCells[0].Value;
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "DeleteContact";
                    cmd.Parameters.AddWithValue("@Id", x);
                    cmd.ExecuteNonQuery();
                    display();
                }
                con.Close();
                MessageBoxFarsi.Show("عملیات با موفقیت انجام شد.", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
            catch (Exception)
            {
                MessageBoxFarsi.Show("خطا در انجام عملیات!!", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "UpdateContact";
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Tel", txtTel.Text);
                    cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@Id", txtId.Text);
                    cmd.ExecuteNonQuery();
                    display();
                }
                con.Close();
             
                MessageBoxFarsi.Show("عملیات با موفقیت انجام شد.", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
            catch (Exception)
            {
                MessageBoxFarsi.Show("خطا در انجام عملیات!!", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
        }

        private void txtFilterName_TextChanged(object sender, EventArgs e)
        {
            displayContactsbyName();
        }

        private void txtFilterMobile_TextChanged(object sender, EventArgs e)
        {
            displayContactsbyMobile();
        }

        private void dgvContacts_MouseUp(object sender, MouseEventArgs e)
        {
            txtId.Text = dgvContacts[0, dgvContacts.CurrentRow.Index].Value.ToString();
            txtName.Text = dgvContacts[1, dgvContacts.CurrentRow.Index].Value.ToString();
            txtTel.Text = dgvContacts[2, dgvContacts.CurrentRow.Index].Value.ToString();
            txtMobile.Text = dgvContacts[3, dgvContacts.CurrentRow.Index].Value.ToString();
            txtAddress.Text = dgvContacts[4, dgvContacts.CurrentRow.Index].Value.ToString();
        }
    }
}
