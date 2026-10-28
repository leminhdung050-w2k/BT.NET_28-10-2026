-- BT5.1 - CSDL cho Form Đăng nhập
IF DB_ID(N'BT5_1_DangNhap') IS NULL
    CREATE DATABASE BT5_1_DangNhap;
GO

USE BT5_1_DangNhap;
GO

IF OBJECT_ID(N'dbo.TaiKhoan', N'U') IS NULL
CREATE TABLE dbo.TaiKhoan
(
    TenDangNhap NVARCHAR(50)  NOT NULL PRIMARY KEY,   
    MatKhau     VARCHAR(64)   NOT NULL,              
    HoTen       NVARCHAR(100) NULL,
    NgayTao     DATETIME      NOT NULL DEFAULT GETDATE()
);
GO


IF NOT EXISTS (SELECT 1 FROM dbo.TaiKhoan WHERE TenDangNhap = N'admin')
    INSERT INTO dbo.TaiKhoan (TenDangNhap, MatKhau, HoTen)
    VALUES (N'admin', CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', N'123456'), 2), N'Quản trị viên');
GO

SELECT * FROM dbo.TaiKhoan;
