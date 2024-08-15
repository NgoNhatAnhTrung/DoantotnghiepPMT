using System;
using System.ComponentModel.DataAnnotations;

namespace WebDangKySuDungPhongMay.ViewModels
{
    public class GiangVienViewModel
    {
        public string MaGiangVien { get; set; }

        public string HoTen { get; set; }

        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }

        public string Email { get; set; }

        public string SoDienThoai { get; set; }

        public string? MatKhau { get; set; }

        public bool TrangThai { get; set; }
    }
}
