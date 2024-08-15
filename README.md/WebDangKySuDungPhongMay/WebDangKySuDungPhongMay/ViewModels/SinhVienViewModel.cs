using System.ComponentModel.DataAnnotations;

namespace WebDangKySuDungPhongMay.ViewModels
{
    public class SinhVienViewModel
    {
   
        public string MaSinhVien { get; set; }

    
        public string TenSinhVien { get; set; }


        public DateTime NgaySinh { get; set; }

        public string GioiTinh { get; set; }

        public string Email { get; set; }

        public string SoDienThoai { get; set; }

        public string? MatKhau { get; set; }

        public bool TrangThai { get; set; }
    }
}
