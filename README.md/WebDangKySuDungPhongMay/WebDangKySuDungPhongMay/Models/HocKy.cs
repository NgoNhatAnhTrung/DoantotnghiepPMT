using System.ComponentModel.DataAnnotations;

namespace WebDangKySuDungPhongMay.Models
{
    public class HocKy
    {
        public int MaHocKy { get; set; }
        public string TenHocKy { get; set; }

        public ICollection<LopHocPhan> LopHocPhans { get; set; }

    }
}
