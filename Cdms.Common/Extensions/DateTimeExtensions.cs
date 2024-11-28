namespace Cdms.Common.Extensions;

public static class DateTimeExtensions
{
    public static DateTime TrimMicroseconds(this DateTime dt)
    {
        return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond, dt.Kind);
    }

    public static DateTime? TrimMicroseconds(this DateTime? dt)
    {
        return dt?.TrimMicroseconds();
    }

    public static DateTime CurrentHour(this DateTime dt)
    {
        return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0, dt.Kind);
    }
    
    public static DateTime NextHour(this DateTime dt)
    {
        return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour+1, 0, 0, dt.Kind);
    }
    
    public static DateTime Yesterday(this DateTime dt)
    {
        return dt.AddDays(-1);
    }
    
    public static DateTime WeekAgo(this DateTime dt)
    {
        return dt.AddDays(-7);
    }
    
    public static DateTime MonthAgo(this DateTime dt)
    {
        return dt.AddMonths(-1);
    }
    private static int CreateRandomInt(int min, int max)
    {
        return Random.Shared.Next(min, max);
    }
    
    public static DateTime RandomTime(this DateTime dt)
    {
        return new DateTime(dt.Year, dt.Month, dt.Day, CreateRandomInt(0,23), CreateRandomInt(0, 60), CreateRandomInt(0, 60), dt.Kind);
    }
}