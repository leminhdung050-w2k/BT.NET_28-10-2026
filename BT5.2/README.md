## THÔNG TIN SINH VIÊN
- **Họ và tên:** Lê Minh Dũng
- **Mã số sinh viên:** 24810320213
- **Lớp:** D19QTANM1
- **Tên môn học:** Lập trình .NET
- **Tên bài tập:** Bài tập 5.2 - Nâng cấp Bài 4.2 (Form Đăng ký học viên) lưu trữ vào CSDL SQL Server

## NỘI DUNG
- Form Đăng ký (Bài 4.2): MaskedTextBox `(000) 000-0000`, DateTimePicker `dd/MM/yyyy`, RadioButton giới tính, ComboBox khóa học.
- `cboCourse` nạp từ bảng `KhoaHoc` (DisplayMember = TenKhoaHoc, ValueMember = MaKhoaHoc).
- Nút Đăng ký lưu vào bảng `DangKy` và in thông tin lên MessageBox (INSERT).
- Tất cả câu lệnh SQL dùng `SqlParameter` (NuGet `Microsoft.Data.SqlClient`).

## CÀI ĐẶT CSDL
1. Mở SSMS, chạy file `Database/BT5_2_DangKyHocVien.sql` (tạo database `BT5_2_DangKyHocVien`, bảng `KhoaHoc`, `DangKy`).
2. Nếu SQL Server không phải `.\SQLEXPRESS`, sửa `Data Source` trong file `Database.cs`.
3. Mở `BT5.2.slnx` bằng Visual Studio, bấm F5.
