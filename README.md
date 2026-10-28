# BÁO CÁO BÀI TẬP THỰC HÀNH LẬP TRÌNH C# WINFORMS

## I. THÔNG TIN SINH VIÊN

- **Họ và tên:** Lê Minh Dũng
- **Mã số sinh viên:** 24810320213
- **Lớp học phần:** Lập trình .NET - Mã Lớp: D19QTANM1
- **Link Repository:** https://github.com/leminhdung050-w2k/BT.NET_28-10-2026.git

---

## II. DANH SÁCH BÀI TẬP HOÀN THÀNH & TỰ ĐÁNH GIÁ

| Bài tập | Tiêu đề bài tập | Trạng thái | Mức độ hoàn thành (%) |
| :---: | :--- | :---: | :---: |
| **Bài 5.1** | Hệ thống Đăng nhập, Validation & Xác thực tài khoản bằng SQL Server | Hoàn thành | 100% |
| **Bài 5.2** | Đăng ký Học viên (Data Binding ComboBox, MaskedTextBox) & lưu trữ CSDL | Hoàn thành | 100% |

---

## III. MINH HỌA GIAO DIỆN & KỸ THUẬT NỔI BẬT

### 1. Bài 5.1: Hệ thống Đăng nhập & Xác thực Tài khoản
- **Kỹ thuật áp dụng:**
  - Kết nối Cơ sở dữ liệu SQL Server (`.\SQLEXPRESS`, database `BT5_1_DangNhap`) qua NuGet `Microsoft.Data.SqlClient`.
  - Truy vấn `SELECT` kiểm tra tài khoản trong bảng `TaiKhoan`, sử dụng `SqlParameter` chống tấn công SQL Injection.
  - Mật khẩu được lưu dưới dạng mã hóa **SHA-256** (khớp với hàm `HASHBYTES('SHA2_256', ...)` trong SQL Server), không lưu mật khẩu dạng chữ thường.
  - Bắt lỗi dữ liệu nhập với `ErrorProvider` (kiểm tra rỗng tên đăng nhập và mật khẩu, báo sai thông tin đăng nhập).
  - Tùy biến hiển thị mật khẩu bằng `chkShowPassword` (`UseSystemPasswordChar`), có chữ gợi ý (placeholder) "Password" trong ô mật khẩu.
  - Thiết lập phím tắt chuẩn WinForms: phím **Enter** kích hoạt `AcceptButton` (Login) và phím **Esc** kích hoạt `CancelButton` (Exit).
- **Hình ảnh minh họa Giao diện:**

![Giao diện chính Form Đăng nhập](Ảnh/Ảnh%20BT5.1/A1_BT5.1.png)
*Mô tả: Giao diện Form Đăng nhập với ô tên đăng nhập, ô mật khẩu, tùy chọn "show password" và hai nút Login / Exit.*

![Đăng nhập thất bại](Ảnh/Ảnh%20BT5.1/A2_BT5.1.png)
*Mô tả: Hộp thoại thông báo khi đăng nhập sai thông tin, đồng thời `ErrorProvider` hiển thị biểu tượng cảnh báo tại ô mật khẩu.*

![Đăng nhập thành công từ CSDL](Ảnh/Ảnh%20BT5.1/A3_BT5.1.png)
*Mô tả: Thông báo đăng nhập thành công, hiển thị Họ tên (`HoTen`) truy vấn từ bảng `TaiKhoan` trong database `BT5_1_DangNhap`.*

![Script tạo CSDL Bài 5.1](Ảnh/Ảnh%20BT5.1/A4_BT5.1.png)
*Mô tả: Script `BT5_1_DangNhap.sql` trong SQL Server Management Studio (SSMS) tạo database, bảng `TaiKhoan` và tài khoản mẫu `admin`.*

---

