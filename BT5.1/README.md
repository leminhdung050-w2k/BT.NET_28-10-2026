## THÔNG TIN SINH VIÊN
- **Họ và tên:** Lê Minh Dũng
- **Mã số sinh viên:** 24810320213
- **Lớp:** D19QTANM1
- **Tên môn học:** Lập trình .NET
- **Tên bài tập:** Bài tập 5.1 - Nâng cấp Bài 4.1 (Form Đăng nhập) lưu trữ vào CSDL SQL Server

## NỘI DUNG
- Form Đăng nhập (Bài 4.1): ErrorProvider, ẩn/hiện mật khẩu, AcceptButton (Enter) / CancelButton (Esc).
- Đăng nhập kiểm tra tài khoản trong bảng `TaiKhoan` bằng câu lệnh SELECT (mật khẩu mã hóa SHA-256), đúng thì hiện thông báo "Đăng nhập thành công".
- Tất cả câu lệnh SQL dùng `SqlParameter` (NuGet `Microsoft.Data.SqlClient`).

## CÀI ĐẶT CSDL
1. Mở SSMS, chạy file `Database/BT5_1_DangNhap.sql` (tạo database `BT5_1_DangNhap`, bảng `TaiKhoan`).
2. Nếu SQL Server không phải `.\SQLEXPRESS`, sửa `Data Source` trong file `Database.cs`.
3. Mở `BT5.1.slnx` bằng Visual Studio, bấm F5. Tài khoản mẫu: **admin / 123456**.
