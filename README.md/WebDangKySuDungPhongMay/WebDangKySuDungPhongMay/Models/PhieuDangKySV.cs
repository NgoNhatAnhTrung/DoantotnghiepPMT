using System;

namespace WebDangKySuDungPhongMay.Models
{
    public class PhieuDangKySV
    {
        public int Id { get; set; }
        public string MaSinhVien { get; set; }
        public DateTime NgayDangKy { get; set; }
        public DateTime NgaySuDung { get; set; }
        public TimeSpan ThoiGianBatDau { get; set; }
        public TimeSpan ThoiGianKetThuc { get; set; }
        public string LyDo { get; set; }
        public string GhiChu { get; set; }
        public bool TrangThai { get; set; }
        public string MaPhongMay { get; set; }

        public SinhVien SinhVien { get; set; }
        public PhongMay PhongMay { get; set; }
    }
}
