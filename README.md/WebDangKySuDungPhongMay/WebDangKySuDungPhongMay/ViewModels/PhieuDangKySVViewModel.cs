using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebDangKySuDungPhongMay.ViewModels
{
    public class PhieuDangKySVViewModel
    {
        public int Id { get; set; }
        public string? MaSinhVien { get; set; }
        public DateTime? NgayDangKy { get; set; }
        public DateTime NgaySuDung { get; set; }
        public TimeSpan ThoiGianBatDau { get; set; }
        public TimeSpan ThoiGianKetThuc { get; set; }
        public string LyDo { get; set; }
        public string GhiChu { get; set; }
        public string MaPhongMay { get; set; }
        public bool? TrangThai { get; set; }

    }
}
