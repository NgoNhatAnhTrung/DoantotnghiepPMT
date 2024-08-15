namespace WebDangKySuDungPhongMay.Models
{
    public class ThongBao
    {
        public int MaThongBao { get; set; }
        public string MaNguoiQuanLy { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public DateTime ThoiGianDang { get; set; }

        public NguoiQuanLyPhongMay NguoiQuanLyPhongMay { get; set; }
    }
}
