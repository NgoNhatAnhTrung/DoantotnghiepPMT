namespace WebDangKySuDungPhongMay.Models
{
    public class MayTinh
    {
        public string MaMay { get; set; }
        public string MaPhong { get; set; }
        public string TinhTrangMayTinh { get; set; }
        public bool TrangThai { get; set; }

        public PhongMay PhongMay { get; set; }
    }
}
