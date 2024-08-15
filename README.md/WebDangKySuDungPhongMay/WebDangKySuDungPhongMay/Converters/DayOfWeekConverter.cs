namespace WebDangKySuDungPhongMay.Converters
{
    public static class DayOfWeekConverter
    {
        public static string ConvertToVietnamese(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "2";
                case DayOfWeek.Tuesday:
                    return "3";
                case DayOfWeek.Wednesday:
                    return "4";
                case DayOfWeek.Thursday:
                    return "5";
                case DayOfWeek.Friday:
                    return "6";
                case DayOfWeek.Saturday:
                    return "7";
                case DayOfWeek.Sunday:
                    return "CN";
                default:
                    return dayOfWeek.ToString();
            }
        }
    }

}
