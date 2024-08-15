
using System.ComponentModel.DataAnnotations;

namespace WebDangKySuDungPhongMay.ViewModels
{
    public class ThongBaoViewModel
    {
        public int MaThongBao { get; set; }
        public string TieuDe { get; set; }

        public string NoiDung { get; set; }

        public DateTime? ThoiGianDang { get; set; }
        public string MaNguoiQuanLy { get; set; }
    }
}
