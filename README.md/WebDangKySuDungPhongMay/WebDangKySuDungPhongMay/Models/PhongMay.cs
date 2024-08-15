namespace WebDangKySuDungPhongMay.Models
{
    public class PhongMay
    {
        public string MaPhongMay { get; set; }
        public string TinhTrangPhongMay { get; set; }
        public string? MaNguoiQuanLy { get; set; }
        public bool TrangThai { get; set; }

        public NguoiQuanLyPhongMay NguoiQuanLyPhongMay { get; set; }
        public ICollection<MayTinh> MayTinhs { get; set; }
        public ICollection<PhieuDangKySV> PhieuDangKySVs { get; set; }
        public ICollection<PhieuDangKyGV> PhieuDangKyGVs { get; set; }
  
    }
}
