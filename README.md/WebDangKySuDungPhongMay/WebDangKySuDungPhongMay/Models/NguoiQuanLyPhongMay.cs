namespace WebDangKySuDungPhongMay.Models
{
    public class NguoiQuanLyPhongMay
    {
        public string MaNguoiQuanLy { get; set; }
        public string TenNguoiQuanLy { get; set; }
        public string MatKhau { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public bool TrangThai { get; set; }

        public ICollection<PhongMay> PhongMays { get; set; }
        public ICollection<ThongBao> ThongBaos { get; set; }
    }
}
