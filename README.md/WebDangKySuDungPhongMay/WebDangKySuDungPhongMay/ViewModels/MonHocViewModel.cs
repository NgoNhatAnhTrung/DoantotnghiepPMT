using System.ComponentModel.DataAnnotations;

namespace WebDangKySuDungPhongMay.ViewModels
{
    public class MonHocViewModel
    {

        public string MaMonHoc { get; set; }
        public string TenMonHoc { get; set; }
        public int TinChiLyThuyet { get; set; }
        public int TinChiThucHanh { get; set; }
    }
}
