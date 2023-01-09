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
using Stimulsoft.Report;

namespace Anbardari
{
    public partial class frmListHesabha : Form
    {
        public frmListHesabha()
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
           // da.SelectCommand.Parameters.Clear();
            da.SelectCommand.CommandText = "select * from Hesabha";
            da.Fill(ds, "Hesabha");
            dgvHesab.DataSource = ds;
            dgvHesab.DataMember = "Hesabha";
        }
        private void frmListHesabha_Load(object sender, EventArgs e)
        {
            display();
            dgvHesab.Columns[0].HeaderText = "کد";
            dgvHesab.Columns[1].HeaderText = "صاحب حساب";
            dgvHesab.Columns[2].HeaderText = "نام حساب";
            dgvHesab.Columns[3].HeaderText = "شماره حساب";
            dgvHesab.Columns[4].HeaderText = "نام بانک";
            dgvHesab.Columns[5].HeaderText = "موجودی";
            dgvHesab.Columns[6].HeaderText = "توضیح";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int x = Convert.ToInt32(dgvHesab.SelectedCells[0].Value);
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "delete from Hesabha where IdHesab=@i";
                cmd.Parameters.AddWithValue("@i", txtIDHesab.Text);
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

        private void dgvHesab_MouseUp(object sender, MouseEventArgs e)
        {
            txtIDHesab.Text = dgvHesab[0, dgvHesab.CurrentRow.Index].Value.ToString();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            display();
        }

        private void txtShomareHesab_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand();
            da.SelectCommand.Connection = con;
            da.SelectCommand.CommandText = "select * from Hesabha where ShomareHesab Like '%'+ @s +'%'";
            da.SelectCommand.Parameters.AddWithValue("@s", txtShomareHesab.Text + "%");
            da.Fill(ds, "Hesabha");
            dgvHesab.DataSource = ds;
            dgvHesab.DataMember = "Hesabha";
        }

        private void txtNameHesab_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand();
            da.SelectCommand.Connection = con;
            da.SelectCommand.CommandText = "select * from Hesabha where NameHesab Like '%'+ @s +'%'";
            da.SelectCommand.Parameters.AddWithValue("@s", txtNameHesab.Text + "%");
            da.Fill(ds, "Hesabha");
            dgvHesab.DataSource = ds;
            dgvHesab.DataMember = "Hesabha";
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            frmHesab frm = new frmHesab();
            frm.txtId.Text = dgvHesab[0, dgvHesab.CurrentRow.Index].Value.ToString();
            frm.txtSahebHesab.Text = dgvHesab[1, dgvHesab.CurrentRow.Index].Value.ToString();
            frm.txtNameHesab.Text = dgvHesab[2, dgvHesab.CurrentRow.Index].Value.ToString();
            frm.txtShomarehesab.Text = dgvHesab[3, dgvHesab.CurrentRow.Index].Value.ToString();
            frm.txtNameBank.Text = dgvHesab[4, dgvHesab.CurrentRow.Index].Value.ToString();
            frm.txtMojodi.Text = dgvHesab[5, dgvHesab.CurrentRow.Index].Value.ToString();
            frm.txtTozih.Text = dgvHesab[6, dgvHesab.CurrentRow.Index].Value.ToString();
            frm.ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report1 = new StiReport();
            Report1.Load("Report/ReportHesabHa.mrt");
            Report1.Compile();
            Report1.ShowWithRibbonGUI();
        }
    }
}
