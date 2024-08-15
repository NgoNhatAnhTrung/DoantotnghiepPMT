using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace WebDangKySuDungPhongMay.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GiangVien> GiangViens { get; set; }
        public DbSet<HocKy> HocKys { get; set; }
        public DbSet<LopHocPhan> LopHocPhans { get; set; }
        public DbSet<MayTinh> MayTinhs { get; set; }
        public DbSet<MonHoc> MonHocs { get; set; }
        public DbSet<NamHoc> NamHocs { get; set; }
        public DbSet<NguoiQuanLyPhongMay> NguoiQuanLyPhongMays { get; set; }
        public DbSet<PhieuDangKyGV> PhieuDangKyGVs { get; set; }
        public DbSet<PhieuDangKySV> PhieuDangKySVs { get; set; }
        public DbSet<PhongMay> PhongMays { get; set; }

        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<SinhVienLopHocPhan> SinhVienLopHocPhans { get; set; }

        public DbSet<ThongBao> ThongBaos { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // GiangVien configuration
            modelBuilder.Entity<GiangVien>(entity =>
            {
                entity.HasKey(e => e.MaGiangVien);
                entity.Property(e => e.MaGiangVien).IsRequired().HasMaxLength(50);
                entity.Property(e => e.HoTen).IsRequired().HasMaxLength(100);
                entity.Property(e => e.NgaySinh).IsRequired().HasColumnType("date");
                entity.Property(e => e.GioiTinh).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.SoDienThoai).HasMaxLength(15);
                entity.Property(e => e.MatKhau).IsRequired().HasMaxLength(128);
                entity.Property(e => e.TrangThai).IsRequired(); // No need to specify the length for boolean

                entity.HasMany(e => e.PhieuDangKyGVs)
                    .WithOne(e => e.GiangVien)
                    .HasForeignKey(e => e.MaGiangVien)
                    .OnDelete(DeleteBehavior.Cascade);

       

                entity.HasData(
                    new GiangVien
                    {
                        MaGiangVien = "GV001",
                        HoTen = "Nguyễn Văn A",
                        NgaySinh = new DateTime(1980, 1, 1),
                        GioiTinh = "Nam",
                        Email = "nguyenvana@ute.edu.vn",
                        SoDienThoai = "0123456789",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new GiangVien
                    {
                        MaGiangVien = "GV002",
                        HoTen = "Trần Thị B",
                        NgaySinh = new DateTime(1985, 5, 15),
                        GioiTinh = "Nữ",
                        Email = "tranthib@ute.edu.vn",
                        SoDienThoai = "0987654321",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new GiangVien
                    {
                        MaGiangVien = "GV003",
                        HoTen = "Lê Thị C",
                        NgaySinh = new DateTime(1978, 9, 22),
                        GioiTinh = "Nữ",
                        Email = "lethic@ute.edu.vn",
                        SoDienThoai = "0912345678",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new GiangVien
                    {
                        MaGiangVien = "GV004",
                        HoTen = "Phạm Văn D",
                        NgaySinh = new DateTime(1990, 12, 30),
                        GioiTinh = "Nam",
                        Email = "phamvand@ute.edu.vn",
                        SoDienThoai = "0908765432",
                        MatKhau = "123456789",
                        TrangThai = true
                    }
                );
            });


            // HocKy configuration
            modelBuilder.Entity<HocKy>(entity =>
            {
                entity.HasKey(e => e.MaHocKy);
                entity.Property(e => e.TenHocKy).IsRequired().HasMaxLength(50);

                entity.HasMany(e => e.LopHocPhans)
                    .WithOne(e => e.HocKy)
                    .HasForeignKey(e => e.MaHocKy)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasData(
                    new HocKy
                    {
                        MaHocKy = 1,
                        TenHocKy = "1"
                    },
                    new HocKy
                    {
                        MaHocKy = 2,
                        TenHocKy = "2"
                    },
                    new HocKy
                    {
                        MaHocKy = 3,
                        TenHocKy = "Hè"
                    }
                );
            });


            // MonHoc configuration
            modelBuilder.Entity<MonHoc>(entity =>
            {
                entity.HasKey(e => e.MaMonHoc);
                entity.Property(e => e.MaMonHoc).IsRequired().HasMaxLength(50);
                entity.Property(e => e.TenMonHoc).IsRequired().HasMaxLength(100);

                entity.HasMany(e => e.LopHocPhans)
                    .WithOne(e => e.MonHoc)
                    .HasForeignKey(e => e.MaMonHoc)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasData(
               
                    new MonHoc
                    {
                        MaMonHoc = "TOEN430979",
                        TenMonHoc = "Công cụ và môi trường phát triển PM",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "SOPM431679",
                        TenMonHoc = "Quản lý dự án phần mềm",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "ADMP431879",
                        TenMonHoc = "Lập trình di động nâng cao",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "ADPL331379",
                        TenMonHoc = "Ngôn ngữ Lập trình tiên tiến",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "DIFO432180",
                        TenMonHoc = "Pháp lý kỹ thuật số",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "WISE432380",
                        TenMonHoc = "An toàn mạng không dây và di động",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "BCAP433280",
                        TenMonHoc = "Blockchain và ứng dụng",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "CLAD432480",
                        TenMonHoc = "Quản trị trên môi trường cloud",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "ADDB331784",
                        TenMonHoc = "Cơ sở dữ liệu Nâng cao",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "DAWH430784",
                        TenMonHoc = "Kho dữ liệu",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "INRE431084",
                        TenMonHoc = "Truy tìm thông tin",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "SEEN431579",
                        TenMonHoc = "Search Engine",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "AIOT331185",
                        TenMonHoc = "Trí tuệ nhân tạo cho IOT",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "PCOM331285",
                        TenMonHoc = "Tính toán song song",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "NLPR431585",
                        TenMonHoc = "Xử lý ngôn ngữ tự nhiên",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "RELE431685",
                        TenMonHoc = "Học tăng cường",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "WESE331479",
                        TenMonHoc = "Bảo mật web",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "OOSD330879",
                        TenMonHoc = "Thiết kế phần mềm hướng đối tượng",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "MOPR331279",
                        TenMonHoc = "Lập trình di động",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "SOTE431079",
                        TenMonHoc = "Kiểm thử phần mềm",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "MTSE431179",
                        TenMonHoc = "Các công nghệ phần mềm mới",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "POSE451479",
                        TenMonHoc = "Tiểu luận chuyên ngành CNPM",
                        TinChiLyThuyet = 5,
                        TinChiThucHanh = 0
                    },
                    new MonHoc
                    {
                        MaMonHoc = "NPRO430980",
                        TenMonHoc = "Lập trình mạng",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "ADNT330580",
                        TenMonHoc = "Mạng máy tính nâng cao",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "ETHA332080",
                        TenMonHoc = "Tấn công mạng và phòng thủ",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "CNDE430780",
                        TenMonHoc = "Thiết kế mạng",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "NSEC430880",
                        TenMonHoc = "An ninh mạng",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "POCN451280",
                        TenMonHoc = "Tiểu luận chuyên ngành Mạng và an ninh mạng",
                        TinChiLyThuyet = 5,
                        TinChiThucHanh = 0
                    },
                    new MonHoc
                    {
                        MaMonHoc = "ISAD330384",
                        TenMonHoc = "Phân tích và thiết kế hệ thống thông tin",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    },
                    new MonHoc
                    {
                        MaMonHoc = "DAMI330484",
                        TenMonHoc = "Khai phá dữ liệu",
                        TinChiLyThuyet = 2,
                        TinChiThucHanh = 1
                    }
                );
            });

            modelBuilder.Entity<NamHoc>(entity =>
            {
                entity.HasKey(e => e.MaNamHoc);
                entity.Property(e => e.NamBatDau).IsRequired();
                entity.Property(e => e.NamKetThuc).IsRequired();

                entity.HasMany(e => e.LopHocPhans)
                    .WithOne(e => e.NamHoc)
                    .HasForeignKey(e => e.MaNamHoc)
                    .OnDelete(DeleteBehavior.SetNull);


                entity.HasData(
                    new NamHoc
                    {
                        MaNamHoc = 1,
                        NamBatDau = 2022,
                        NamKetThuc = 2023
                    },
                    new NamHoc
                    {
                        MaNamHoc = 2,
                        NamBatDau = 2023,
                        NamKetThuc = 2024
                    },
                    new NamHoc
                    {
                        MaNamHoc = 3,
                        NamBatDau = 2024,
                        NamKetThuc = 2025
                    }
                );
            });
            modelBuilder.Entity<SinhVien>(entity =>
            {
                entity.HasKey(e => e.MaSinhVien);
                entity.Property(e => e.MaSinhVien).IsRequired().HasMaxLength(50);
                entity.Property(e => e.TenSinhVien).IsRequired().HasMaxLength(100);
                entity.Property(e => e.NgaySinh).IsRequired().HasColumnType("date");
                entity.Property(e => e.GioiTinh).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.SoDienThoai).HasMaxLength(15);
                entity.Property(e => e.MatKhau).IsRequired().HasMaxLength(100);
                entity.Property(e => e.TrangThai).IsRequired();

                entity.HasMany(e => e.PhieuDangKySVs)
                    .WithOne(e => e.SinhVien)
                    .HasForeignKey(e => e.MaSinhVien)
                    .OnDelete(DeleteBehavior.Cascade); // Cascade delete

                entity.HasMany(e => e.SinhVienLopHocPhans)
                    .WithOne(e => e.SinhVien)
                    .HasForeignKey(e => e.MaSinhVien)
                    .OnDelete(DeleteBehavior.Cascade); // Cascade delete

                entity.HasData(
                    new SinhVien
                    {
                        MaSinhVien = "16110250",
                        TenSinhVien = "Nguyễn Hoàng Thanh Tùng",
                        NgaySinh = new DateTime(2000, 4, 1),
                        GioiTinh = "Nam",
                        Email = "16110250@ute.edu.vn",
                        SoDienThoai = "0123456789",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new SinhVien
                    {
                        MaSinhVien = "16110052",
                        TenSinhVien = "Vũ Tấn Đạt",
                        NgaySinh = new DateTime(2000, 5, 1),
                        GioiTinh = "Nam",
                        Email = "16110052@ute.edu.vn",
                        SoDienThoai = "0123456790",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new SinhVien
                    {
                        MaSinhVien = "16110195",
                        TenSinhVien = "Nguyễn Ngọc Quý",
                        NgaySinh = new DateTime(2000, 6, 1),
                        GioiTinh = "Nam",
                        Email = "16110195@ute.edu.vn",
                        SoDienThoai = "0123456791",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new SinhVien
                    {
                        MaSinhVien = "16110236",
                        TenSinhVien = "Võ Châu Nhật Trường",
                        NgaySinh = new DateTime(2000, 7, 1),
                        GioiTinh = "Nam",
                        Email = "16110236@ute.edu.vn",
                        SoDienThoai = "0123456792",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    // More data
                    new SinhVien
                    {
                        MaSinhVien = "17116192",
                        TenSinhVien = "Trần Kim Ngân",
                        NgaySinh = new DateTime(2000, 1, 1),
                        GioiTinh = "Nữ",
                        Email = "17116192@ute.edu.vn",
                        SoDienThoai = "0123456780",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new SinhVien
                    {
                        MaSinhVien = "17116145",
                        TenSinhVien = "Lê Nguyễn Phúc Vĩnh",
                        NgaySinh = new DateTime(2000, 2, 1),
                        GioiTinh = "Nam",
                        Email = "17116145@ute.edu.vn",
                        SoDienThoai = "0123456781",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new SinhVien
                    {
                        MaSinhVien = "18116141",
                        TenSinhVien = "Nguyễn Lê Diễm Tú",
                        NgaySinh = new DateTime(2000, 3, 1),
                        GioiTinh = "Nữ",
                        Email = "18116141@ute.edu.vn",
                        SoDienThoai = "0123456782",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new SinhVien
                    {
                        MaSinhVien = "18116095",
                        TenSinhVien = "Lê Đại Nghĩa",
                        NgaySinh = new DateTime(2000, 4, 1),
                        GioiTinh = "Nam",
                        Email = "18116095@ute.edu.vn",
                        SoDienThoai = "0123456783",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new SinhVien
                    {
                        MaSinhVien = "18116069",
                        TenSinhVien = "Phạm Gia Huy",
                        NgaySinh = new DateTime(2000, 5, 1),
                        GioiTinh = "Nam",
                        Email = "18116069@ute.edu.vn",
                        SoDienThoai = "0123456784",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new SinhVien
                    {
                        MaSinhVien = "19116164",
                        TenSinhVien = "Nguyễn Thị Xuân Đào",
                        NgaySinh = new DateTime(2000, 6, 1),
                        GioiTinh = "Nữ",
                        Email = "19116164@ute.edu.vn",
                        SoDienThoai = "0123456785",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new SinhVien
                    {
                        MaSinhVien = "19116162",
                        TenSinhVien = "Trương Thị Thùy Dương",
                        NgaySinh = new DateTime(2000, 7, 1),
                        GioiTinh = "Nữ",
                        Email = "19116162@ute.edu.vn",
                        SoDienThoai = "0123456786",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new SinhVien
                    {
                        MaSinhVien = "19116165",
                        TenSinhVien = "Vũ Trọng Đạt",
                        NgaySinh = new DateTime(2000, 8, 1),
                        GioiTinh = "Nam",
                        Email = "19116165@ute.edu.vn",
                        SoDienThoai = "0123456787",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new SinhVien
                    {
                        MaSinhVien = "19116227",
                        TenSinhVien = "Nguyễn Ngọc Quế Trân",
                        NgaySinh = new DateTime(2000, 9, 1),
                        GioiTinh = "Nữ",
                        Email = "19116227@ute.edu.vn",
                        SoDienThoai = "0123456788",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new SinhVien
                    {
                        MaSinhVien = "19116237",
                        TenSinhVien = "Lê Hà Hoàng Yến",
                        NgaySinh = new DateTime(2000, 10, 1),
                        GioiTinh = "Nữ",
                        Email = "19116237@ute.edu.vn",
                        SoDienThoai = "0123456789",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new SinhVien
                    {
                        MaSinhVien = "19116207",
                        TenSinhVien = "Bùi Nhật Phúc",
                        NgaySinh = new DateTime(2000, 11, 1),
                        GioiTinh = "Nam",
                        Email = "19116207@ute.edu.vn",
                        SoDienThoai = "0123456790",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new SinhVien
                    {
                        MaSinhVien = "19116185",
                        TenSinhVien = "Tiêu Kim Loan",
                        NgaySinh = new DateTime(2000, 12, 1),
                        GioiTinh = "Nữ",
                        Email = "19116185@ute.edu.vn",
                        SoDienThoai = "0123456791",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new SinhVien
                    {
                        MaSinhVien = "19116196",
                        TenSinhVien = "Nguyễn Thị Thanh Nhàn",
                        NgaySinh = new DateTime(2001, 1, 1),
                        GioiTinh = "Nữ",
                        Email = "19116196@ute.edu.vn",
                        SoDienThoai = "0123456792",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new SinhVien
                    {
                        MaSinhVien = "19116026",
                        TenSinhVien = "Nguyễn Thụy Quế Ngân",
                        NgaySinh = new DateTime(2001, 2, 1),
                        GioiTinh = "Nữ",
                        Email = "19116026@ute.edu.vn",
                        SoDienThoai = "0123456793",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new SinhVien
                    {
                        MaSinhVien = "19116231",
                        TenSinhVien = "Nguyễn Phương Uyên",
                        NgaySinh = new DateTime(2001, 3, 1),
                        GioiTinh = "Nữ",
                        Email = "19116231@ute.edu.vn",
                        SoDienThoai = "0123456794",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new SinhVien
                    {
                        MaSinhVien = "19116230",
                        TenSinhVien = "Huỳnh Thị Bích Tuyền",
                        NgaySinh = new DateTime(2001, 4, 1),
                        GioiTinh = "Nữ",
                        Email = "19116230@ute.edu.vn",
                        SoDienThoai = "0123456795",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new SinhVien
                    {
                        MaSinhVien = "19116190",
                        TenSinhVien = "Nguyễn Thị Na",
                        NgaySinh = new DateTime(2001, 5, 1),
                        GioiTinh = "Nữ",
                        Email = "19116190@ute.edu.vn",
                        SoDienThoai = "0123456796",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new SinhVien
                    {
                        MaSinhVien = "19116232",
                        TenSinhVien = "Lê Thị Thúy Uyển",
                        NgaySinh = new DateTime(2001, 6, 1),
                        GioiTinh = "Nữ",
                        Email = "19116232@ute.edu.vn",
                        SoDienThoai = "0123456797",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new SinhVien
                    {
                        MaSinhVien = "19116157",
                        TenSinhVien = "Lý Chánh Bình",
                        NgaySinh = new DateTime(2001, 7, 1),
                        GioiTinh = "Nam",
                        Email = "19116157@ute.edu.vn",
                        SoDienThoai = "0123456798",
                        MatKhau = "123456789",
                        TrangThai = true
                    },
                    new SinhVien
                    {
                        MaSinhVien = "19116215",
                        TenSinhVien = "Nguyễn Thị Hồng Thắm",
                        NgaySinh = new DateTime(2001, 8, 1),
                        GioiTinh = "Nữ",
                        Email = "19116215@ute.edu.vn",
                        SoDienThoai = "0123456799",
                        MatKhau = "123456789",
                        TrangThai = true
                    }
                    // Add more students as needed
                );
            });


            // LopHocPhan configuration
            modelBuilder.Entity<LopHocPhan>(entity =>
            {
                entity.HasKey(e => e.MaLopHocPhan);
                entity.Property(e => e.MaLopHocPhan).IsRequired().HasMaxLength(50);
                entity.Property(e => e.MaMonHoc).IsRequired();
                entity.Property(e => e.MaHocKy).IsRequired(false);
                entity.Property(e => e.MaNamHoc).IsRequired(false);
                entity.Property(e => e.MaGiangVien).HasMaxLength(50);
                entity.Property(e => e.SoSinhVienToiDa).IsRequired();


                entity.HasOne(e => e.MonHoc)
                    .WithMany(e => e.LopHocPhans)
                    .HasForeignKey(e => e.MaMonHoc)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.HocKy)
                    .WithMany(e => e.LopHocPhans)
                    .HasForeignKey(e => e.MaHocKy)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.NamHoc)
                    .WithMany(e => e.LopHocPhans)
                    .HasForeignKey(e => e.MaNamHoc)
                    .OnDelete(DeleteBehavior.SetNull);

          

                entity.HasMany(e => e.SinhVienLopHocPhans)
                    .WithOne(e => e.LopHocPhan)
                    .HasForeignKey(e => e.MaLopHocPhan)
                    .OnDelete(DeleteBehavior.Cascade);


                entity.HasOne(e => e.GiangVien)
                      .WithMany(e => e.LopHocPhans)
                      .HasForeignKey(e => e.MaGiangVien)
                      .OnDelete(DeleteBehavior.SetNull);
                entity.HasData(
                    new LopHocPhan
                    {
                        MaLopHocPhan = "LHP001",
                        MaHocKy = 1,
                        MaNamHoc = 1,
                        MaMonHoc = "TOEN430979",
                        SoSinhVienToiDa = 30,
                        MaGiangVien = "GV001"
                    },
                    new LopHocPhan
                    {
                        MaLopHocPhan = "LHP002",
                        MaHocKy = 1,
                        MaNamHoc = 1,
                        MaMonHoc = "SOPM431679",
                        SoSinhVienToiDa = 25,
                        MaGiangVien = "GV002"
                    },
                    new LopHocPhan
                    {
                        MaLopHocPhan = "LHP003",
                        MaHocKy = 2,
                        MaNamHoc = 1,
                        MaMonHoc = "ADMP431879",
                        SoSinhVienToiDa = 20,
                        MaGiangVien = "GV003"
                    },
                    new LopHocPhan
                    {
                        MaLopHocPhan = "LHP004",
                        MaHocKy = 2,
                        MaNamHoc = 2,
                        MaMonHoc = "ADPL331379",
                        SoSinhVienToiDa = 35,
                        MaGiangVien = "GV004"
                    }
                );
            });



            modelBuilder.Entity<NguoiQuanLyPhongMay>(entity =>
            {
                entity.HasKey(e => e.MaNguoiQuanLy);
                entity.Property(e => e.MaNguoiQuanLy).IsRequired().HasMaxLength(50);
                entity.Property(e => e.TenNguoiQuanLy).IsRequired().HasMaxLength(100);
                entity.Property(e => e.MatKhau).IsRequired().HasMaxLength(128);
                entity.Property(e => e.NgaySinh).IsRequired().HasColumnType("date");
                entity.Property(e => e.GioiTinh).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.SoDienThoai).HasMaxLength(15);
                entity.Property(e => e.TrangThai).IsRequired();

                entity.HasMany(e => e.PhongMays)
                    .WithOne(e => e.NguoiQuanLyPhongMay)
                    .HasForeignKey(e => e.MaNguoiQuanLy)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(e => e.ThongBaos)
                    .WithOne(e => e.NguoiQuanLyPhongMay)
                    .HasForeignKey(e => e.MaNguoiQuanLy)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasData(
                    new NguoiQuanLyPhongMay
                    {
                        MaNguoiQuanLy = "NQL001",
                        TenNguoiQuanLy = "Nguyễn Văn Q",
                        MatKhau = "123456789",
                        NgaySinh = new DateTime(1980, 1, 1),
                        GioiTinh = "Nam",
                        Email = "nguyenvanq@ute.edu.vn",
                        SoDienThoai = "0123456789",
                        TrangThai = true
                    },
                    new NguoiQuanLyPhongMay
                    {
                        MaNguoiQuanLy = "NQL002",
                        TenNguoiQuanLy = "Trần Thị P",
                        MatKhau = "123456789",
                        NgaySinh = new DateTime(1985, 5, 15),
                        GioiTinh = "Nữ",
                        Email = "tranthip@ute.edu.vn",
                        SoDienThoai = "0987654321",
                        TrangThai = true
                    },
                    new NguoiQuanLyPhongMay
                    {
                        MaNguoiQuanLy = "NQL003",
                        TenNguoiQuanLy = "Lê Thị R",
                        MatKhau = "123456789",
                        NgaySinh = new DateTime(1978, 9, 22),
                        GioiTinh = "Nữ",
                        Email = "lethir@ute.edu.vn",
                        SoDienThoai = "0912345678",
                        TrangThai = true
                    },
                    new NguoiQuanLyPhongMay
                    {
                        MaNguoiQuanLy = "NQL004",
                        TenNguoiQuanLy = "Phạm Văn S",
                        MatKhau = "123456789",
                        NgaySinh = new DateTime(1990, 12, 30),
                        GioiTinh = "Nam",
                        Email = "phamvans@ute.edu.vn",
                        SoDienThoai = "0908765432",
                        TrangThai = true
                    }
                );
            });

            modelBuilder.Entity<PhongMay>(entity =>
            {
                entity.HasKey(e => e.MaPhongMay);
                entity.Property(e => e.MaPhongMay).IsRequired().HasMaxLength(50);
                entity.Property(e => e.TinhTrangPhongMay).IsRequired().HasMaxLength(100);
                entity.Property(e => e.MaNguoiQuanLy).HasMaxLength(50);
                entity.Property(e => e.TrangThai).IsRequired();

                entity.HasOne(e => e.NguoiQuanLyPhongMay)
                    .WithMany(e => e.PhongMays)
                    .HasForeignKey(e => e.MaNguoiQuanLy)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(e => e.MayTinhs)
                    .WithOne(e => e.PhongMay)
                    .HasForeignKey(e => e.MaPhong)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.PhieuDangKySVs)
                    .WithOne(e => e.PhongMay)
                    .HasForeignKey(e => e.MaPhongMay)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.PhieuDangKyGVs)
                    .WithOne(e => e.PhongMay)
                    .HasForeignKey(e => e.MaPhongMay)
                    .OnDelete(DeleteBehavior.Cascade);

            

                entity.HasData(
                    new PhongMay
                    {
                        MaPhongMay = "PM001",
                        TinhTrangPhongMay = "Hoạt động tốt",
                        MaNguoiQuanLy = "NQL001",
                        TrangThai = true
                    },
                    new PhongMay
                    {
                        MaPhongMay = "PM002",
                        TinhTrangPhongMay = "Đang bảo trì",
                        MaNguoiQuanLy = "NQL002",
                        TrangThai = false
                    },
                    new PhongMay
                    {
                        MaPhongMay = "PM003",
                        TinhTrangPhongMay = "Hoạt động tốt",
                        MaNguoiQuanLy = "NQL003",
                        TrangThai = true
                    },
                    new PhongMay
                    {
                        MaPhongMay = "PM004",
                        TinhTrangPhongMay = "Đang bảo trì",
                        MaNguoiQuanLy = "NQL004",
                        TrangThai = false
                    }
                );
            });

            // MayTinh configuration
            modelBuilder.Entity<MayTinh>(entity =>
            {
                entity.HasKey(e => e.MaMay);
                entity.Property(e => e.MaMay).IsRequired().HasMaxLength(50);
                entity.Property(e => e.MaPhong).IsRequired().HasMaxLength(50);
                entity.Property(e => e.TinhTrangMayTinh).IsRequired().HasMaxLength(100);
                entity.Property(e => e.TrangThai).IsRequired().HasMaxLength(20);

                entity.HasOne(e => e.PhongMay)
                    .WithMany(e => e.MayTinhs)
                    .HasForeignKey(e => e.MaPhong)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasData(
                    new MayTinh
                    {
                        MaMay = "MT001",
                        MaPhong = "PM001",
                        TinhTrangMayTinh = "Hoạt động tốt",
                        TrangThai = true
                    },
                    new MayTinh
                    {
                        MaMay = "MT002",
                        MaPhong = "PM002",
                        TinhTrangMayTinh = "Đang bảo trì",
                        TrangThai = false
                    },
                    new MayTinh
                    {
                        MaMay = "MT003",
                        MaPhong = "PM003",
                        TinhTrangMayTinh = "Hoạt động tốt",
                        TrangThai = true
                    },
                    new MayTinh
                    {
                        MaMay = "MT004",
                        MaPhong = "PM004",
                        TinhTrangMayTinh = "Đang bảo trì",
                        TrangThai = false
                    }
                );
            });



         

            modelBuilder.Entity<PhieuDangKyGV>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MaGiangVien).IsRequired().HasMaxLength(50);
                entity.Property(e => e.NgayDangKy).IsRequired().HasColumnType("date");
                entity.Property(e => e.NgaySuDung).IsRequired().HasColumnType("date");
                entity.Property(e => e.TietBatDau).IsRequired();
                entity.Property(e => e.TietKetThuc).IsRequired();
                entity.Property(e => e.LyDo).IsRequired().HasMaxLength(200);
                entity.Property(e => e.GhiChu).HasMaxLength(200);
                entity.Property(e => e.TrangThai).IsRequired();
                entity.Property(e => e.MaPhongMay).IsRequired().HasMaxLength(50);
                entity.Property(e => e.MaLopHocPhan).IsRequired().HasMaxLength(50);

                entity.HasOne(e => e.GiangVien)
                    .WithMany(e => e.PhieuDangKyGVs)
                    .HasForeignKey(e => e.MaGiangVien)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.PhongMay)
                    .WithMany(e => e.PhieuDangKyGVs)
                    .HasForeignKey(e => e.MaPhongMay)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.LopHocPhan)
                .WithMany(e => e.PhieuDangKyGVs)
                .HasForeignKey(e => e.MaLopHocPhan)
                .OnDelete(DeleteBehavior.Cascade);


                entity.HasData(
                     new PhieuDangKyGV
                     {
                         Id = 1,
                         MaGiangVien = "GV001",
                         NgayDangKy = new DateTime(2023, 8, 1),
                         NgaySuDung = new DateTime(2023, 8, 10),
                         TietBatDau = 1,
                         TietKetThuc = 3,
                         LyDo = "Sử dụng phòng máy cho lớp học",
                         GhiChu = "Không có",
                         TrangThai = true,
                         MaPhongMay = "PM001",
                         MaLopHocPhan = "LHP001"
                     },
                     new PhieuDangKyGV
                     {
                         Id = 2,
                         MaGiangVien = "GV002",
                         NgayDangKy = new DateTime(2023, 8, 2),
                         NgaySuDung = new DateTime(2023, 8, 11),
                         TietBatDau = 4,
                         TietKetThuc = 6,
                         LyDo = "Sử dụng phòng máy cho nghiên cứu",
                         GhiChu = "Không có",
                         TrangThai = true,
                         MaPhongMay = "PM002",
                         MaLopHocPhan = "LHP002"
                     }
                 );
            });


            modelBuilder.Entity<PhieuDangKySV>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MaSinhVien).IsRequired().HasMaxLength(50);
                entity.Property(e => e.NgayDangKy).IsRequired().HasColumnType("date");
                entity.Property(e => e.NgaySuDung).IsRequired().HasColumnType("date");
                entity.Property(e => e.ThoiGianBatDau).IsRequired();
                entity.Property(e => e.ThoiGianKetThuc).IsRequired();
                entity.Property(e => e.LyDo).HasMaxLength(500);
                entity.Property(e => e.GhiChu).HasMaxLength(500);
                entity.Property(e => e.TrangThai).IsRequired();
                entity.Property(e => e.MaPhongMay).IsRequired().HasMaxLength(50);

                entity.HasOne(e => e.SinhVien)
                    .WithMany(sv => sv.PhieuDangKySVs)
                    .HasForeignKey(e => e.MaSinhVien)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.PhongMay)
                    .WithMany(pm => pm.PhieuDangKySVs)
                    .HasForeignKey(e => e.MaPhongMay)
                    .OnDelete(DeleteBehavior.Cascade);

                // Seed data for PhieuDangKySV
                entity.HasData(
                    new PhieuDangKySV
                    {
                        Id = 1,
                        MaSinhVien = "16110250",
                        NgayDangKy = new DateTime(2024, 6, 1),
                        NgaySuDung = new DateTime(2024, 6, 10),
                        ThoiGianBatDau = new TimeSpan(9, 0, 0),
                        ThoiGianKetThuc = new TimeSpan(11, 0, 0),
                        LyDo = "Làm bài tập nhóm",
                        GhiChu = "Mang theo laptop",
                        TrangThai = true,
                        MaPhongMay = "PM001"
                    },
                    new PhieuDangKySV
                    {
                        Id = 2,
                        MaSinhVien = "16110052",
                        NgayDangKy = new DateTime(2024, 6, 2),
                        NgaySuDung = new DateTime(2024, 6, 11),
                        ThoiGianBatDau = new TimeSpan(13, 0, 0),
                        ThoiGianKetThuc = new TimeSpan(15, 0, 0),
                        LyDo = "Nghiên cứu dự án",
                        GhiChu = "Mang theo tài liệu",
                        TrangThai = true,
                        MaPhongMay = "PM002"
                    }
                );
            });

            modelBuilder.Entity<SinhVienLopHocPhan>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MaSinhVien).IsRequired().HasMaxLength(50);
                entity.Property(e => e.MaLopHocPhan).IsRequired().HasMaxLength(50);

                entity.HasOne(e => e.SinhVien)
                    .WithMany(e => e.SinhVienLopHocPhans)
                    .HasForeignKey(e => e.MaSinhVien)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.LopHocPhan)
                    .WithMany(e => e.SinhVienLopHocPhans)
                    .HasForeignKey(e => e.MaLopHocPhan)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasData(
                    new SinhVienLopHocPhan
                    {
                        Id = 1,
                        MaSinhVien = "16110250",
                        MaLopHocPhan = "LHP001"
                    },
                    new SinhVienLopHocPhan
                    {
                        Id = 2,
                        MaSinhVien = "16110052",
                        MaLopHocPhan = "LHP002"
                    },
                    new SinhVienLopHocPhan
                    {
                        Id = 3,
                        MaSinhVien = "16110195",
                        MaLopHocPhan = "LHP003"
                    },
                    new SinhVienLopHocPhan
                    {
                        Id = 4,
                        MaSinhVien = "16110236",
                        MaLopHocPhan = "LHP004"
                    },
                    new SinhVienLopHocPhan
                    {
                        Id = 5,
                        MaSinhVien = "17116192",
                        MaLopHocPhan = "LHP001"
                    },
                    new SinhVienLopHocPhan
                    {
                        Id = 6,
                        MaSinhVien = "17116145",
                        MaLopHocPhan = "LHP002"
                    },
                    new SinhVienLopHocPhan
                    {
                        Id = 7,
                        MaSinhVien = "18116141",
                        MaLopHocPhan = "LHP003"
                    },
                    new SinhVienLopHocPhan
                    {
                        Id = 8,
                        MaSinhVien = "18116095",
                        MaLopHocPhan = "LHP004"
                    },
                    new SinhVienLopHocPhan
                    {
                        Id = 9,
                        MaSinhVien = "18116069",
                        MaLopHocPhan = "LHP001"
                    },
                    new SinhVienLopHocPhan
                    {
                        Id = 10,
                        MaSinhVien = "19116164",
                        MaLopHocPhan = "LHP002"
                    },
                    new SinhVienLopHocPhan
                    {
                        Id = 11,
                        MaSinhVien = "19116162",
                        MaLopHocPhan = "LHP003"
                    },
                    new SinhVienLopHocPhan
                    {
                        Id = 12,
                        MaSinhVien = "19116165",
                        MaLopHocPhan = "LHP004"
                    },
                    new SinhVienLopHocPhan
                    {
                        Id = 13,
                        MaSinhVien = "19116227",
                        MaLopHocPhan = "LHP001"
                    },
                    new SinhVienLopHocPhan
                    {
                        Id = 14,
                        MaSinhVien = "19116237",
                        MaLopHocPhan = "LHP002"
                    },
                    new SinhVienLopHocPhan
                    {
                        Id = 15,
                        MaSinhVien = "19116207",
                        MaLopHocPhan = "LHP003"
                    },
                    new SinhVienLopHocPhan
                    {
                        Id = 16,
                        MaSinhVien = "19116185",
                        MaLopHocPhan = "LHP004"
                    },
                    new SinhVienLopHocPhan
                    {
                        Id = 17,
                        MaSinhVien = "19116196",
                        MaLopHocPhan = "LHP001"
                    },
                    new SinhVienLopHocPhan
                    {
                        Id = 18,
                        MaSinhVien = "19116026",
                        MaLopHocPhan = "LHP002"
                    },
                    new SinhVienLopHocPhan
                    {
                        Id = 19,
                        MaSinhVien = "19116231",
                        MaLopHocPhan = "LHP003"
                    },
                    new SinhVienLopHocPhan
                    {
                        Id = 20,
                        MaSinhVien = "19116230",
                        MaLopHocPhan = "LHP004"
                    }
                );
            });


            // ThongBao configuration
            modelBuilder.Entity<ThongBao>(entity =>
            {
                entity.HasKey(e => e.MaThongBao);
                entity.Property(e => e.MaNguoiQuanLy).IsRequired().HasMaxLength(50);
                entity.Property(e => e.TieuDe).IsRequired().HasMaxLength(200);
                entity.Property(e => e.NoiDung).IsRequired().HasColumnType("ntext");
                entity.Property(e => e.ThoiGianDang).IsRequired();

                entity.HasOne(e => e.NguoiQuanLyPhongMay)
                    .WithMany(e => e.ThongBaos)
                    .HasForeignKey(e => e.MaNguoiQuanLy)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasData(
                    new ThongBao
                    {
                        MaThongBao = 1,
                        MaNguoiQuanLy = "NQL001",
                        TieuDe = "Thông báo bảo trì phòng máy",
                        NoiDung = "Phòng máy PM001 sẽ được bảo trì vào ngày 10/07/2023.",
                        ThoiGianDang = new DateTime(2023, 6, 1)
                    },
                    new ThongBao
                    {
                        MaThongBao = 2,
                        MaNguoiQuanLy = "NQL002",
                        TieuDe = "Thông báo lịch học kỳ mới",
                        NoiDung = "Lịch học kỳ mới đã được cập nhật trên hệ thống. Vui lòng kiểm tra chi tiết.",
                        ThoiGianDang = new DateTime(2023, 6, 15)
                    },
                    new ThongBao
                    {
                        MaThongBao = 3,
                        MaNguoiQuanLy = "NQL003",
                        TieuDe = "Thông báo thi giữa kỳ",
                        NoiDung = "Kỳ thi giữa kỳ sẽ diễn ra từ ngày 20/07/2023 đến 30/07/2023.",
                        ThoiGianDang = new DateTime(2023, 7, 1)
                    },
                    new ThongBao
                    {
                        MaThongBao = 4,
                        MaNguoiQuanLy = "NQL004",
                        TieuDe = "Thông báo hội thảo công nghệ",
                        NoiDung = "Hội thảo về công nghệ mới sẽ được tổ chức vào ngày 25/07/2023 tại hội trường lớn.",
                        ThoiGianDang = new DateTime(2023, 7, 10)
                    }
                );
            });
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.TenDangNhap);
                entity.Property(e => e.TenDangNhap).IsRequired().HasMaxLength(100);
                entity.Property(e => e.MatKhau).IsRequired().HasMaxLength(128);
                entity.Property(e => e.TenHienThi).HasMaxLength(100);
            });

            // Seed data
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    TenDangNhap = "admin",
                    MatKhau = "123456789", 
                    TenHienThi = "Quản lý"
                }
            );


        }
    }
}
