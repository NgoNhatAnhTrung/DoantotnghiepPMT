using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebDangKySuDungPhongMay.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    TenDangNhap = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TenHienThi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.TenDangNhap);
                });

            migrationBuilder.CreateTable(
                name: "GiangViens",
                columns: table => new
                {
                    MaGiangVien = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "date", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiangViens", x => x.MaGiangVien);
                });

            migrationBuilder.CreateTable(
                name: "HocKys",
                columns: table => new
                {
                    MaHocKy = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenHocKy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocKys", x => x.MaHocKy);
                });

            migrationBuilder.CreateTable(
                name: "MonHocs",
                columns: table => new
                {
                    MaMonHoc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenMonHoc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TinChiLyThuyet = table.Column<int>(type: "int", nullable: false),
                    TinChiThucHanh = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonHocs", x => x.MaMonHoc);
                });

            migrationBuilder.CreateTable(
                name: "NamHocs",
                columns: table => new
                {
                    MaNamHoc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NamBatDau = table.Column<int>(type: "int", nullable: false),
                    NamKetThuc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NamHocs", x => x.MaNamHoc);
                });

            migrationBuilder.CreateTable(
                name: "NguoiQuanLyPhongMays",
                columns: table => new
                {
                    MaNguoiQuanLy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenNguoiQuanLy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "date", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiQuanLyPhongMays", x => x.MaNguoiQuanLy);
                });

            migrationBuilder.CreateTable(
                name: "SinhViens",
                columns: table => new
                {
                    MaSinhVien = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenSinhVien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "date", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhViens", x => x.MaSinhVien);
                });

            migrationBuilder.CreateTable(
                name: "LopHocPhans",
                columns: table => new
                {
                    MaLopHocPhan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaHocKy = table.Column<int>(type: "int", nullable: true),
                    MaNamHoc = table.Column<int>(type: "int", nullable: true),
                    MaMonHoc = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    SoSinhVienToiDa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHocPhans", x => x.MaLopHocPhan);
                    table.ForeignKey(
                        name: "FK_LopHocPhans_HocKys_MaHocKy",
                        column: x => x.MaHocKy,
                        principalTable: "HocKys",
                        principalColumn: "MaHocKy",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_LopHocPhans_MonHocs_MaMonHoc",
                        column: x => x.MaMonHoc,
                        principalTable: "MonHocs",
                        principalColumn: "MaMonHoc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LopHocPhans_NamHocs_MaNamHoc",
                        column: x => x.MaNamHoc,
                        principalTable: "NamHocs",
                        principalColumn: "MaNamHoc",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PhongMays",
                columns: table => new
                {
                    MaPhongMay = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TinhTrangPhongMay = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaNguoiQuanLy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongMays", x => x.MaPhongMay);
                    table.ForeignKey(
                        name: "FK_PhongMays_NguoiQuanLyPhongMays_MaNguoiQuanLy",
                        column: x => x.MaNguoiQuanLy,
                        principalTable: "NguoiQuanLyPhongMays",
                        principalColumn: "MaNguoiQuanLy",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ThongBaos",
                columns: table => new
                {
                    MaThongBao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNguoiQuanLy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NoiDung = table.Column<string>(type: "ntext", nullable: false),
                    ThoiGianDang = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBaos", x => x.MaThongBao);
                    table.ForeignKey(
                        name: "FK_ThongBaos_NguoiQuanLyPhongMays_MaNguoiQuanLy",
                        column: x => x.MaNguoiQuanLy,
                        principalTable: "NguoiQuanLyPhongMays",
                        principalColumn: "MaNguoiQuanLy",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SinhVienLopHocPhans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSinhVien = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaLopHocPhan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhVienLopHocPhans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SinhVienLopHocPhans_LopHocPhans_MaLopHocPhan",
                        column: x => x.MaLopHocPhan,
                        principalTable: "LopHocPhans",
                        principalColumn: "MaLopHocPhan",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SinhVienLopHocPhans_SinhViens_MaSinhVien",
                        column: x => x.MaSinhVien,
                        principalTable: "SinhViens",
                        principalColumn: "MaSinhVien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MayTinhs",
                columns: table => new
                {
                    MaMay = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaPhong = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TinhTrangMayTinh = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MayTinhs", x => x.MaMay);
                    table.ForeignKey(
                        name: "FK_MayTinhs_PhongMays_MaPhong",
                        column: x => x.MaPhong,
                        principalTable: "PhongMays",
                        principalColumn: "MaPhongMay",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhieuDangKyGVs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaGiangVien = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayDangKy = table.Column<DateTime>(type: "date", nullable: false),
                    NgaySuDung = table.Column<DateTime>(type: "date", nullable: false),
                    TietBatDau = table.Column<int>(type: "int", nullable: false),
                    TietKetThuc = table.Column<int>(type: "int", nullable: false),
                    LyDo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    MaPhongMay = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaLopHocPhan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuDangKyGVs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhieuDangKyGVs_GiangViens_MaGiangVien",
                        column: x => x.MaGiangVien,
                        principalTable: "GiangViens",
                        principalColumn: "MaGiangVien",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhieuDangKyGVs_LopHocPhans_MaLopHocPhan",
                        column: x => x.MaLopHocPhan,
                        principalTable: "LopHocPhans",
                        principalColumn: "MaLopHocPhan",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhieuDangKyGVs_PhongMays_MaPhongMay",
                        column: x => x.MaPhongMay,
                        principalTable: "PhongMays",
                        principalColumn: "MaPhongMay",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhieuDangKySVs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSinhVien = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayDangKy = table.Column<DateTime>(type: "date", nullable: false),
                    NgaySuDung = table.Column<DateTime>(type: "date", nullable: false),
                    ThoiGianBatDau = table.Column<TimeSpan>(type: "time", nullable: false),
                    ThoiGianKetThuc = table.Column<TimeSpan>(type: "time", nullable: false),
                    LyDo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    MaPhongMay = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuDangKySVs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhieuDangKySVs_PhongMays_MaPhongMay",
                        column: x => x.MaPhongMay,
                        principalTable: "PhongMays",
                        principalColumn: "MaPhongMay",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhieuDangKySVs_SinhViens_MaSinhVien",
                        column: x => x.MaSinhVien,
                        principalTable: "SinhViens",
                        principalColumn: "MaSinhVien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "TenDangNhap", "MatKhau", "TenHienThi" },
                values: new object[] { "admin", "123456789", "Quản lý" });

            migrationBuilder.InsertData(
                table: "GiangViens",
                columns: new[] { "MaGiangVien", "Email", "GioiTinh", "HoTen", "MatKhau", "NgaySinh", "SoDienThoai", "TrangThai" },
                values: new object[,]
                {
                    { "GV001", "nguyenvana@ute.edu.vn", "Nam", "Nguyễn Văn A", "123456789", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456789", true },
                    { "GV002", "tranthib@ute.edu.vn", "Nữ", "Trần Thị B", "123456789", new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "0987654321", true },
                    { "GV003", "lethic@ute.edu.vn", "Nữ", "Lê Thị C", "123456789", new DateTime(1978, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "0912345678", true },
                    { "GV004", "phamvand@ute.edu.vn", "Nam", "Phạm Văn D", "123456789", new DateTime(1990, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "0908765432", true }
                });

            migrationBuilder.InsertData(
                table: "HocKys",
                columns: new[] { "MaHocKy", "TenHocKy" },
                values: new object[,]
                {
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "Hè" }
                });

            migrationBuilder.InsertData(
                table: "MonHocs",
                columns: new[] { "MaMonHoc", "TenMonHoc", "TinChiLyThuyet", "TinChiThucHanh" },
                values: new object[,]
                {
                    { "ADDB331784", "Cơ sở dữ liệu Nâng cao", 2, 1 },
                    { "ADMP431879", "Lập trình di động nâng cao", 2, 1 },
                    { "ADNT330580", "Mạng máy tính nâng cao", 2, 1 },
                    { "ADPL331379", "Ngôn ngữ Lập trình tiên tiến", 2, 1 },
                    { "AIOT331185", "Trí tuệ nhân tạo cho IOT", 2, 1 },
                    { "BCAP433280", "Blockchain và ứng dụng", 2, 1 },
                    { "CLAD432480", "Quản trị trên môi trường cloud", 2, 1 },
                    { "CNDE430780", "Thiết kế mạng", 2, 1 },
                    { "DAMI330484", "Khai phá dữ liệu", 2, 1 },
                    { "DAWH430784", "Kho dữ liệu", 2, 1 },
                    { "DIFO432180", "Pháp lý kỹ thuật số", 2, 1 },
                    { "ETHA332080", "Tấn công mạng và phòng thủ", 2, 1 },
                    { "INRE431084", "Truy tìm thông tin", 2, 1 },
                    { "ISAD330384", "Phân tích và thiết kế hệ thống thông tin", 2, 1 },
                    { "MOPR331279", "Lập trình di động", 2, 1 },
                    { "MTSE431179", "Các công nghệ phần mềm mới", 2, 1 },
                    { "NLPR431585", "Xử lý ngôn ngữ tự nhiên", 2, 1 },
                    { "NPRO430980", "Lập trình mạng", 2, 1 },
                    { "NSEC430880", "An ninh mạng", 2, 1 },
                    { "OOSD330879", "Thiết kế phần mềm hướng đối tượng", 2, 1 },
                    { "PCOM331285", "Tính toán song song", 2, 1 },
                    { "POCN451280", "Tiểu luận chuyên ngành Mạng và an ninh mạng", 5, 0 },
                    { "POSE451479", "Tiểu luận chuyên ngành CNPM", 5, 0 },
                    { "RELE431685", "Học tăng cường", 2, 1 },
                    { "SEEN431579", "Search Engine", 2, 1 },
                    { "SOPM431679", "Quản lý dự án phần mềm", 2, 1 },
                    { "SOTE431079", "Kiểm thử phần mềm", 2, 1 },
                    { "TOEN430979", "Công cụ và môi trường phát triển PM", 2, 1 },
                    { "WESE331479", "Bảo mật web", 2, 1 },
                    { "WISE432380", "An toàn mạng không dây và di động", 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "NamHocs",
                columns: new[] { "MaNamHoc", "NamBatDau", "NamKetThuc" },
                values: new object[,]
                {
                    { 1, 2022, 2023 },
                    { 2, 2023, 2024 },
                    { 3, 2024, 2025 }
                });

            migrationBuilder.InsertData(
                table: "NguoiQuanLyPhongMays",
                columns: new[] { "MaNguoiQuanLy", "Email", "GioiTinh", "MatKhau", "NgaySinh", "SoDienThoai", "TenNguoiQuanLy", "TrangThai" },
                values: new object[,]
                {
                    { "NQL001", "nguyenvanq@ute.edu.vn", "Nam", "123456789", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456789", "Nguyễn Văn Q", true },
                    { "NQL002", "tranthip@ute.edu.vn", "Nữ", "123456789", new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "0987654321", "Trần Thị P", true },
                    { "NQL003", "lethir@ute.edu.vn", "Nữ", "123456789", new DateTime(1978, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "0912345678", "Lê Thị R", true },
                    { "NQL004", "phamvans@ute.edu.vn", "Nam", "123456789", new DateTime(1990, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "0908765432", "Phạm Văn S", true }
                });

            migrationBuilder.InsertData(
                table: "SinhViens",
                columns: new[] { "MaSinhVien", "Email", "GioiTinh", "MatKhau", "NgaySinh", "SoDienThoai", "TenSinhVien", "TrangThai" },
                values: new object[,]
                {
                    { "16110052", "16110052@ute.edu.vn", "Nam", "123456789", new DateTime(2000, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456790", "Vũ Tấn Đạt", true },
                    { "16110195", "16110195@ute.edu.vn", "Nam", "123456789", new DateTime(2000, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456791", "Nguyễn Ngọc Quý", true },
                    { "16110236", "16110236@ute.edu.vn", "Nam", "123456789", new DateTime(2000, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456792", "Võ Châu Nhật Trường", true },
                    { "16110250", "16110250@ute.edu.vn", "Nam", "123456789", new DateTime(2000, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456789", "Nguyễn Hoàng Thanh Tùng", true },
                    { "17116145", "17116145@ute.edu.vn", "Nam", "123456789", new DateTime(2000, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456781", "Lê Nguyễn Phúc Vĩnh", true },
                    { "17116192", "17116192@ute.edu.vn", "Nữ", "123456789", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456780", "Trần Kim Ngân", true },
                    { "18116069", "18116069@ute.edu.vn", "Nam", "123456789", new DateTime(2000, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456784", "Phạm Gia Huy", true },
                    { "18116095", "18116095@ute.edu.vn", "Nam", "123456789", new DateTime(2000, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456783", "Lê Đại Nghĩa", true },
                    { "18116141", "18116141@ute.edu.vn", "Nữ", "123456789", new DateTime(2000, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456782", "Nguyễn Lê Diễm Tú", true },
                    { "19116026", "19116026@ute.edu.vn", "Nữ", "123456789", new DateTime(2001, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456793", "Nguyễn Thụy Quế Ngân", true },
                    { "19116157", "19116157@ute.edu.vn", "Nam", "123456789", new DateTime(2001, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456798", "Lý Chánh Bình", true },
                    { "19116162", "19116162@ute.edu.vn", "Nữ", "123456789", new DateTime(2000, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456786", "Trương Thị Thùy Dương", true },
                    { "19116164", "19116164@ute.edu.vn", "Nữ", "123456789", new DateTime(2000, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456785", "Nguyễn Thị Xuân Đào", true },
                    { "19116165", "19116165@ute.edu.vn", "Nam", "123456789", new DateTime(2000, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456787", "Vũ Trọng Đạt", true },
                    { "19116185", "19116185@ute.edu.vn", "Nữ", "123456789", new DateTime(2000, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456791", "Tiêu Kim Loan", true },
                    { "19116190", "19116190@ute.edu.vn", "Nữ", "123456789", new DateTime(2001, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456796", "Nguyễn Thị Na", true },
                    { "19116196", "19116196@ute.edu.vn", "Nữ", "123456789", new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456792", "Nguyễn Thị Thanh Nhàn", true },
                    { "19116207", "19116207@ute.edu.vn", "Nam", "123456789", new DateTime(2000, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456790", "Bùi Nhật Phúc", true },
                    { "19116215", "19116215@ute.edu.vn", "Nữ", "123456789", new DateTime(2001, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456799", "Nguyễn Thị Hồng Thắm", true },
                    { "19116227", "19116227@ute.edu.vn", "Nữ", "123456789", new DateTime(2000, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456788", "Nguyễn Ngọc Quế Trân", true },
                    { "19116230", "19116230@ute.edu.vn", "Nữ", "123456789", new DateTime(2001, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456795", "Huỳnh Thị Bích Tuyền", true },
                    { "19116231", "19116231@ute.edu.vn", "Nữ", "123456789", new DateTime(2001, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456794", "Nguyễn Phương Uyên", true },
                    { "19116232", "19116232@ute.edu.vn", "Nữ", "123456789", new DateTime(2001, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456797", "Lê Thị Thúy Uyển", true },
                    { "19116237", "19116237@ute.edu.vn", "Nữ", "123456789", new DateTime(2000, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456789", "Lê Hà Hoàng Yến", true }
                });

            migrationBuilder.InsertData(
                table: "LopHocPhans",
                columns: new[] { "MaLopHocPhan", "MaHocKy", "MaMonHoc", "MaNamHoc", "SoSinhVienToiDa" },
                values: new object[,]
                {
                    { "LHP001", 1, "TOEN430979", 1, 30 },
                    { "LHP002", 1, "SOPM431679", 1, 25 },
                    { "LHP003", 2, "ADMP431879", 1, 20 },
                    { "LHP004", 2, "ADPL331379", 2, 35 }
                });

            migrationBuilder.InsertData(
                table: "PhongMays",
                columns: new[] { "MaPhongMay", "MaNguoiQuanLy", "TinhTrangPhongMay", "TrangThai" },
                values: new object[,]
                {
                    { "PM001", "NQL001", "Hoạt động tốt", true },
                    { "PM002", "NQL002", "Đang bảo trì", false },
                    { "PM003", "NQL003", "Hoạt động tốt", true },
                    { "PM004", "NQL004", "Đang bảo trì", false }
                });

            migrationBuilder.InsertData(
                table: "ThongBaos",
                columns: new[] { "MaThongBao", "MaNguoiQuanLy", "NoiDung", "ThoiGianDang", "TieuDe" },
                values: new object[,]
                {
                    { 1, "NQL001", "<p>Phòng máy PM001 sẽ được bảo trì vào ngày 10/07/2023.</p>", new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thông báo bảo trì phòng máy" },
                    { 2, "NQL002", "<p>Lịch học kỳ mới đã được cập nhật trên hệ thống. Vui lòng kiểm tra chi tiết.</p>", new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thông báo lịch học kỳ mới" },
                    { 3, "NQL003", "<p>Kỳ thi giữa kỳ sẽ diễn ra từ ngày 20/07/2023 đến 30/07/2023.</p>", new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thông báo thi giữa kỳ" },
                    { 4, "NQL004", "<p>Hội thảo về công nghệ mới sẽ được tổ chức vào ngày 25/07/2023 tại hội trường lớn.</p>", new DateTime(2023, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thông báo hội thảo công nghệ" }
                });

            migrationBuilder.InsertData(
                table: "MayTinhs",
                columns: new[] { "MaMay", "MaPhong", "TinhTrangMayTinh", "TrangThai" },
                values: new object[,]
                {
                    { "MT001", "PM001", "Hoạt động tốt", true },
                    { "MT002", "PM002", "Đang bảo trì", false },
                    { "MT003", "PM003", "Hoạt động tốt", true },
                    { "MT004", "PM004", "Đang bảo trì", false }
                });

            migrationBuilder.InsertData(
                table: "PhieuDangKyGVs",
                columns: new[] { "Id", "GhiChu", "LyDo", "MaGiangVien", "MaLopHocPhan", "MaPhongMay", "NgayDangKy", "NgaySuDung", "TietBatDau", "TietKetThuc", "TrangThai" },
                values: new object[,]
                {
                    { 1, "Không có", "Sử dụng phòng máy cho lớp học", "GV001", "LHP001", "PM001", new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, true },
                    { 2, "Không có", "Sử dụng phòng máy cho nghiên cứu", "GV002", "LHP002", "PM002", new DateTime(2023, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 6, true }
                });

            migrationBuilder.InsertData(
                table: "PhieuDangKySVs",
                columns: new[] { "Id", "GhiChu", "LyDo", "MaPhongMay", "MaSinhVien", "NgayDangKy", "NgaySuDung", "ThoiGianBatDau", "ThoiGianKetThuc", "TrangThai" },
                values: new object[,]
                {
                    { 1, "Mang theo laptop", "Làm bài tập nhóm", "PM001", "16110250", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0), new TimeSpan(0, 11, 0, 0, 0), true },
                    { 2, "Mang theo tài liệu", "Nghiên cứu dự án", "PM002", "16110052", new DateTime(2024, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 13, 0, 0, 0), new TimeSpan(0, 15, 0, 0, 0), true }
                });

            migrationBuilder.InsertData(
                table: "SinhVienLopHocPhans",
                columns: new[] { "Id", "MaLopHocPhan", "MaSinhVien" },
                values: new object[,]
                {
                    { 1, "LHP001", "16110250" },
                    { 2, "LHP002", "16110052" },
                    { 3, "LHP003", "16110195" },
                    { 4, "LHP004", "16110236" },
                    { 5, "LHP001", "17116192" },
                    { 6, "LHP002", "17116145" },
                    { 7, "LHP003", "18116141" },
                    { 8, "LHP004", "18116095" },
                    { 9, "LHP001", "18116069" },
                    { 10, "LHP002", "19116164" },
                    { 11, "LHP003", "19116162" },
                    { 12, "LHP004", "19116165" },
                    { 13, "LHP001", "19116227" },
                    { 14, "LHP002", "19116237" },
                    { 15, "LHP003", "19116207" },
                    { 16, "LHP004", "19116185" },
                    { 17, "LHP001", "19116196" },
                    { 18, "LHP002", "19116026" },
                    { 19, "LHP003", "19116231" },
                    { 20, "LHP004", "19116230" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LopHocPhans_MaHocKy",
                table: "LopHocPhans",
                column: "MaHocKy");

            migrationBuilder.CreateIndex(
                name: "IX_LopHocPhans_MaMonHoc",
                table: "LopHocPhans",
                column: "MaMonHoc");

            migrationBuilder.CreateIndex(
                name: "IX_LopHocPhans_MaNamHoc",
                table: "LopHocPhans",
                column: "MaNamHoc");

            migrationBuilder.CreateIndex(
                name: "IX_MayTinhs_MaPhong",
                table: "MayTinhs",
                column: "MaPhong");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuDangKyGVs_MaGiangVien",
                table: "PhieuDangKyGVs",
                column: "MaGiangVien");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuDangKyGVs_MaLopHocPhan",
                table: "PhieuDangKyGVs",
                column: "MaLopHocPhan");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuDangKyGVs_MaPhongMay",
                table: "PhieuDangKyGVs",
                column: "MaPhongMay");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuDangKySVs_MaPhongMay",
                table: "PhieuDangKySVs",
                column: "MaPhongMay");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuDangKySVs_MaSinhVien",
                table: "PhieuDangKySVs",
                column: "MaSinhVien");

            migrationBuilder.CreateIndex(
                name: "IX_PhongMays_MaNguoiQuanLy",
                table: "PhongMays",
                column: "MaNguoiQuanLy");

            migrationBuilder.CreateIndex(
                name: "IX_SinhVienLopHocPhans_MaLopHocPhan",
                table: "SinhVienLopHocPhans",
                column: "MaLopHocPhan");

            migrationBuilder.CreateIndex(
                name: "IX_SinhVienLopHocPhans_MaSinhVien",
                table: "SinhVienLopHocPhans",
                column: "MaSinhVien");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBaos_MaNguoiQuanLy",
                table: "ThongBaos",
                column: "MaNguoiQuanLy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "MayTinhs");

            migrationBuilder.DropTable(
                name: "PhieuDangKyGVs");

            migrationBuilder.DropTable(
                name: "PhieuDangKySVs");

            migrationBuilder.DropTable(
                name: "SinhVienLopHocPhans");

            migrationBuilder.DropTable(
                name: "ThongBaos");

            migrationBuilder.DropTable(
                name: "GiangViens");

            migrationBuilder.DropTable(
                name: "PhongMays");

            migrationBuilder.DropTable(
                name: "LopHocPhans");

            migrationBuilder.DropTable(
                name: "SinhViens");

            migrationBuilder.DropTable(
                name: "NguoiQuanLyPhongMays");

            migrationBuilder.DropTable(
                name: "HocKys");

            migrationBuilder.DropTable(
                name: "MonHocs");

            migrationBuilder.DropTable(
                name: "NamHocs");
        }
    }
}
