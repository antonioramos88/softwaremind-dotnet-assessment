namespace SoftwareMindWeb.Utility
{
    public static class Extensions
    {
        public static string GetYearsMonthsPassedSince(this DateTime hiredDate)
        {
            int years;
            int months;
            int days;
            var today = DateTime.Now;
            for (var i = 1; ; ++i)
            {
                if (hiredDate.AddYears(i) > today)
                {
                    years = i - 1;
                    break;
                }
            }
            for (var i = 1; ; ++i)
            {
                if (hiredDate.AddYears(years).AddMonths(i) > today)
                {
                    months = i - 1;
                    break;
                }
            }
            for (var i = 1; ; ++i)
            {
                if (hiredDate.AddYears(years).AddMonths(months).AddDays(i) > today)
                {
                    days = i - 1;
                    break;
                }
            }
            return $"{years}y - {months}m - {days}d";
        }

        public static string GetTodaysCommaYear(this DateTime current)
        {
            return current.ToString("MMMM dd, yyyy");
        }

        public static long GetPhoneForEntityFromString(this string current)
        {
            return long.Parse(current.Replace(" ", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Replace("-", string.Empty).Replace("+", string.Empty));
        }

        public static string LongNumberToPhoneFormatString(this long current)
        {
            return string.Format("{0:(###) ###-####}", current);
        }
    }
}
