using System;
using System.Globalization;

using TimeAndDates.Enums;

namespace TimeAndDates
{
    // ------------------------------------------------------------------------
    public struct TimeCalendarConfig
    {
        // ----------------------------------------------------------------------
        public CultureInfo Culture { get; set; }

        // ----------------------------------------------------------------------
        public TimeSpan? StartOffset { get; set; }

        // ----------------------------------------------------------------------
        public TimeSpan? EndOffset { get; set; }

        // ----------------------------------------------------------------------
        public YearMonth? YearBaseMonth { get; set; }

        // ----------------------------------------------------------------------
        public YearMonth? FiscalYearBaseMonth { get; set; }

        // ----------------------------------------------------------------------
        public DayOfWeek? FiscalFirstDayOfYear { get; set; }

        // ----------------------------------------------------------------------
        public YearWeekType? YearWeekType { get; set; }

        // ----------------------------------------------------------------------
        public CalendarNameType? DayNameType { get; set; }

        // ----------------------------------------------------------------------
        public CalendarNameType? MonthNameType { get; set; }
    } // struct TimeCalendarConfig
}