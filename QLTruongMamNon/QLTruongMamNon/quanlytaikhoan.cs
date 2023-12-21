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
    public partial class quanlytaikhoan : Form
    {
        SqlConnection con;
        public quanlytaikhoan()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=LAPTOP-C24RU1TB\SQLEXPRESS;Initial Catalog=QLTruongMamNon;Integrated Security=True");
            con.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Quanlylophoc f = new Quanlylophoc();
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Quanlytaichinh f = new Quanlytaichinh();
            f.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            baocaothongke f = new baocaothongke();
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void bttiml_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from TaiKhoan where TenTaiKhoan like N'%" + textBox3.Text + "%' ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView3.DataSource = dt;
        }

        private void bttheml_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                SqlCommand cmd = new SqlCommand("insert into TaiKhoan(TenTaiKhoan,MatKhau,Chucdanh) values (@TenTaiKhoan,@MatKhau,@Chucdanh)", con);
                cmd.Parameters.AddWithValue("@TenTaiKhoan", tentaikhoan.Text);
                cmd.Parameters.AddWithValue("@MatKhau", matkhau.Text);
                cmd.Parameters.AddWithValue("@Chucdanh", "Giao vien");
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Thêm thành công");
                }
                loadtaikhoan();
            }
            else if (radioButton2.Checked)
            {

                SqlCommand cmd = new SqlCommand("insert into TaiKhoan(TenTaiKhoan,MatKhau,Chucdanh) values (@TenTaiKhoan,@MatKhau,@Chucdanh)", con);
                cmd.Parameters.AddWithValue("@TenTaiKhoan", tentaikhoan.Text);
                cmd.Parameters.AddWithValue("@MatKhau", matkhau.Text);
                cmd.Parameters.AddWithValue("@Chucdanh", "Phu huynh");
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Thêm thành công");
                }
                loadtaikhoan();
            }
            else
            {
                MessageBox.Show("Kiểm tra lại thông tin!", "Thông báo");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void loadtaikhoan()
        {
            SqlCommand cmd = new SqlCommand("select * from TaiKhoan", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView3.DataSource = dt;
        }
        private void quanlytaikhoan_Load(object sender, EventArgs e)
        {
            label_ten.Text = User.Uname;
            loadtaikhoan();
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dataGridView3.CurrentRow.Index;
            if (chon >= 0)
            {
                string gv = "Giao vien";
                if (dataGridView3.Rows[chon].Cells["chucdanh"].Value.ToString() == gv)
                {
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;
                }
                else
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                }
                tentaikhoan.Text = dataGridView3.Rows[chon].Cells["tentk"].Value.ToString();
                matkhau.Text = dataGridView3.Rows[chon].Cells["mk"].Value.ToString();
            }
        }

        private void btsual_Click(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dataGridView3.CurrentRow.Index;
            if (chon >= 0)
            {
                if (radioButton1.Checked)
                {
                    SqlCommand cmd = new SqlCommand(
                    "update TaiKhoan set TenTaiKhoan=@TenTaiKhoan,MatKhau=@MatKhau,Chucdanh=@Chucdanh where TenTaiKhoan=@TenTaiKhoanCu;", con);
                    cmd.Parameters.AddWithValue("@TenTaiKhoan", tentaikhoan.Text);
                    cmd.Parameters.AddWithValue("@TenTaiKhoanCu", dataGridView3.Rows[chon].Cells["tentk"].Value.ToString());
                    cmd.Parameters.AddWithValue("@MatKhau", matkhau.Text);
                    cmd.Parameters.AddWithValue("@Chucdanh", "Giao vien");
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Sửa thành công", "Thông báo");
                    }
                    loadtaikhoan();

                }
                else if (radioButton2.Checked)
                {

                    SqlCommand cmd = new SqlCommand(
                    "update TaiKhoan set TenTaiKhoan=@TenTaiKhoan,MatKhau=@MatKhau,Chucdanh=@Chucdanh where TenTaiKhoan=@TenTaiKhoan;", con);
                    cmd.Parameters.AddWithValue("@TenTaiKhoan", tentaikhoan.Text);
                    cmd.Parameters.AddWithValue("@MatKhau", matkhau.Text);
                    cmd.Parameters.AddWithValue("@Chucdanh", "Phu huynh");
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Sửa thành công", "Thông báo");
                    }
                    loadtaikhoan();
                }
                else
                {
                    MessageBox.Show("Kiểm tra lại thông tin!", "Thông báo");
                }
            }
        }

        private void btxoal_Click(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dataGridView3.CurrentRow.Index;
            if (chon >= 0)
            {
                SqlCommand cmd = new SqlCommand("delete from TaiKhoan where TenTaiKhoan=@TenTaiKhoan;" +
                    "delete from TaiKhoan where TenTaiKhoan=@TenTaiKhoan; ", con);
                cmd.Parameters.AddWithValue("@TenTaiKhoan", dataGridView3.Rows[chon].Cells["tentk"].Value.ToString());
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Xoá thành công");
                }
                loadtaikhoan();
            }
        }

        private void quanlytaikhoan_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
    }
}
