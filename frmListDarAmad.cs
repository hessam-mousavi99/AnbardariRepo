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
    public partial class frmListDarAmad : Form
    {
        public frmListDarAmad()
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
            da.SelectCommand.CommandText = "select * from DarAmad";
            da.Fill(ds, "DarAmad");
            dgvListDarAmad.DataSource = ds;
            dgvListDarAmad.DataMember = "DarAmad";
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int x = Convert.ToInt32(dgvListDarAmad.SelectedCells[0].Value);
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "delete from DarAmad where IdDarAmad=@I";
                cmd.Parameters.AddWithValue("@I", x);
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

        private void frmListDarAmad_Load(object sender, EventArgs e)
        {
            display();
            dgvListDarAmad.Columns[0].HeaderText = "کد";
            dgvListDarAmad.Columns[1].HeaderText = "نام درآمد";
            dgvListDarAmad.Columns[2].HeaderText = "شماره حساب";
            dgvListDarAmad.Columns[3].HeaderText = "نام حساب";
            dgvListDarAmad.Columns[4].HeaderText = "تاریخ ثبت";
            dgvListDarAmad.Columns[5].HeaderText = "مبلغ";
            dgvListDarAmad.Columns[6].HeaderText = "توضیخ";
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            frmDarAmad frm = new frmDarAmad();
            frm.txtID.Text = dgvListDarAmad[0, dgvListDarAmad.CurrentRow.Index].Value.ToString();
            frm.txtNameDarAmad.Text = dgvListDarAmad[1, dgvListDarAmad.CurrentRow.Index].Value.ToString();
            frm.txtShomareHesab.Text = dgvListDarAmad[2, dgvListDarAmad.CurrentRow.Index].Value.ToString();
            frm.txtNameHesab.Text = dgvListDarAmad[3, dgvListDarAmad.CurrentRow.Index].Value.ToString();
            frm.txtTarikhSabt.Text = dgvListDarAmad[4, dgvListDarAmad.CurrentRow.Index].Value.ToString();
            frm.txtMablagh.Text = dgvListDarAmad[5, dgvListDarAmad.CurrentRow.Index].Value.ToString();
            frm.txtTozih.Text = dgvListDarAmad[6, dgvListDarAmad.CurrentRow.Index].Value.ToString();
            frm.ShowDialog();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            display();
        }

        private void txtNameHazine_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand();
            da.SelectCommand.Connection = con;
            da.SelectCommand.CommandText = "select * from DarAmad where NameDarAmad Like '%' + @S +'%'";
            da.SelectCommand.Parameters.AddWithValue("@S", txtNameDarAmad.Text + "%");
            da.Fill(ds, "DarAmad");
            dgvListDarAmad.DataSource = ds;
            dgvListDarAmad.DataMember = "DarAmad";
        }
    }
}
