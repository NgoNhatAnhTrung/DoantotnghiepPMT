namespace WebDangKySuDungPhongMay.Models
{
    public class SinhVien
    {
        public string MaSinhVien { get; set; }
        public string TenSinhVien { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string MatKhau { get; set; }
        public bool TrangThai { get; set; }

        public ICollection<PhieuDangKySV> PhieuDangKySVs { get; set; }
        public ICollection<SinhVienLopHocPhan> SinhVienLopHocPhans { get; set; }
    }
}
