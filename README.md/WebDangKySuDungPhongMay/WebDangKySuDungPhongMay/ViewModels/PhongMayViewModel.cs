using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

public class PhongMayViewModel
{
    public string MaPhongMay { get; set; }
    public string TinhTrangPhongMay { get; set; }
    public string? MaNguoiQuanLy { get; set; }
    public bool TrangThai { get; set; }
}
