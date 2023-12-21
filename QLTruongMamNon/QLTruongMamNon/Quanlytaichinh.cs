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
namespace QLTruongMamNon
{
    public partial class Quanlytaichinh : Form
    {
        SqlConnection con;
        public Quanlytaichinh()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=LAPTOP-C24RU1TB\SQLEXPRESS;Initial Catalog=QLTruongMamNon;Integrated Security=True");
            con.Open();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            baocaothongke f = new baocaothongke();
            f.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            quanlytaikhoan f = new quanlytaikhoan();
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Quanlylophoc f = new Quanlylophoc();
            f.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn đăng xuất?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                DangNhap f = new DangNhap();
                f.ShowDialog();
                this.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

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


            SqlCommand cmd1 = new SqlCommand("select bangluong.*,giaovien.Tengiaovien from bangluong,giaovien where bangluong.Magiaovien = giaovien.Magiaovien", con);
            cmd1 = new SqlCommand("update bangluong set luong = Hsluong * Luongcoban ", con);
            cmd1 = new SqlCommand("select bangluong.*,giaovien.Tengiaovien from bangluong,giaovien where bangluong.Magiaovien = giaovien.Magiaovien", con);
            SqlDataAdapter d = new SqlDataAdapter(cmd1);
            DataTable dt2 = new DataTable();
            d.Fill(dt2);
            dataLuongthuong.DataSource = dt2;
        }
        private void Quanlytaichinh_Load(object sender, EventArgs e)
        {
            label_ten.Text = User.Uname;
            QLtaichinh_Load();
            
        }

