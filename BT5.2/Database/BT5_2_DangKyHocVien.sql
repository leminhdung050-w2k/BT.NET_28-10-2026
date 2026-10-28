-- BT5.2 - CSDL cho Form Đăng ký học viên
IF DB_ID(N'BT5_2_DangKyHocVien') IS NULL
    CREATE DATABASE BT5_2_DangKyHocVien;
GO

USE BT5_2_DangKyHocVien;
GO

-- Danh sách khóa học
IF OBJECT_ID(N'dbo.KhoaHoc', N'U') IS NULL
CREATE TABLE dbo.KhoaHoc
(
    MaKhoaHoc  VARCHAR(10)   NOT NULL PRIMARY KEY,   
    TenKhoaHoc NVARCHAR(100) NOT NULL               
);
GO

-- Thông tin đăng ký
IF OBJECT_ID(N'dbo.DangKy', N'U') IS NULL
CREATE TABLE dbo.DangKy
(
    MaDangKy    INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    HoTen       NVARCHAR(100) NOT NULL,            
    SoDienThoai VARCHAR(20)   NOT NULL,            
    NgaySinh    DATE          NOT NULL,          
    GioiTinh    NVARCHAR(5)   NOT NULL,            
    MaKhoaHoc   VARCHAR(10)   NOT NULL REFERENCES dbo.KhoaHoc(MaKhoaHoc),  
    NgayDangKy  DATETIME      NOT NULL DEFAULT GETDATE()
);
GO

IF NOT EXISTS (SELECT 1 FROM dbo.KhoaHoc)
    INSERT INTO dbo.KhoaHoc (MaKhoaHoc, TenKhoaHoc) VALUES
    ('KH01', N'Lập trình C# cơ bản'),
    ('KH02', N'Lập trình Windows Forms'),
    ('KH03', N'Cơ sở dữ liệu SQL Server'),
    ('KH04', N'Lập trình Web ASP.NET');
GO

SELECT * FROM dbo.KhoaHoc;
SELECT * FROM dbo.DangKy;
