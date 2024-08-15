using System.ComponentModel.DataAnnotations;

namespace WebDangKySuDungPhongMay.Models
{
    public class SinhVienLopHocPhan
    {
        public int Id { get; set; }
        public string MaSinhVien { get; set; }
        public string MaLopHocPhan { get; set; }
        public SinhVien SinhVien { get; set; }
        public LopHocPhan LopHocPhan { get; set; }
     
    }
}
