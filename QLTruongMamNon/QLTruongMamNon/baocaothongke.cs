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
    public partial class baocaothongke : Form
    {
        SqlConnection con;
        public baocaothongke()
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

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            quanlytaikhoan f = new quanlytaikhoan();
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

        private void bttimgv_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select Hocphi.* , hocsinh.TenHS from Hocphi,hocsinh where Hocphi.MaHS = hocsinh.MaHS " +
                "and Tongtien = 0 and ( Mahp like N'%" + txtTim1.Text + "%' or Hocphi.MaHS like N'%" + txtTim1.Text + "%' " +
                "or TenHS like N'%" + txtTim1.Text + "%' )", con);

            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            dataChua.DataSource = dataTable;
        }

        private void dataChua_SelectionChanged(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dataChua.CurrentRow.Index;
            if (chon >= 0)
            {
                txtMahocphi1.Text = dataChua.Rows[chon].Cells["mahpp"].Value.ToString();
                txtMahocsinh1.Text = dataChua.Rows[chon].Cells["mahss"].Value.ToString();
                txtTenhocsinh1.Text = dataChua.Rows[chon].Cells["tenhss"].Value.ToString();
                txtTongtien1.Text = dataChua.Rows[chon].Cells["tongtienn"].Value.ToString();
                DateTime dt1 = new DateTime();
                dt1 = DateTime.Parse(dataChua.Rows[chon].Cells["ngaythangg"].Value.ToString());
                dateTimePicker1.Value = dt1;
            }
        }
        private void baocaothongke_Load()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd2 = new SqlCommand("select Hocphi.*,hocsinh.TenHS from Hocphi,hocsinh where Tongtien = 0 and Hocphi.MaHS = hocsinh.MaHS ", con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataChua.DataSource = dt2;
            SqlCommand dem1 = new SqlCommand("select count(*) from Hocphi where Tongtien = 0", con);
            txtDem1.Text = dem1.ExecuteScalar() + " học sinh";

            SqlCommand cmd = new SqlCommand("select Hocphi.*,hocsinh.TenHS from Hocphi,hocsinh where Tongtien > 0 and Hocphi.MaHS = hocsinh.MaHS  ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataRoi.DataSource = dt;
            SqlCommand dem2 = new SqlCommand("select count(*) from Hocphi where Tongtien > 0", con);
            txtDem2.Text = dem2.ExecuteScalar() + " học sinh";

            SqlCommand cmd1 = new SqlCommand("select bangluong.*,giaovien.Tengiaovien from bangluong,giaovien where bangluong.Magiaovien = giaovien.Magiaovien ", con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            dataLuong.DataSource = dt1;
            SqlCommand dem3 = new SqlCommand("select count(*) from bangluong", con);
            txtDem3.Text = dem3.ExecuteScalar() + " giáo viên";
        }
        private void baocaothongke_Load(object sender, EventArgs e)
        {
            labe_tenn.Text = User.Uname;
            baocaothongke_Load();
        }

        private void dataLuong_SelectionChanged(object sender, EventArgs e)
        {
            if (dataLuong.CurrentRow.Index != -1)
            {
                txtMabangluong3.Text = dataLuong.Rows[dataLuong.CurrentRow.Index].Cells["mabl3"].Value.ToString();
                txtMagiaovien3.Text = dataLuong.Rows[dataLuong.CurrentRow.Index].Cells["magiaovien3"].Value.ToString();
                txtHesoluong3.Text = dataLuong.Rows[dataLuong.CurrentRow.Index].Cells["hsluong3"].Value.ToString();
                txtLuong2.Text = dataLuong.Rows[dataLuong.CurrentRow.Index].Cells["luongcoban3"].Value.ToString();
                txtTengiaovien.Text = dataLuong.Rows[dataLuong.CurrentRow.Index].Cells["tengiaovien3"].Value.ToString();
                txtLuongcuoi.Text = (Double.Parse(txtLuong2.Text) * Double.Parse(txtHesoluong3.Text)) + "";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("select bangluong.*,giaovien.Tengiaovien from bangluong,giaovien where bangluong.Magiaovien = giaovien.Magiaovien and ( Mabl like N'%" + txtTim3.Text + "%' or Tengiaovien like N'%"+txtTim3.Text+"%' ) ", con);
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            dataLuong.DataSource = dataTable;
        }

        private void txtDem3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataRoi_SelectionChanged(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dataRoi.CurrentRow.Index;
            if (chon >= 0)
            {
                txtMahocphi2.Text = dataRoi.Rows[chon].Cells["mahppp"].Value.ToString();
                txtMahocsinh2.Text = dataRoi.Rows[chon].Cells["mahsss"].Value.ToString();
                txtTenhocsinh2.Text = dataRoi.Rows[chon].Cells["tenhsss"].Value.ToString();
                txtTongtien2.Text = dataRoi.Rows[chon].Cells["tongtiennn"].Value.ToString();
                DateTime dt2 = new DateTime();
                dt2 = DateTime.Parse(dataRoi.Rows[chon].Cells["ngaythanggg"].Value.ToString());
                dateTimePicker3.Value = dt2;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select Hocphi.* , hocsinh.TenHS from Hocphi,hocsinh where Hocphi.MaHS = hocsinh.MaHS and Tongtien >0 and ( Mahp like N'%" + txtTim2.Text + "%' or Hocphi.MaHS like N'%" + txtTim2.Text + "%' or TenHS like N'%" + txtTim2.Text + "%' )", con);

            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            dataRoi.DataSource = dataTable;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string excelFilePath = null;
            SqlCommand cmd = new SqlCommand("select Hocphi.*,hocsinh.TenHS from Hocphi,hocsinh where Tongtien = 0 and Hocphi.MaHS = hocsinh.MaHS  ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            var excelApp = new Excel.Application();
            excelApp.Workbooks.Add();

            // single worksheet
            Excel._Worksheet workSheet = excelApp.ActiveSheet;
            workSheet.Cells[1, 3] = "DANH SÁCH HỌC SINH CHƯA NỘP HỌC PHÍ";
            // column headings
            for (var i = 0; i < tbl.Columns.Count; i++)
            {

                workSheet.Cells[2, i + 1] = tbl.Columns[i].ColumnName;
            }

            // rows
            for (var i = 0; i < tbl.Rows.Count; i++)
            {
                // to do: format datetime values before printing
                for (var j = 0; j < tbl.Columns.Count; j++)
                {
                    if (j == 3)
                    { workSheet.Cells[i + 3, j + 1] = Convert.ToDateTime(tbl.Rows[i][j]).ToString("MM/dd/yyyy"); }
                    else
                    { workSheet.Cells[i + 3, j + 1] = tbl.Rows[i][j]; }
                }
            }

            if (!string.IsNullOrEmpty(excelFilePath))
            {
                try
                {
                    workSheet.SaveAs(excelFilePath);
                    excelApp.Quit();
                    MessageBox.Show("Excel file saved!");
                }
                catch (Exception ex)
                {
                    throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                                        + ex.Message);
                }
            }
            else
            {
                excelApp.Visible = true;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string excelFilePath = null;
            SqlCommand cmd = new SqlCommand("select Hocphi.*,hocsinh.TenHS from Hocphi,hocsinh where Tongtien > 0 and Hocphi.MaHS = hocsinh.MaHS  ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            var excelApp = new Excel.Application();
            excelApp.Workbooks.Add();

            // single worksheet
            Excel._Worksheet workSheet = excelApp.ActiveSheet;
            workSheet.Cells[1, 3] = "DANH SÁCH HỌC SINH ĐÃ NỘP HỌC PHÍ";
            // column headings
            for (var i = 0; i < tbl.Columns.Count; i++)
            {

                workSheet.Cells[2, i + 1] = tbl.Columns[i].ColumnName;
            }

            // rows
            for (var i = 0; i < tbl.Rows.Count; i++)
            {
                // to do: format datetime values before printing
                for (var j = 0; j < tbl.Columns.Count; j++)
                {
                    if (j == 3)
                    { workSheet.Cells[i + 3, j + 1] = Convert.ToDateTime(tbl.Rows[i][j]).ToString("MM/dd/yyyy"); }
                    else
                    { workSheet.Cells[i + 3, j + 1] = tbl.Rows[i][j]; }
                }
            }

            if (!string.IsNullOrEmpty(excelFilePath))
            {
                try
                {
                    workSheet.SaveAs(excelFilePath);
                    excelApp.Quit();
                    MessageBox.Show("Excel file saved!");
                }
                catch (Exception ex)
                {
                    throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                                        + ex.Message);
                }
            }
            else
            {
                excelApp.Visible = true;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string excelFilePath = null;
            SqlCommand cmd = new SqlCommand("select bangluong.*,giaovien.Tengiaovien from bangluong,giaovien where bangluong.Magiaovien = giaovien.Magiaovien  ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            var excelApp = new Excel.Application();
            excelApp.Workbooks.Add();

            // single worksheet
            Excel._Worksheet workSheet = excelApp.ActiveSheet;
            workSheet.Cells[1, 3] = "DANH SÁCH LƯƠNG THƯỞNG";
            // column headings
            for (var i = 0; i < tbl.Columns.Count; i++)
            {

                workSheet.Cells[2, i + 1] = tbl.Columns[i].ColumnName;
            }

            // rows
            for (var i = 0; i < tbl.Rows.Count; i++)
            {
                // to do: format datetime values before printing
                for (var j = 0; j < tbl.Columns.Count; j++)
                {

                    workSheet.Cells[i + 3, j + 1] = tbl.Rows[i][j];
                }
            }

            if (!string.IsNullOrEmpty(excelFilePath))
            {
                try
                {
                    workSheet.SaveAs(excelFilePath);
                    excelApp.Quit();
                    MessageBox.Show("Excel file saved!");
                }
                catch (Exception ex)
                {
                    throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                                        + ex.Message);
                }
            }
            else
            {
                excelApp.Visible = true;
            }
        }

        private void label21_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void baocaothongke_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
