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
    public partial class FrmTanzimat : Form
    {
        public FrmTanzimat()
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
            adp.SelectCommand.CommandText = "select * from Tanzimat";
            adp.Fill(ds, "Tanzimat");
            dgvTanzimat.DataSource = ds;
            dgvTanzimat.DataMember = "Tanzimat";
        }
        private void FrmTanzimat_Load(object sender, EventArgs e)
        {
            display();
            dgvTanzimat.Columns[0].HeaderText = "کد";
            dgvTanzimat.Columns[1].HeaderText = "نام فروشگاه";
            dgvTanzimat.Columns[2].HeaderText = "تلفن";
            dgvTanzimat.Columns[3].HeaderText = "موبایل";
            dgvTanzimat.Columns[4].HeaderText = "آدرس";
            dgvTanzimat.Columns[5].HeaderText = "توضیحات";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "insert into Tanzimat (NameFroshgah,tel,mobile,address,tozih) values(@a,@b,@c,@d,@e)";
            cmd.Parameters.AddWithValue("@a",txtNameForoshgah.Text);
            cmd.Parameters.AddWithValue("@b", txtTel.Text);
            cmd.Parameters.AddWithValue("@c", txtMobile.Text);
            cmd.Parameters.AddWithValue("@d", txtAddress.Text);
            cmd.Parameters.AddWithValue("@e", txtTozihat.Text);
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
            { int x = Convert.ToInt32(dgvTanzimat.SelectedCells[0].Value);
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "delete from Tanzimat where IdTanzimat=@n";
            cmd.Parameters.AddWithValue("@n", txtId.Text);
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

        private void dgvTanzimat_MouseUp(object sender, MouseEventArgs e)
        {
            txtId.Text = dgvTanzimat[0, dgvTanzimat.CurrentRow.Index].Value.ToString();
            txtNameForoshgah.Text = dgvTanzimat[1, dgvTanzimat.CurrentRow.Index].Value.ToString();
            txtTel.Text = dgvTanzimat[2, dgvTanzimat.CurrentRow.Index].Value.ToString();
            txtMobile.Text = dgvTanzimat[3, dgvTanzimat.CurrentRow.Index].Value.ToString();
            txtAddress.Text = dgvTanzimat[4, dgvTanzimat.CurrentRow.Index].Value.ToString();
            txtTozihat.Text = dgvTanzimat[5, dgvTanzimat.CurrentRow.Index].Value.ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
           
            try
            { cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "update Tanzimat set NameFroshgah=N'"+txtNameForoshgah.Text+"',tel='"+txtTel.Text+"',mobile='"+txtMobile.Text+"',address=N'"+txtAddress.Text+"',tozih=N'"+txtTozihat.Text+"' where IdTanzimat="+txtId.Text;
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
