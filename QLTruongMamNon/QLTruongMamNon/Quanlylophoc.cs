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
    public partial class Quanlylophoc : Form
    {
        SqlConnection con;

        public Quanlylophoc()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=LAPTOP-C24RU1TB\SQLEXPRESS;Initial Catalog=QLTruongMamNon;Integrated Security=True");
            con.Open();
           
        }
        public void loadlop()
        {
            SqlCommand cmd = new SqlCommand("select * from lop", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGlop.DataSource = dt;
        }
        public void loadcombobox()
        {
            //loadcho combobox tenlop ở giáo viên, học sinh,chỉ lấy tên lớp đã nhập
            SqlCommand cmd = new SqlCommand("select * from lop", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "tenlop";
            comboBox2.ValueMember = "tenlop";
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "tenlop";
            comboBox1.ValueMember = "tenlop";
        }
        public void loadhs()
        {
            SqlCommand cmd = new SqlCommand("select * from hocsinh", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataHocSinh.DataSource = dt;
        }
        public void loadgv()
        {
            SqlCommand cmd = new SqlCommand("select * from giaovien", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGiaoVien.DataSource = dt;
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            label_ten.Text = User.Uname;
            loadhs();
            loadlop();
            loadgv();
            loadcombobox();
            
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

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            quanlytaikhoan f = new quanlytaikhoan();
            f.ShowDialog();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            DangNhap f = new DangNhap();
            f.ShowDialog();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn đăng xuất?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                DangNhap f = new DangNhap();
                f.ShowDialog();
                this.Close();
            }
            
        }

        private void bttiml_Click(object sender, EventArgs e)
        {

        }

        private void bttheml_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("insert into lop(tenlop,soluong) values (@tenlop,@soluong)", con);
                cmd.Parameters.AddWithValue("@tenlop", tenlp.Text);
                cmd.Parameters.AddWithValue("@soluong", int.Parse(soluongg.Text));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Thêm thành công");
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    MessageBox.Show("Thêm thất bại");
            }
            loadlop();
            loadcombobox();
        }

        private void btsual_Click(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dataGlop.CurrentRow.Index;
            if (chon >= 0)
            {
                SqlCommand cmd = new SqlCommand(
                    "update lop set Soluong=@soluong where Tenlop = @Tenlopcu;", con);

                cmd.Parameters.AddWithValue("@Soluong", int.Parse(soluongg.Text));
                cmd.Parameters.AddWithValue("@Tenlopcu", dataGlop.Rows[chon].Cells["Tenlop"].Value.ToString());

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Sửa thành công");
                }
                else MessageBox.Show("Sửa thất bại");
                loadgv();
                loadhs();
                loadlop();
                loadcombobox();
            }
        }

        private void btxoal_Click(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dataGlop.CurrentRow.Index;
            if (chon >= 0)
            {
                SqlCommand cmd = new SqlCommand("delete from giaovien where Tenlop = @Tenlop;" +
                    "delete from hocsinh where Tenlop = @Tenlop;" +
                    "delete from lop where Tenlop=@Tenlop;", con);
                cmd.Parameters.AddWithValue("@Tenlop", dataGlop.Rows[chon].Cells["Tenlopp"].Value.ToString());
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Xoá thành công");
                }
                else MessageBox.Show("Xoá thất bại");
                loadgv();
                loadhs();
                loadlop();
                loadcombobox();
            }
        }

        private void bttimhs_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from hocsinh where MaHS like N'%" + timhs.Text + "%' or TenHS like N'%" + timhs.Text + "%' or Tenlop like N'%" + timhs.Text + "%'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataHocSinh.DataSource = dt;
        }

        private void bttimgv_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from giaovien where Magiaovien like N'%" + timgv.Text + "%' or Tengiaovien like N'%" + timgv.Text + "%' or Tenlop like N'%" + timgv.Text + "%'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGiaoVien.DataSource = dt;
        }

        private void bttiml_Click_1(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from lop where Tenlop like N'%" + timlop.Text + "%'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGlop.DataSource = dt;
        }

        private void dataGlop_SelectionChanged(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dataGlop.CurrentRow.Index;
            if (chon >= 0)
            {
                tenlp.Text = dataGlop.Rows[chon].Cells["Tenlopp"].Value.ToString();
                soluongg.Text = dataGlop.Rows[chon].Cells["Soluong"].Value.ToString();
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into giaovien (Magiaovien,Tengiaovien,Sdt,Diachi,Tenlop,cccd) values (@Magiaovien,@Tengiaovien,@Sdt,@Diachi,@Tenlop,@cccd)", con);
            cmd.Parameters.AddWithValue("@Magiaovien", magiaovienn.Text);
            cmd.Parameters.AddWithValue("@Tengiaovien", tengiaovienn.Text);
            cmd.Parameters.AddWithValue("@Sdt", sdtgv.Text);
            cmd.Parameters.AddWithValue("@Diachi", diachigv.Text);
            cmd.Parameters.AddWithValue("@Tenlop", comboBox1.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@cccd", text_cccd.Text.ToString());
            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Thêm thành công");
            }
            else MessageBox.Show("Thêm thất bại");
            loadgv();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGlop_SelectionChanged_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {

        }

        private void dataGiaoVien_SelectionChanged(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dataGiaoVien.CurrentRow.Index;
            if (chon >= 0)
            {
                magiaovienn.Text = dataGiaoVien.Rows[chon].Cells["Magiaovien"].Value.ToString();
                tengiaovienn.Text = dataGiaoVien.Rows[chon].Cells["Tengiaovien"].Value.ToString();
                diachigv.Text = dataGiaoVien.Rows[chon].Cells["Diachi"].Value.ToString();
                comboBox1.SelectedValue = dataGiaoVien.Rows[chon].Cells["Tenlop"].Value;
                sdtgv.Text = dataGiaoVien.Rows[chon].Cells["Sdt"].Value.ToString();
                text_cccd.Text = dataGiaoVien.Rows[chon].Cells["CCCD"].Value.ToString();
            }
        }

        private void dataHocSinh_SelectionChanged(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dataHocSinh.CurrentRow.Index;
            if (chon >= 0)
            {
                mahs.Text = dataHocSinh.Rows[chon].Cells["mahss"].Value.ToString();
                tenhs.Text = dataHocSinh.Rows[chon].Cells["tenhss"].Value.ToString();
                diachihs.Text = dataHocSinh.Rows[chon].Cells["diachii"].Value.ToString();
                comboBox2.SelectedValue = dataHocSinh.Rows[chon].Cells["tenloppp"].Value;
                sdths.Text = dataHocSinh.Rows[chon].Cells["Sdtphh"].Value.ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dataGiaoVien.CurrentRow.Index;
            if (chon >= 0)
            {
                SqlCommand cmd = new SqlCommand(
                    "update giaovien set Tengiaovien=@Tengiaovien,Diachi=@Diachi,Tenlop=@Tenlop,Sdt=@Sdtgv,cccd=@cccd where Magiaovien= @Magiaoviencu;", con);

                cmd.Parameters.AddWithValue("@Tengiaovien", tengiaovienn.Text);
                cmd.Parameters.AddWithValue("@Diachi", diachigv.Text);
                cmd.Parameters.AddWithValue("@Tenlop", comboBox1.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Sdtgv", sdtgv.Text.ToString());
                cmd.Parameters.AddWithValue("@cccd", text_cccd.Text.ToString());
                cmd.Parameters.AddWithValue("@Magiaoviencu", dataGiaoVien.Rows[chon].Cells["Magiaovien"].Value.ToString());
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Sửa thành công");
                }
                loadgv();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dataGiaoVien.CurrentRow.Index;
            if (chon >= 0)
            {
                SqlCommand cmd = new SqlCommand("delete from bangluong where Magiaovien=@Magiaovien;" +
                    "delete from giaovien where Magiaovien=@Magiaovien; ", con);
                cmd.Parameters.AddWithValue("@Magiaovien", dataGiaoVien.Rows[chon].Cells["Magiaovien"].Value.ToString());
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Xoá thành công");
                }
                loadgv();
            }
        }

        private void btthemhs_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into hocsinh(MaHS,TenHS,Diachi,Tenlop,Sdtph) values (@MaHS,@TenHS,@Diachi,@Tenlop,@Sdtph)", con);
            cmd.Parameters.AddWithValue("@MaHS", mahs.Text);
            cmd.Parameters.AddWithValue("@TenHS", tenhs.Text);
            cmd.Parameters.AddWithValue("@Diachi", diachihs.Text);
            cmd.Parameters.AddWithValue("@Tenlop", comboBox2.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Sdtph", sdths.Text.ToString());
            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Thêm thành công");
            }
            loadhs();
        }

        private void btsuahs_Click(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dataHocSinh.CurrentRow.Index;
            if (chon >= 0)
            {
                SqlCommand cmd = new SqlCommand(
                    "update hocsinh set TenHS=@TenHS,Diachi=@Diachi,Tenlop=@Tenlop,Sdtph=@Sdtph where MaHS= @MaHScu;"
                    , con);
                cmd.Parameters.AddWithValue("@TenHS", tenhs.Text);
                cmd.Parameters.AddWithValue("@Diachi", diachihs.Text);
                cmd.Parameters.AddWithValue("@Tenlop", comboBox2.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Sdtph", sdths.Text.ToString());
                cmd.Parameters.AddWithValue("@MaHScu", dataHocSinh.Rows[chon].Cells["MaHS"].Value.ToString());
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Sửa thành công");
                }
                loadhs();
            }
        }

        private void btxoahs_Click(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dataHocSinh.CurrentRow.Index;
            if (chon >= 0)
            {
                SqlCommand cmd = new SqlCommand("delete from Hocphi where MaHS=@MaHS;" +
                    "delete from hocsinh where MaHS=@MaHS; ", con);
                cmd.Parameters.AddWithValue("@MaHS", dataHocSinh.Rows[chon].Cells["mahss"].Value.ToString());
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Xoá thành công");
                }
                loadhs();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Quanlylophoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
    }
}
