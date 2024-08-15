using System.ComponentModel.DataAnnotations;

namespace WebDangKySuDungPhongMay.Models
{
    public class MonHoc
    {
      
        public string MaMonHoc { get; set; }

     
        public string TenMonHoc { get; set; }
      
        public int TinChiLyThuyet { get; set; }
    
        public int TinChiThucHanh { get; set; }
        public ICollection<LopHocPhan> LopHocPhans { get; set; }
    }
}