        private void btnThem1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into Hocphi(Mahp,MaHS,Tongtien,Ngaythang) " +
                "values ('" + txtMahocphi.Text + "'," +
                "'" + txtMahocsinh.Text + "'," +
                " '" + txtTongtien.Text + "' ," +
                " '" + dateTimePicker1.Value + "' )", con);

            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("thêm thành công");
            }
            QLtaichinh_Load();
        }

        private void btnSua1_Click(object sender, EventArgs e)
        {
            if (dataHocphi.CurrentRow.Index != -1)
            {
                string a = dateTimePicker1.Value.Year + "/" + dateTimePicker1.Value.Month + "/" + dateTimePicker1.Value.Day;
                SqlCommand cmd = new SqlCommand("update Hocphi set " +
                    " MaHS = '" + txtMahocsinh.Text + "'," +
                    " Tongtien = '" + txtTongtien.Text + "'," +
                    " Ngaythang = '" + a + "'" +
                    " where Mahp = '" + txtMahocphi.Text + "' ", con);
                if (cmd.ExecuteNonQuery() > 0) MessageBox.Show("Sửa thành công");

                cmd = new SqlCommand("select Hocphi.*,hocsinh.TenHS from Hocphi,hocsinh where Hocphi.MaHS = hocsinh.MaHS", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataHocphi.DataSource = dt;
            }
        }

        private void btnXoa1_Click(object sender, EventArgs e)
        {
            if (dataHocphi.CurrentRow.Index != -1)
            {
                SqlCommand cmd = new SqlCommand("delete from Hocphi where Mahp = '" + txtMahocphi.Text + "'", con);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Xóa thành công");
                }
            }
            SqlCommand c = new SqlCommand("select *from Hocphi", con);
            SqlDataAdapter d = new SqlDataAdapter(c);
            DataTable dt = new DataTable();
            d.Fill(dt);
            dataHocphi.DataSource = dt;
        }

        private void bttimgv_Click(object sender, EventArgs e)
        {
            //SqlCommand sqlCommand = new SqlCommand("select * from Hocphi where Mahp like N'%" + txtTimkiem.Text + "%' or MaHS like N'%" + txtTimkiem.Text + "%' or TenHS like N'%" + txtTimkiem.Text + "%'", con);
            SqlCommand command = new SqlCommand("select Hocphi.* , hocsinh.TenHS from Hocphi,hocsinh where Hocphi.MaHS = hocsinh.MaHS and ( Mahp like N'%" + txtTimkiem.Text + "%' or Hocphi.MaHS like N'%" + txtTimkiem.Text + "%' or TenHS like N'%" + txtTimkiem.Text + "%' )", con);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataHocphi.DataSource = dataTable;
        }

        private void btnThem2_Click(object sender, EventArgs e)
        {
            if (dataLuongthuong.CurrentRow.Index != -1)
            {
                SqlCommand cmd = new SqlCommand("insert into bangluong(Mabl,Magiaovien,Hsluong,Luongcoban,Luong) values (" +
                    " '" + txtMabangluong.Text + "'," +
                    " '" + txtMagiaovien.Text + "'," +
                    " '" + txtHesoluong.Text + "'," +
                     " '" + txtLuong.Text + "'," +
                    " '" + txtLuongcuoi.Text + "' )", con);
                if (cmd.ExecuteNonQuery() > 0) MessageBox.Show("Thêm thành công");

                cmd = new SqlCommand("select bangluong.*,giaovien.Tengiaovien from bangluong,giaovien where bangluong.Magiaovien = giaovien.Magiaovien", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataLuongthuong.DataSource = dt;
            }
        }

        private void btnSua2_Click(object sender, EventArgs e)
        {
            if (dataLuongthuong.CurrentRow.Index != -1)
            {
                string luong = double.Parse(txtHesoluong.Text) * double.Parse(txtLuong.Text) + "";
                SqlCommand cmd1 = new SqlCommand("update bangluong set " +
                    "Magiaovien='" + txtMagiaovien.Text + "'," +
                    "Hsluong='" + txtHesoluong.Text + "'," +
                    "Luongcoban='" + txtLuong.Text + "' ," +
                    "Luong='" + luong.ToString() + "' " +
                    "where Mabl='" + txtMabangluong.Text + "' ", con);
                if (cmd1.ExecuteNonQuery() > 0) MessageBox.Show("Sửa thành công");

                SqlCommand cmd = new SqlCommand("select bangluong.*,giaovien.Tengiaovien from bangluong,giaovien where bangluong.Magiaovien = giaovien.Magiaovien", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataLuongthuong.DataSource = dt;
            }
        }

        private void btnXoa2_Click(object sender, EventArgs e)
        {
            if (dataLuongthuong.CurrentRow.Index != -1)
            {
                SqlCommand cmd = new SqlCommand("delete from bangluong where Mabl='" + txtMabangluong.Text + "'", con);
                if (cmd.ExecuteNonQuery() > 0) MessageBox.Show("Xóa thành công");
                cmd = new SqlCommand("select bangluong.*,giaovien.Tengiaovien from bangluong,giaovien where bangluong.Magiaovien = giaovien.Magiaovien", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataLuongthuong.DataSource = dt;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //SqlCommand cmd = new SqlCommand("select bangluong.*,giaovien.Tengiaovien from bangluong,giaovien where bangluong.Tengiaovien = giaovien.Tengiaovien and ( Mabl like N'%" + txtLuongthuong.Text + "%' or Magiaovien like N'%" + txtLuongthuong.Text + "%' ) ", con);
            SqlCommand cmd = new SqlCommand("select bangluong.*,giaovien.Tengiaovien from bangluong,giaovien where bangluong.Magiaovien = giaovien.Magiaovien and ( Mabl like N'%" + txtLuongthuong.Text + "%' or Tengiaovien like N'%" + txtLuongthuong.Text + "%' ) ", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataLuongthuong.DataSource = dataTable;
        }

        private void dataLuongthuong_SelectionChanged(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dataLuongthuong.CurrentRow.Index;
            if (chon >= 0)
            {
                txtMabangluong.Text = dataLuongthuong.Rows[chon].Cells["mabll"].Value.ToString();
                txtMagiaovien.Text = dataLuongthuong.Rows[chon].Cells["magiaovienn"].Value.ToString();
                txtTengiaovien.Text = dataLuongthuong.Rows[chon].Cells["tengiaovienn"].Value.ToString();
                txtHesoluong.Text = dataLuongthuong.Rows[chon].Cells["hsluongg"].Value.ToString();
                txtLuong.Text = dataLuongthuong.Rows[chon].Cells["luongcobann"].Value.ToString();
                txtLuongcuoi.Text = (Double.Parse(txtLuong.Text) * Double.Parse(txtHesoluong.Text)) + "";
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataHocphi_SelectionChanged_1(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dataHocphi.CurrentRow.Index;
            if (chon >= 0)
            {
                txtMahocphi.Text = dataHocphi.Rows[chon].Cells["mahpp"].Value.ToString();
                txtMahocsinh.Text = dataHocphi.Rows[chon].Cells["mahs"].Value.ToString();
                txtTongtien.Text = dataHocphi.Rows[chon].Cells["tongtienn"].Value.ToString();
                dateTimePicker1.Value = DateTime.Parse(dataHocphi.Rows[chon].Cells["ngaythangg"].Value.ToString());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Quanlytaichinh_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
    }
}