### 2. Bài 5.2: Đăng ký Học viên
- **Kỹ thuật áp dụng:**
  - Nạp danh mục khóa học động từ bảng `KhoaHoc` (câu lệnh `SELECT`) lên `ComboBox` qua kỹ thuật **Data Binding** với cặp thuộc tính `DisplayMember` (hiển thị `TenKhoaHoc`) và `ValueMember` (nhận ngầm `MaKhoaHoc`).
  - Định dạng nhập số điện thoại chuẩn qua `MaskedTextBox` với mặt nạ `Mask = "(000) 000-0000"` và kiểm tra đủ số bằng `MaskCompleted`.
  - Định dạng ngày sinh qua `DateTimePicker` (`Format = Custom`, `CustomFormat = "dd/MM/yyyy"`).
  - Lựa chọn giới tính với `RadioButton` (Nam / Nữ).
  - Nút **Đăng ký**: kiểm tra dữ liệu nhập, ghi bản ghi vào bảng `DangKy` bằng câu lệnh `INSERT` (dùng `SqlParameter`, lấy mã vừa tạo bằng `OUTPUT INSERTED.MaDangKy`), sau đó tổng hợp chi tiết thông tin học viên in ra `MessageBox`.
- **Hình ảnh minh họa Giao diện:**

![Giao diện Form Đăng ký](Ảnh/Ảnh%20BT5.2/A1_BT5.2.png)
*Mô tả: Giao diện Form Đăng ký học viên, danh mục khóa học được tải tự động từ CSDL vào ComboBox.*

![Tổng hợp thông tin đăng ký lên MessageBox](Ảnh/Ảnh%20BT5.2/A2_BT5.2.png)
*Mô tả: Hộp thoại MessageBox tổng hợp thông tin học viên sau khi lưu thành công vào CSDL (kèm mã đăng ký).*

![Script tạo CSDL Bài 5.2](Ảnh/Ảnh%20BT5.2/A3_BT5.3.png)
*Mô tả: Script `BT5_2_DangKyHocVien.sql` trong SSMS tạo database, bảng `KhoaHoc` (kèm 4 khóa học mẫu) và bảng `DangKy`.*

---

## IV. HƯỚNG DẪN CÀI ĐẶT & CHẠY DỰ ÁN

1. **Cấu hình Cơ sở dữ liệu (SQL Server):**
   - Mở công cụ **SQL Server Management Studio (SSMS)** và kết nối tới SQL Server (Windows Authentication).
   - Mở và thực thi (F5) lần lượt các file script:
     - `BT5.1/Database/BT5_1_DangNhap.sql` để khởi tạo database `BT5_1_DangNhap` và bảng `TaiKhoan` (tài khoản mẫu: **admin / 123456**).
     - `BT5.2/Database/BT5_2_DangKyHocVien.sql` để khởi tạo database `BT5_2_DangKyHocVien`, bảng `KhoaHoc` và `DangKy`.

2. **Cấu hình chuỗi kết nối:**
   - Chuỗi kết nối được thiết lập trong file `Database.cs` của mỗi bài:
     ```csharp
     // BT5.1
     @"Data Source=.\SQLEXPRESS;Initial Catalog=BT5_1_DangNhap;Integrated Security=True;TrustServerCertificate=True"
     // BT5.2
     @"Data Source=.\SQLEXPRESS;Initial Catalog=BT5_2_DangKyHocVien;Integrated Security=True;TrustServerCertificate=True"
     ```
   - Nếu SQL Server trên máy có tên khác `.\SQLEXPRESS`, sửa lại phần `Data Source`.

3. **Chạy ứng dụng:**
   - Mở file `BT5.1/BT5.1.slnx` hoặc `BT5.2/BT5.2.slnx` bằng **Visual Studio**.
   - Bấm **F5** (hoặc chọn nút **Start**) để biên dịch và chạy từng bài tập. Visual Studio sẽ tự tải lại thư viện NuGet `Microsoft.Data.SqlClient` khi Build.
