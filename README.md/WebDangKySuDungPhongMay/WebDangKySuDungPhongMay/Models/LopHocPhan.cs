using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebDangKySuDungPhongMay.Models
{
    public class LopHocPhan
    {
        public string MaLopHocPhan { get; set; }
        public int? MaHocKy { get; set; }
        public int? MaNamHoc { get; set; }
        public string MaMonHoc { get; set; }
        public string? MaGiangVien { get; set; }
        public int SoSinhVienToiDa { get; set; }
        

        public HocKy HocKy { get; set; }
        public NamHoc NamHoc { get; set; }
        public MonHoc MonHoc { get; set; }
        public GiangVien GiangVien { get; set; }

        public ICollection<SinhVienLopHocPhan> SinhVienLopHocPhans { get; set; }
        public ICollection<PhieuDangKyGV> PhieuDangKyGVs { get; set; }
    }
}
