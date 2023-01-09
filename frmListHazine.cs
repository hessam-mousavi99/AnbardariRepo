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
    public partial class frmListHazine : Form
    {
        public frmListHazine()
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
            da.SelectCommand.CommandText = "select * from Hazine";
            da.Fill(ds,"Hazine");
            dgvListHazine.DataSource = ds;
            dgvListHazine.DataMember = "Hazine";
        }
        private void frmListHazine_Load(object sender, EventArgs e)
        {
            display();
            dgvListHazine.Columns[0].HeaderText = "کد";
            dgvListHazine.Columns[1].HeaderText = "نام هزینه";
            dgvListHazine.Columns[2].HeaderText = "شماره حساب";
            dgvListHazine.Columns[3].HeaderText = "نام حساب";
            dgvListHazine.Columns[4].HeaderText = "تاریخ ثبت";
            dgvListHazine.Columns[5].HeaderText = "مبلغ";
            dgvListHazine.Columns[6].HeaderText = "توضیخ";
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            frmHazine frm = new frmHazine();
            frm.txtID.Text = dgvListHazine[0, dgvListHazine.CurrentRow.Index].Value.ToString();
            frm.txtNameHazine.Text = dgvListHazine[1, dgvListHazine.CurrentRow.Index].Value.ToString();
            frm.txtShomareHesab.Text = dgvListHazine[2, dgvListHazine.CurrentRow.Index].Value.ToString();
            frm.txtNameHesab.Text = dgvListHazine[3, dgvListHazine.CurrentRow.Index].Value.ToString();
            frm.txtTarikhSabt.Text = dgvListHazine[4, dgvListHazine.CurrentRow.Index].Value.ToString();
            frm.txtMablagh.Text = dgvListHazine[5, dgvListHazine.CurrentRow.Index].Value.ToString();
            frm.txtTozih.Text = dgvListHazine[6, dgvListHazine.CurrentRow.Index].Value.ToString();
            frm.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int x = Convert.ToInt32(dgvListHazine.SelectedCells[0].Value);
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "delete from Hazine where IdHazine=@I";
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
            da.SelectCommand.CommandText = "select * from Hazine where NameHazine Like '%' + @S +'%'";
            da.SelectCommand.Parameters.AddWithValue("@S", txtNameHazine.Text + "%");
            da.Fill(ds, "Hazine");
            dgvListHazine.DataSource = ds;
            dgvListHazine.DataMember = "Hazine";
        }
    }
}
