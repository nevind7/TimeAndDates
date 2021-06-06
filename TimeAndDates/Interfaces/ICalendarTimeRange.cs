namespace TimeAndDates.Interfaces
{
    public interface ICalendarTimeRange : ITimeRange
    {
        // ----------------------------------------------------------------------
        ITimeCalendar Calendar { get; }
    } // interface ICalendarTimeRange
}