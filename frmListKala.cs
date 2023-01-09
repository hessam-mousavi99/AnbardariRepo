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
    public partial class frmListKala : Form
    {
        public frmListKala()
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
            da.SelectCommand.CommandText = "select * from Kala";
            da.Fill(ds,"Kala");
            dgvKala.DataSource = ds;
            dgvKala.DataMember = "Kala";
        }
        private void frmListKala_Load(object sender, EventArgs e)
        {
            display();
            dgvKala.Columns[0].HeaderText = "کد";
            dgvKala.Columns[1].HeaderText = "گروه کالا";
            dgvKala.Columns[2].HeaderText = "نام کالا";
            dgvKala.Columns[3].HeaderText = "قیمت خرید";
            dgvKala.Columns[4].HeaderText = "قیمت فروش";
            dgvKala.Columns[5].HeaderText = "تعداد";
            dgvKala.Columns[6].HeaderText = "واحد";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            { 
                int x = Convert.ToInt32(dgvKala.SelectedCells[0].Value);
                cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "delete from Kala Where IdKala=@n";
            cmd.Parameters.AddWithValue("@n", txtIDKala.Text);
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

        private void txtNameKala_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand();
            da.SelectCommand.Connection = con;
            da.SelectCommand.CommandText = "select * from Kala where NameKala Like '%' + @S +'%'";
            da.SelectCommand.Parameters.AddWithValue("@S",txtNameKala.Text+"%");
            da.Fill(ds, "Kala");
            dgvKala.DataSource = ds;
            dgvKala.DataMember = "Kala";
        }

        private void txtGroohKala_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand();
            da.SelectCommand.Connection = con;
            da.SelectCommand.CommandText = "select * from Kala where NameGrooh Like '%' + @S +'%'";
            da.SelectCommand.Parameters.AddWithValue("@S", txtGroohKala.Text + "%");
            da.Fill(ds, "Kala");
            dgvKala.DataSource = ds;
            dgvKala.DataMember = "Kala";
        }

        private void dgvKala_MouseUp(object sender, MouseEventArgs e)
        {
            txtIDKala.Text = dgvKala[0, dgvKala.CurrentRow.Index].Value.ToString();
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            frmKala frm = new frmKala();
            frm.txtID.Text = dgvKala[0, dgvKala.CurrentRow.Index].Value.ToString();
            frm.comboGroohKala.Text = dgvKala[1, dgvKala.CurrentRow.Index].Value.ToString();
            frm.txtNameKala.Text = dgvKala[2, dgvKala.CurrentRow.Index].Value.ToString();
            frm.txtKharid.Text = dgvKala[3, dgvKala.CurrentRow.Index].Value.ToString();
            frm.txtForosh.Text = dgvKala[4, dgvKala.CurrentRow.Index].Value.ToString();
            frm.txtTedad.Text = dgvKala[5, dgvKala.CurrentRow.Index].Value.ToString();
            frm.txtVahed.Text = dgvKala[6, dgvKala.CurrentRow.Index].Value.ToString();
            frm.ShowDialog();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            display();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report1 = new StiReport();
            Report1.Load("Report/ReportKalaha.mrt");
            Report1.Compile();
            Report1.ShowWithRibbonGUI();
        }
    }
}
