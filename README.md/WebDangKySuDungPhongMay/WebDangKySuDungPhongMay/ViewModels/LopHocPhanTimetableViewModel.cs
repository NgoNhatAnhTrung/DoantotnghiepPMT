namespace WebDangKySuDungPhongMay.ViewModels
{
    public class LopHocPhanTimetableViewModel
    {
        public string MaLopHocPhan { get; set; }
        public string TenMonHoc { get; set; }
        public List<TimetableEntry> TimetableEntries { get; set; }

        public class TimetableEntry
        {
            public string DayOfWeek { get; set; }
            public DateTime Date { get; set; }
            public int StartTime { get; set; }
            public int EndTime { get; set; }
        }
    }
}
