using Microsoft.AspNetCore.Mvc.Rendering;
using WebDangKySuDungPhongMay.Models;

namespace WebDangKySuDungPhongMay.ViewModels
{
    public class LopHocPhanSinhVienViewModel
    {
        public string MaLopHocPhan { get; set; }
        public string TenLopHocPhan { get; set; }
    
        public string SelectedSinhVien { get; set; }
    }
}
