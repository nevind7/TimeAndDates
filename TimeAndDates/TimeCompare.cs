using System;
using System.Globalization;

using TimeAndDates.enums;

namespace TimeAndDates
{
    public static class TimeCompare
    {
        #region Year

        // ----------------------------------------------------------------------
        public static bool IsSameYear(DateTime left, DateTime right)
        {
            return left.Year == right.Year;
        } // IsSameYear

        // ----------------------------------------------------------------------
        public static bool IsSameYear(YearMonth yearStartMonth, DateTime left, DateTime right)
        {
            return TimeTool.GetYearOf(yearStartMonth, left) == TimeTool.GetYearOf(yearStartMonth, right);
        } // IsSameYear

        #endregion Year

        #region Hafyear

        // ----------------------------------------------------------------------
        public static bool IsSameHalfYear(DateTime left, DateTime right)
        {
            return IsSameHalfYear(TimeSpec.CalendarYearStartMonth, left, right);
        } // IsSameHalfYear

        // ----------------------------------------------------------------------
        public static bool IsSameHalfYear(YearMonth yearStartMonth, DateTime left, DateTime right)
        {
            var leftYear = TimeTool.GetYearOf(yearStartMonth, left);
            var rightYear = TimeTool.GetYearOf(yearStartMonth, right);
            if (leftYear != rightYear)
            {
                return false;
            }

            return TimeTool.GetHalfYearOfMonth(yearStartMonth, (YearMonth)left.Month) == TimeTool.GetHalfYearOfMonth(yearStartMonth, (YearMonth)right.Month);
        } // IsSameHalfYear

        #endregion Hafyear

        #region Quarter

        // ----------------------------------------------------------------------
        public static bool IsSameQuarter(DateTime left, DateTime right)
        {
            return IsSameQuarter(TimeSpec.CalendarYearStartMonth, left, right);
        } // IsSameQuarter

        // ----------------------------------------------------------------------
        public static bool IsSameQuarter(YearMonth yearStartMonth, DateTime left, DateTime right)
        {
            var leftYear = TimeTool.GetYearOf(yearStartMonth, left);
            var rightYear = TimeTool.GetYearOf(yearStartMonth, right);
            if (leftYear != rightYear)
            {
                return false;
            }

            return TimeTool.GetQuarterOfMonth(yearStartMonth, (YearMonth)left.Month) == TimeTool.GetQuarterOfMonth(yearStartMonth, (YearMonth)right.Month);
        } // IsSameQuarter

        #endregion Quarter

        #region Month

        // ----------------------------------------------------------------------
        public static bool IsSameMonth(DateTime left, DateTime right)
        {
            return IsSameYear(left, right) && left.Month == right.Month;
        } // IsSameMonth

        #endregion Month

        #region Week

        // ----------------------------------------------------------------------
        public static bool IsSameWeek(DateTime left, DateTime right, CultureInfo culture, YearWeekType weekType)
        {
            return IsSameWeek(left, right, culture, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek, weekType);
        } // IsSameWeek

        // ----------------------------------------------------------------------
        public static bool IsSameWeek(DateTime left, DateTime right, CultureInfo culture,
            CalendarWeekRule weekRule, DayOfWeek firstDayOfWeek, YearWeekType weekType)
        {
            if (culture == null)
            {
                throw new ArgumentNullException(nameof(culture));
            }

            // left
            TimeTool.GetWeekOfYear(left, culture, weekRule, firstDayOfWeek, weekType, out var leftYear, out var leftWeekOfYear);

            // rught
            TimeTool.GetWeekOfYear(right, culture, weekRule, firstDayOfWeek, weekType, out var rightYear, out var rightWeekOfYear);

            return leftYear == rightYear && leftWeekOfYear == rightWeekOfYear;
        } // IsSameWeek

        #endregion Week

        #region Day

        // ----------------------------------------------------------------------
        public static bool IsSameDay(DateTime left, DateTime right)
        {
            return IsSameMonth(left, right) && left.Day == right.Day;
        } // IsSameDay

        #endregion Day

        #region Hour

        // ----------------------------------------------------------------------
        public static bool IsSameHour(DateTime left, DateTime right)
        {
            return IsSameDay(left, right) && left.Hour == right.Hour;
        } // IsSameHour

        #endregion Hour

        #region Minute

        // ----------------------------------------------------------------------
        public static bool IsSameMinute(DateTime left, DateTime right)
        {
            return IsSameHour(left, right) && left.Minute == right.Minute;
        } // IsSameMinute

        #endregion Minute

        #region Second

        // ----------------------------------------------------------------------
        public static bool IsSameSecond(DateTime left, DateTime right)
        {
            return IsSameMinute(left, right) && left.Second == right.Second;
        } // IsSameSecond

        #endregion Second
    } // class TimeCompare
}