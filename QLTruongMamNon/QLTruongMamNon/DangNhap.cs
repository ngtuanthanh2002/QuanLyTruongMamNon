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
    public partial class DangNhap : Form
    {
        public static string username;
        SqlConnection con;
        public DangNhap()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=LAPTOP-C24RU1TB\SQLEXPRESS;Initial Catalog=QLTruongMamNon;Integrated Security=True");
            con.Open();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string tentk = taikhoan.Text;
            string mk = matkhau.Text;

            if (tentk.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản");
            }
            else if (mk.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu");
            }
            else
            {
                try
                {
                    if (radioButton1.Checked)
                    {
                        string xnchucdanh = "Giao vien";
                        string query = "Select * from TaiKhoan where TenTaiKhoan = '" + tentk + "' " +
                            "and MatKhau ='" + mk + "' and ChucDanh ='" + xnchucdanh + "'";
                        SqlDataAdapter sda = new SqlDataAdapter(query, con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK);
                            User.Uname = taikhoan.Text;
                            Quanlylophoc f = new Quanlylophoc();
                            f.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Đăng nhập thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            taikhoan.Focus();
                        }
                       
                    }
                    else if (radioButton2.Checked)
                    {
                        string xnchucdanh = "Phu huynh";
                        string query = "Select * from TaiKhoan where TenTaiKhoan = '" + tentk + "' " +
                            "and MatKhau ='" + mk + "' and ChucDanh ='" + xnchucdanh + "'";
                        SqlDataAdapter sda = new SqlDataAdapter(query, con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK);
                            PhuHuynhXem f = new PhuHuynhXem();
                            f.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Đăng nhập thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            taikhoan.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập chức danh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        taikhoan.Focus();
                    }
                    
                }
                catch
                {
                    MessageBox.Show("Lỗi đăng nhập");
                }
                finally
                {
                    con.Close();
                }
               
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {

        }

        private void DangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
    }
}
