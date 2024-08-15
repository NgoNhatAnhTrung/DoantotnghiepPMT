using System;
using System.ComponentModel.DataAnnotations;

namespace WebDangKySuDungPhongMay.ViewModels
{
    public class NguoiQuanLyPhongMayViewModel
    {
        public string MaNguoiQuanLy { get; set; }
        public string TenNguoiQuanLy { get; set; }
        public string? MatKhau { get; set; }
        public DateTime NgaySinh { get; set; }

        public string GioiTinh { get; set; }

        public string Email { get; set; }

        public string SoDienThoai { get; set; }
        public bool TrangThai { get; set; }
    }
}
