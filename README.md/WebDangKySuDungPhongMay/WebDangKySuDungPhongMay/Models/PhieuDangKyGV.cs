namespace WebDangKySuDungPhongMay.Models
{
    public class PhieuDangKyGV
    {
        public int Id { get; set; }
        public string MaGiangVien { get; set; }
        public DateTime NgayDangKy { get; set; }
        public DateTime NgaySuDung { get; set; }
        public int TietBatDau { get; set; }
        public int TietKetThuc { get; set; }
        public string LyDo { get; set; }
        public string GhiChu { get; set; }
        public bool TrangThai { get; set; }
        public string MaPhongMay { get; set; }
        public string MaLopHocPhan { get; set; }

        public GiangVien GiangVien { get; set; }
        public PhongMay PhongMay { get; set; }
        public LopHocPhan LopHocPhan { get; set; }
    }
}
