using System;

using TimeAndDates.Interfaces;

namespace TimeAndDates
{
    // ------------------------------------------------------------------------
    public sealed class Minute : MinuteTimeRange
    {
        // ----------------------------------------------------------------------
        public Minute() :
            this(new TimeCalendar())
        {
        } // Minute

        // ----------------------------------------------------------------------
        public Minute(ITimeCalendar calendar) :
            this(ClockProxy.Clock.Now, calendar)
        {
        } // Minute

        // ----------------------------------------------------------------------
        public Minute(DateTime moment) :
            this(moment, new TimeCalendar())
        {
        } // Minute

        // ----------------------------------------------------------------------
        public Minute(DateTime moment, ITimeCalendar calendar) :
            this(calendar.GetYear(moment), calendar.GetMonth(moment), calendar.GetDayOfMonth(moment),
            calendar.GetHour(moment), calendar.GetMinute(moment), calendar)
        {
        } // Minute

        // ----------------------------------------------------------------------
        public Minute(int year, int month, int day, int hour, int minute) :
            this(year, month, day, hour, minute, new TimeCalendar())
        {
        } // Minute

        // ----------------------------------------------------------------------
        public Minute(int year, int month, int day, int hour, int minute, ITimeCalendar calendar) :
            base(year, month, day, hour, minute, 1, calendar)
        {
        } // Minute

        // ----------------------------------------------------------------------
        public int Year => StartYear; // Year

        // ----------------------------------------------------------------------
        public int Month => StartMonth; // Month

        // ----------------------------------------------------------------------
        public int Day => StartDay; // Day

        // ----------------------------------------------------------------------
        public int Hour => StartHour; // Hour

        // ----------------------------------------------------------------------
        public int MinuteValue => StartMinute; // MinuteValue

        // ----------------------------------------------------------------------
        public Minute GetPreviousMinute()
        {
            return AddMinutes(-1);
        } // GetPreviousMinute

        // ----------------------------------------------------------------------
        public Minute GetNextMinute()
        {
            return AddMinutes(1);
        } // GetNextMinute

        // ----------------------------------------------------------------------
        public Minute AddMinutes(int minutes)
        {
            DateTime minute = new DateTime(StartYear, StartMonth, StartDay, StartHour, StartMinute, 0);
            return new Minute(minute.AddMinutes(minutes), Calendar);
        } // AddMinutes

        // ----------------------------------------------------------------------
        protected override string Format(ITimeFormatter formatter)
        {
            return formatter.GetCalendarPeriod(formatter.GetShortDate(Start),
                formatter.GetShortTime(Start), formatter.GetShortTime(End), Duration);
        } // Format
    } // class Minute
}