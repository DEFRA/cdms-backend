namespace Cdms.Types.Ipaffs.Mapping;

public static class DateTimeMapper
{
    public static DateTime? Map(DateOnly? date, TimeOnly? time)
    {
        return date?.ToDateTime(time ?? TimeOnly.MinValue);
    }
}