using System.ComponentModel.DataAnnotations;

public class LopHocPhanViewModel
{
    public string MaLopHocPhan { get; set; }
    public int? MaHocKy { get; set; }
    public int? MaNamHoc { get; set; }
    public string MaMonHoc { get; set; }
    public string? MaGiangVien { get; set; }
    public int SoSinhVienToiDa { get; set; }
}
