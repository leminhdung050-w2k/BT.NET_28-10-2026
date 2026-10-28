using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace BT5._2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TaiKhoaHoc();
            dtpBirthDate.Value = new DateTime(2005, 1, 1);
        }

        // SELECT: nạp danh sách khóa học từ bảng KhoaHoc vào cboCourse
        private void TaiKhoaHoc()
        {
            try
            {
                using (SqlConnection conn = Database.TaoKetNoi())
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT MaKhoaHoc, TenKhoaHoc FROM KhoaHoc ORDER BY MaKhoaHoc", conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboCourse.DataSource = dt;
                    cboCourse.DisplayMember = "TenKhoaHoc"; // Chữ hiển thị trên ComboBox
                    cboCourse.ValueMember = "MaKhoaHoc";    // Giá trị lấy ra bằng SelectedValue
                    cboCourse.SelectedIndex = -1;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không kết nối được CSDL!\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút "Đăng ký": lưu vào CSDL (INSERT) rồi in thông tin đăng ký lên MessageBox
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (txtHoTen.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập họ và tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }
            if (!mtxtPhone.MaskCompleted)
            {
                MessageBox.Show("Vui lòng nhập đủ số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtxtPhone.Focus();
                return;
            }
            if (cboCourse.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn khóa học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCourse.Focus();
                return;
            }

            string gioiTinh = rdoNam.Checked ? "Nam" : "Nữ";

            try
            {
                int maDangKy;
                using (SqlConnection conn = Database.TaoKetNoi())
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO DangKy (HoTen, SoDienThoai, NgaySinh, GioiTinh, MaKhoaHoc) " +
                    "OUTPUT INSERTED.MaDangKy " +
                    "VALUES (@HoTen, @SoDienThoai, @NgaySinh, @GioiTinh, @MaKhoaHoc)", conn))
                {
                    // Dùng SqlParameter để chống SQL Injection
                    cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar, 100).Value = txtHoTen.Text.Trim();
                    cmd.Parameters.Add("@SoDienThoai", SqlDbType.VarChar, 20).Value = mtxtPhone.Text;
                    cmd.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = dtpBirthDate.Value.Date;
                    cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar, 5).Value = gioiTinh;
                    cmd.Parameters.Add("@MaKhoaHoc", SqlDbType.VarChar, 10).Value = cboCourse.SelectedValue;

                    conn.Open();
                    maDangKy = Convert.ToInt32(cmd.ExecuteScalar());
                }

                string thongTin =
                    "Họ và tên: " + txtHoTen.Text.Trim() + "\n" +
                    "Số điện thoại: " + mtxtPhone.Text + "\n" +
                    "Ngày sinh: " + dtpBirthDate.Value.ToString("dd/MM/yyyy") + "\n" +
                    "Giới tính: " + gioiTinh + "\n" +
                    "Khóa học: " + cboCourse.Text + " (Mã: " + cboCourse.SelectedValue + ")" + "\n\n" +
                    "Đã lưu vào CSDL (Mã đăng ký: " + maDangKy + ")";

                MessageBox.Show(thongTin, "Thông tin đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
