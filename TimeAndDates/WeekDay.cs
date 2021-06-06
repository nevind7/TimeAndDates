using System;

using TimeAndDates.Interfaces;

namespace TimeAndDates
{
    public class WeekDay : IWeekDay
    {
        // ----------------------------------------------------------------------
        public DayOfWeek DayOfWeek { get; set; }

        public TimeRange Hours { get; set; }

        // ----------------------------------------------------------------------
        public WeekDay(DayOfWeek dayOfWeek)
        {
            DayOfWeek = dayOfWeek;

            var today = ClockProxy.Clock.Now.Date;

            Hours = new TimeRange(today, today.AddHours(23.5));
        } // Day

        // ----------------------------------------------------------------------
        public WeekDay(DayOfWeek dayOfWeek, TimeRange hours)
        {
            DayOfWeek = dayOfWeek;
            Hours = hours;
        } // Day

        public void SetHours(TimeRange hours)
        {
            Hours = hours;
        }

        public void SetHours(DateTime start, DateTime end)
        {
            Hours = new TimeRange(start, end);
        }
    }
}