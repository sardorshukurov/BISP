namespace Welisten.Common.Extensions;

public static class DateTimeExtensions
{
    public static string FormatDate(this DateTime dateTime)
    {
        var timeSpan = DateTime.Now - dateTime;
        
        if (timeSpan.TotalMinutes < 60)
        {
            var minutes = (int)timeSpan.TotalMinutes;
            return $"{minutes}m ago";
        }

        if (timeSpan.TotalHours < 24)
        {
            var hours = (int)timeSpan.TotalHours;
            return $"{hours}h ago";
        }

        if (timeSpan.TotalDays < 7)
        {
            var days = (int)timeSpan.TotalDays;
            return $"{days}d ago";
        }

        if (timeSpan.TotalDays < 30)
        {
            var weeks = (int)(timeSpan.TotalDays / 7);
            return $"{weeks}w ago";
        }

        return dateTime.ToString("dd MMMM");
    }
}