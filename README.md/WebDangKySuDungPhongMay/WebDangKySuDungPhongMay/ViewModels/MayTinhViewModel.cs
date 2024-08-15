using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

public class MayTinhViewModel
{
    public string MaMay { get; set; }

    public string MaPhong { get; set; }
    public string TinhTrangMayTinh { get; set; }
    public bool TrangThai { get; set; }

}
