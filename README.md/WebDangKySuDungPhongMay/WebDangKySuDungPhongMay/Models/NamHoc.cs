namespace WebDangKySuDungPhongMay.Models
{
    public class NamHoc
    {
        public int MaNamHoc { get; set; }
        public int NamBatDau { get; set; }
        public int NamKetThuc { get; set; }
        public ICollection<LopHocPhan> LopHocPhans { get; set; }
     
    }
}
