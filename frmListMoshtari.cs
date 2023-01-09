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
    public partial class frmListMoshtari : Form
    {
        public frmListMoshtari()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=HesabdariDB;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        void display()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand();
            da.SelectCommand.Connection = con;
            da.SelectCommand.CommandText = "Select * from Moshtari";
            da.Fill(ds, "Moshtari");
            dgvListMoshtari.DataSource = ds;
            dgvListMoshtari.DataMember = "Moshtari";
        }
        private void frmListMoshtari_Load(object sender, EventArgs e)
        {
            try
            {
                display();
                dgvListMoshtari.Columns[0].HeaderText = "کد";
                dgvListMoshtari.Columns[1].HeaderText = "نام مشتری";
                dgvListMoshtari.Columns[2].HeaderText = "نوع مشتری";
                dgvListMoshtari.Columns[3].HeaderText = "تلفن";
                dgvListMoshtari.Columns[4].HeaderText = "تلفن همراه";
                dgvListMoshtari.Columns[5].HeaderText = "بدهکار";
                dgvListMoshtari.Columns[6].HeaderText = "بستانکار";
            }
             catch (Exception)
            {
                MessageBoxFarsi.Show("مشکلی پیش آمده است!!", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int x = (int)dgvListMoshtari.SelectedCells[0].Value;
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "delete from Moshtari where IdMoshtari=@i";
                cmd.Parameters.AddWithValue("i", txtIDMoshtari.Text);//az x hm mishavad estefde krd.
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                display();
                MessageBoxFarsi.Show("عملیات با موفقیت انجام شد.", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
            catch (Exception)
            {
                MessageBoxFarsi.Show("مشکلی پیش آمده است!!", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
        }

        private void dgvListMoshtari_MouseUp(object sender, MouseEventArgs e)
        {
            txtIDMoshtari.Text = dgvListMoshtari[0, dgvListMoshtari.CurrentRow.Index].Value.ToString();
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            frmMoshtari frm = new frmMoshtari();
            frm.txtId.Text = dgvListMoshtari[0, dgvListMoshtari.CurrentRow.Index].Value.ToString();
            frm.txtNameMoshtari.Text = dgvListMoshtari[1, dgvListMoshtari.CurrentRow.Index].Value.ToString();
            frm.txtNoeMoshtari.Text = dgvListMoshtari[2, dgvListMoshtari.CurrentRow.Index].Value.ToString();
            frm.txtTel.Text = dgvListMoshtari[3, dgvListMoshtari.CurrentRow.Index].Value.ToString();
            frm.txtMobile.Text = dgvListMoshtari[4, dgvListMoshtari.CurrentRow.Index].Value.ToString();
            frm.txtBedehkar.Text = dgvListMoshtari[5, dgvListMoshtari.CurrentRow.Index].Value.ToString();
            frm.txtBestankar.Text = dgvListMoshtari[6, dgvListMoshtari.CurrentRow.Index].Value.ToString();
            frm.ShowDialog();
        }

        private void txtNameMoshtari_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand();
            da.SelectCommand.Connection = con;
            da.SelectCommand.CommandText = "select * from Moshtari where NameMoshtari Like '%'+ @n +'%'";
            da.SelectCommand.Parameters.AddWithValue("@n",txtNameMoshtari.Text+"%");
            da.Fill(ds, "Moshtari");
            dgvListMoshtari.DataSource = ds;
            dgvListMoshtari.DataMember = "Moshtari";
        }

        private void txtNoeMoshtari_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand();
            da.SelectCommand.Connection = con;
            da.SelectCommand.CommandText = "select * from Moshtari where NoeMoshtari Like '%'+ @n +'%'";
            da.SelectCommand.Parameters.AddWithValue("@n", txtNoeMoshtari.Text + "%");
            da.Fill(ds, "Moshtari");
            dgvListMoshtari.DataSource = ds;
            dgvListMoshtari.DataMember = "Moshtari";
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            display();
        }
    }
}
