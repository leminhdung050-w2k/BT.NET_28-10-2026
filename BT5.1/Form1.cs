using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;


namespace BT5._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Người dùng gõ chữ thật (không phải chữ gợi ý) thì xóa báo lỗi
            if (txtUsername.ForeColor != Color.Gray && txtUsername.Text.Trim() != "")
                errorProvider.SetError(txtUsername, "");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // Ô mật khẩu đang hiện chữ gợi ý "Password" (chưa nhập gì)
        private bool DangHienGoiYPassword()
        {
            return txtPassword.Text == "Password" && txtPassword.ForeColor == Color.Gray;
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (DangHienGoiYPassword()) return; // Chữ gợi ý "Password" luôn hiện rõ

            if (chkShowPassword.Checked == true)
            {
                txtPassword.UseSystemPasswordChar = false; // Hiện mật khẩu
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;  // Ẩn mật khẩu (*)
            }
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Username" && txtUsername.ForeColor == Color.Gray)
            {
                txtUsername.Text = "";
                txtUsername.ForeColor = Color.Black; // Đổi lại màu chữ đen bình thường
            }
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
            {
                txtUsername.ForeColor = Color.Gray; // Chữ gợi ý màu xám
                txtUsername.Text = "Username";
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (DangHienGoiYPassword())
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
                // Ẩn / hiện mật khẩu theo ô "show password"
                txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                // Tắt tính năng ẩn mật khẩu để chữ "Password" hiện ra bình thường
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.ForeColor = Color.Gray;
                txtPassword.Text = "Password";
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            // Người dùng gõ chữ thật (không phải chữ gợi ý) thì xóa báo lỗi
            if (!DangHienGoiYPassword() && txtPassword.Text != "")
                errorProvider.SetError(txtPassword, "");
        }

        // Validation: không để trống Tên đăng nhập & Mật khẩu (dùng errorProvider kéo từ Toolbox)
        private bool KiemTraHopLe()
        {
            bool hopLe = true;
            errorProvider.Clear();

            string tenDangNhap = (txtUsername.ForeColor == Color.Gray) ? "" : txtUsername.Text.Trim();
            string matKhau = DangHienGoiYPassword() ? "" : txtPassword.Text;

            if (tenDangNhap == "")
            {
                errorProvider.SetError(txtUsername, "Tên đăng nhập không được để trống!");
                hopLe = false;
            }

            if (matKhau == "")
            {
                errorProvider.SetError(txtPassword, "Mật khẩu không được để trống!");
                hopLe = false;
            }

            return hopLe;
        }

        // Nút Login (phím Enter cũng kích hoạt nút này nhờ AcceptButton)
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!KiemTraHopLe())
            {
                if (errorProvider.GetError(txtUsername) != "") txtUsername.Focus();
                else txtPassword.Focus();
                return;
            }

            string tenDangNhap = txtUsername.Text.Trim();
            object hoTen;

            // SELECT: kiểm tra tài khoản trong CSDL (dùng SqlParameter để chống SQL Injection)
            try
            {
                using (SqlConnection conn = Database.TaoKetNoi())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT ISNULL(HoTen, TenDangNhap) FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau", conn))
                {
                    cmd.Parameters.Add("@TenDangNhap", SqlDbType.NVarChar, 50).Value = tenDangNhap;
                    cmd.Parameters.Add("@MatKhau", SqlDbType.VarChar, 64).Value = Database.MaHoaMatKhau(txtPassword.Text);
                    conn.Open();
                    hoTen = cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không kết nối được CSDL!\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (hoTen == null)
            {
                errorProvider.SetError(txtPassword, "Sai tên đăng nhập hoặc mật khẩu!");
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                txtPassword.SelectAll();
                return;
            }

            MessageBox.Show("Đăng nhập thành công!\nXin chào " + hoTen + ".", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Nút Exit (phím Esc cũng kích hoạt nút này nhờ CancelButton)
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
