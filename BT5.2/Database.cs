using Microsoft.Data.SqlClient;

namespace BT5._2
{
    // Lớp hỗ trợ kết nối SQL Server
    internal static class Database
    {
        // Chuỗi kết nối: sửa "Data Source" nếu SQL Server của bạn có tên khác
        public const string ChuoiKetNoi =
            @"Data Source=.\SQLEXPRESS;Initial Catalog=BT5_2_DangKyHocVien;Integrated Security=True;TrustServerCertificate=True";

        public static SqlConnection TaoKetNoi()
        {
            return new SqlConnection(ChuoiKetNoi);
        }
    }
}
