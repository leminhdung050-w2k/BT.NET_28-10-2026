using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.SqlClient;

namespace BT5._1
{
    // Lớp hỗ trợ kết nối SQL Server
    internal static class Database
    {
        // Chuỗi kết nối: sửa "Data Source" nếu SQL Server của bạn có tên khác
        public const string ChuoiKetNoi =
            @"Data Source=.\SQLEXPRESS;Initial Catalog=BT5_1_DangNhap;Integrated Security=True;TrustServerCertificate=True";

        public static SqlConnection TaoKetNoi()
        {
            return new SqlConnection(ChuoiKetNoi);
        }

        // Mã hóa mật khẩu bằng SHA-256 (khớp với HASHBYTES('SHA2_256', N'...') trong SQL)
        public static string MaHoaMatKhau(string matKhau)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] hash = sha.ComputeHash(Encoding.Unicode.GetBytes(matKhau));
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }
    }
}
