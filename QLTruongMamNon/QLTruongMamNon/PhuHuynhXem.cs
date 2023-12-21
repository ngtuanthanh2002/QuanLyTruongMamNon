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
using Excel = Microsoft.Office.Interop.Excel;
namespace QLTruongMamNon
{
    public partial class PhuHuynhXem : Form
    {
        SqlConnection con;
        public PhuHuynhXem()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=LAPTOP-C24RU1TB\SQLEXPRESS;Initial Catalog=QLTruongMamNon;Integrated Security=True");
            con.Open();
        }
        private void QLtaichinh_Load()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("select Hocphi.*,hocsinh.TenHS from Hocphi,hocsinh where Hocphi.MaHS = hocsinh.MaHS", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataHocphi.DataSource = dt;
        }
        public void loadhs()
        {
            SqlCommand cmd = new SqlCommand("select * from hocsinh", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataHocSinh.DataSource = dt;
        }
        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataHocphi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PhuHuynhXem_Load(object sender, EventArgs e)
        {
            
            QLtaichinh_Load();
            loadhs();
        }

        private void bttimhs_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from hocsinh where MaHS like N'%" + timhs.Text + "%' or TenHS like N'%" + timhs.Text + "%' or Tenlop like N'%" + timhs.Text + "%'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataHocSinh.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select Hocphi.* , hocsinh.TenHS from Hocphi,hocsinh where Hocphi.MaHS = hocsinh.MaHS and ( Mahp like N'%" + txtTimkiem.Text + "%' or Hocphi.MaHS like N'%" + txtTimkiem.Text + "%' or TenHS like N'%" + txtTimkiem.Text + "%' )", con);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataHocphi.DataSource = dataTable;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn đăng xuất?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                DangNhap f = new DangNhap();
                f.ShowDialog();
                this.Close();
            }
        }

        private void labe_tenn_Click(object sender, EventArgs e)
        {

        }

        private void PhuHuynhXem_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
