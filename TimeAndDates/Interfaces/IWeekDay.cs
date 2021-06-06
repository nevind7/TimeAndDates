using System;

namespace TimeAndDates.Interfaces
{
    public interface IWeekDay
    {
        DayOfWeek DayOfWeek { get; set; }

        TimeRange Hours { get; set; }

        void SetHours(TimeRange hours);

        void SetHours(DateTime start, DateTime end);
    }
}