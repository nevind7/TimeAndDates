using System;
using System.Globalization;

using TimeAndDates.enums;

namespace TimeAndDates
{
    public static class TimeTool
    {
        #region Date and Time

        // ----------------------------------------------------------------------
        public static DateTime GetDate(DateTime dateTime) => dateTime.Date;  // GetDate

        // ----------------------------------------------------------------------
        public static DateTime SetDate(DateTime from, DateTime to)
        {
            return SetDate(from, to.Year, to.Month, to.Day);
        } // SetDate

        // ----------------------------------------------------------------------
        public static DateTime SetDate(DateTime from, int year, int month = 1, int day = 1)
        {
            return new(year, month, day, from.Hour, from.Minute, from.Second, from.Millisecond);
        } // SetDate

        // ----------------------------------------------------------------------
        public static bool HasTimeOfDay(DateTime dateTime)
        {
            return dateTime.TimeOfDay > TimeSpan.Zero;
        } // HasTimeOfDay

        // ----------------------------------------------------------------------
        public static DateTime SetTimeOfDay(DateTime from, DateTime to)
        {
            return SetTimeOfDay(from, to.Hour, to.Minute, to.Second, to.Millisecond);
        } // SetTimeOfDay

        // ----------------------------------------------------------------------
        public static DateTime SetTimeOfDay(DateTime from, int hour = 0, int minute = 0, int second = 0, int millisecond = 0)
        {
            return new(from.Year, from.Month, from.Day, hour, minute, second, millisecond);
        } // SetTimeOfDay

        #endregion Date and Time

        #region Year

        // ----------------------------------------------------------------------
        public static int GetYearOf(YearMonth yearBaseMonth, DateTime moment)
        {
            return GetYearOf(yearBaseMonth, moment.Year, moment.Month);
        } // GetYearOf

        // ----------------------------------------------------------------------
        public static int GetYearOf(YearMonth yearBaseMonth, int year, int month)
        {
            return month >= (int)yearBaseMonth ? year : year - 1;
        } // GetYearOf

        #endregion Year

        #region HalfYear

        // ----------------------------------------------------------------------
        public static void NextHalfYear(YearHalfYear startHalfYear, out int year, out YearHalfYear halfYear)
        {
            AddHalfYear(startHalfYear, 1, out year, out halfYear);
        } // NextHalfYear

        // ----------------------------------------------------------------------
        public static void PreviousHalfYear(YearHalfYear startHalfYear, out int year, out YearHalfYear halfYear)
        {
            AddHalfYear(startHalfYear, -1, out year, out halfYear);
        } // PreviousHalfYear

        // ----------------------------------------------------------------------
        public static void AddHalfYear(YearHalfYear startHalfYear, int count, out int year, out YearHalfYear halfYear)
        {
            AddHalfYear(0, startHalfYear, count, out year, out halfYear);
        } // AddHalfYear

        // ----------------------------------------------------------------------
        public static void AddHalfYear(int startYear, YearHalfYear startHalfYear, int count, out int year, out YearHalfYear halfYear)
        {
            var offsetYear = (Math.Abs(count) / TimeSpec.HalfYearsPerYear) + 1;
            var startHalfYearCount = ((startYear + offsetYear) * TimeSpec.HalfYearsPerYear) + ((int)startHalfYear - 1);
            var targetHalfYearCount = startHalfYearCount + count;

            year = (targetHalfYearCount / TimeSpec.HalfYearsPerYear) - offsetYear;
            halfYear = (YearHalfYear)((targetHalfYearCount % TimeSpec.HalfYearsPerYear) + 1);
        } // AddHalfYear

        // ----------------------------------------------------------------------
        public static YearHalfYear GetHalfYearOfMonth(YearMonth yearMonth)
        {
            return GetHalfYearOfMonth(TimeSpec.CalendarYearStartMonth, yearMonth);
        } // GetHalfYearOfMonth

        // ----------------------------------------------------------------------
        public static YearHalfYear GetHalfYearOfMonth(YearMonth yearBaseMonth, YearMonth yearMonth)
        {
            var yearMonthIndex = (int)yearMonth - 1;
            var yearStartMonthIndex = (int)yearBaseMonth - 1;
            if (yearMonthIndex < yearStartMonthIndex)
            {
                yearMonthIndex += TimeSpec.MonthsPerYear;
            }
            var deltaMonths = yearMonthIndex - yearStartMonthIndex;
            return (YearHalfYear)((deltaMonths / TimeSpec.MonthsPerHalfYear) + 1);
        } // GetHalfYearOfMonth

        // ----------------------------------------------------------------------
        public static YearMonth[] GetMonthsOfHalfYear(YearHalfYear yearHalfYear)
        {
            switch (yearHalfYear)
            {
                case YearHalfYear.First:
                    return TimeSpec.FirstHalfYearMonths;

                case YearHalfYear.Second:
                    return TimeSpec.SecondHalfYearMonths;
            }
            throw new InvalidOperationException("invalid year halfYear " + yearHalfYear);
        } // GetMonthsOfHalfYear

        #endregion HalfYear

        #region Quarter

        // ----------------------------------------------------------------------
        public static void NextQuarter(YearQuarter startQuarter, out int year, out YearQuarter quarter)
        {
            AddQuarter(startQuarter, 1, out year, out quarter);
        } // NextQuarter

        // ----------------------------------------------------------------------
        public static void PreviousQuarter(YearQuarter startQuarter, out int year, out YearQuarter quarter)
        {
            AddQuarter(startQuarter, -1, out year, out quarter);
        } // PreviousQuarter

        // ----------------------------------------------------------------------
        public static void AddQuarter(YearQuarter startQuarter, int count, out int year, out YearQuarter quarter)
        {
            AddQuarter(0, startQuarter, count, out year, out quarter);
        } // AddQuarter

        // ----------------------------------------------------------------------
        public static void AddQuarter(int startYear, YearQuarter startQuarter, int count, out int year, out YearQuarter quarter)
        {
            var offsetYear = (Math.Abs(count) / TimeSpec.QuartersPerYear) + 1;
            var startQuarterCount = ((startYear + offsetYear) * TimeSpec.QuartersPerYear) + ((int)startQuarter - 1);
            var targetQuarterCount = startQuarterCount + count;

            year = (targetQuarterCount / TimeSpec.QuartersPerYear) - offsetYear;
            quarter = (YearQuarter)((targetQuarterCount % TimeSpec.QuartersPerYear) + 1);
        } // AddQuarter

        // ----------------------------------------------------------------------
        public static YearQuarter GetQuarterOfMonth(YearMonth yearMonth)
        {
            return GetQuarterOfMonth(TimeSpec.CalendarYearStartMonth, yearMonth);
        } // GetQuarterOfMonth

        // ----------------------------------------------------------------------
        public static YearQuarter GetQuarterOfMonth(YearMonth yearBaseMonth, YearMonth yearMonth)
        {
            var yearMonthIndex = (int)yearMonth - 1;
            var yearStartMonthIndex = (int)yearBaseMonth - 1;
            if (yearMonthIndex < yearStartMonthIndex)
            {
                yearMonthIndex += TimeSpec.MonthsPerYear;
            }
            var deltaMonths = yearMonthIndex - yearStartMonthIndex;
            return (YearQuarter)((deltaMonths / TimeSpec.MonthsPerQuarter) + 1);
        } // GetQuarterOfMonth

        // ----------------------------------------------------------------------
        public static YearMonth[] GetMonthsOfQuarter(YearQuarter yearQuarter)
        {
            switch (yearQuarter)
            {
                case YearQuarter.First:
                    return TimeSpec.FirstQuarterMonths;

                case YearQuarter.Second:
                    return TimeSpec.SecondQuarterMonths;

                case YearQuarter.Third:
                    return TimeSpec.ThirdQuarterMonths;

                case YearQuarter.Fourth:
                    return TimeSpec.FourthQuarterMonths;
            }
            throw new InvalidOperationException("invalid year quarter " + yearQuarter);
        } // GetMonthsOfQuarter

        #endregion Quarter

        #region Month

        // ----------------------------------------------------------------------
        public static void NextMonth(YearMonth startMonth, out int year, out YearMonth month)
        {
            AddMonth(startMonth, 1, out year, out month);
        } // NextMonth

        // ----------------------------------------------------------------------
        public static void PreviousMonth(YearMonth startMonth, out int year, out YearMonth month)
        {
            AddMonth(startMonth, -1, out year, out month);
        } // PreviousMonth

        // ----------------------------------------------------------------------
        public static void AddMonth(YearMonth startMonth, int count, out int year, out YearMonth month)
        {
            AddMonth(0, startMonth, count, out year, out month);
        } // AddMonth

        // ----------------------------------------------------------------------
        public static void AddMonth(int startYear, YearMonth startMonth, int count, out int year, out YearMonth month)
        {
            var offsetYear = (Math.Abs(count) / TimeSpec.MonthsPerYear) + 1;
            var startMonthCount = ((startYear + offsetYear) * TimeSpec.MonthsPerYear) + ((int)startMonth - 1);
            var targetMonthCount = startMonthCount + count;

            year = (targetMonthCount / TimeSpec.MonthsPerYear) - offsetYear;
            month = (YearMonth)((targetMonthCount % TimeSpec.MonthsPerYear) + 1);
        } // AddMonth

        // ----------------------------------------------------------------------
        public static int GetDaysInMonth(int year, int month)
        {
            var firstDay = new DateTime(year, month, 1);
            return firstDay.AddMonths(1).AddDays(-1).Day;
        } // GetDaysInMonth

        #endregion Month

        #region Week

        // ----------------------------------------------------------------------
        public static DateTime GetStartOfWeek(DateTime time, DayOfWeek firstDayOfWeek)
        {
            var currentDay = new DateTime(time.Year, time.Month, time.Day);
            while (currentDay.DayOfWeek != firstDayOfWeek)
            {
                currentDay = currentDay.AddDays(-1);
            }
            return currentDay;
        } // GetStartOfWeek

        // ----------------------------------------------------------------------
        public static void GetWeekOfYear(DateTime moment, CultureInfo culture, YearWeekType yearWeekType,
            out int year, out int weekOfYear)
        {
            GetWeekOfYear(moment, culture, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek, yearWeekType,
                out year, out weekOfYear);
        } // GetWeekOfYear

        // ----------------------------------------------------------------------
        public static void GetWeekOfYear(DateTime moment, CultureInfo culture,
            CalendarWeekRule weekRule, DayOfWeek firstDayOfWeek, YearWeekType yearWeekType, out int year, out int weekOfYear)
        {
            if (culture == null)
            {
                throw new ArgumentNullException(nameof(culture));
            }

            if (yearWeekType == YearWeekType.Iso8601 && weekRule == CalendarWeekRule.FirstFourDayWeek)
            {
                // see http://blogs.msdn.com/b/shawnste/archive/2006/01/24/517178.aspx
                var day = culture.Calendar.GetDayOfWeek(moment);
                if (day >= firstDayOfWeek && (int)day <= (int)(firstDayOfWeek + 2) % 7)
                {
                    moment = moment.AddDays(3);
                }
            }

            weekOfYear = culture.Calendar.GetWeekOfYear(moment, weekRule, firstDayOfWeek);
            year = moment.Year;
            if (weekOfYear >= 52 && moment.Month < 12)
            {
                year--;
            }
        } // GetWeekOfYear

        // ----------------------------------------------------------------------
        public static int GetWeeksOfYear(int year, CultureInfo culture, YearWeekType yearWeekType)
        {
            return GetWeeksOfYear(year, culture, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek, yearWeekType);
        } // GetWeeksOfYear

        // ----------------------------------------------------------------------
        public static int GetWeeksOfYear(int year, CultureInfo culture,
            CalendarWeekRule weekRule, DayOfWeek firstDayOfWeek, YearWeekType yearWeekType)
        {
            if (culture == null)
            {
                throw new ArgumentNullException(nameof(culture));
            }

            int currentYear;
            int currentWeek;
            var currentDay = new DateTime(year, 12, 31);
            GetWeekOfYear(currentDay, culture, weekRule, firstDayOfWeek, yearWeekType, out currentYear, out currentWeek);
            while (currentYear != year)
            {
                currentDay = currentDay.AddDays(-1);
                GetWeekOfYear(currentDay, culture, weekRule, firstDayOfWeek, yearWeekType, out currentYear, out currentWeek);
            }
            return currentWeek;
        } // GetWeeksOfYear

        // ----------------------------------------------------------------------
        public static DateTime GetStartOfYearWeek(int year, int weekOfYear, CultureInfo culture, YearWeekType yearWeekType)
        {
            if (culture == null)
            {
                throw new ArgumentNullException(nameof(culture));
            }
            return GetStartOfYearWeek(year, weekOfYear, culture,
                culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek, yearWeekType);
        } // GetStartOfYearWeek

        // ----------------------------------------------------------------------
        public static DateTime GetStartOfYearWeek(int year, int weekOfYear, CultureInfo culture,
            CalendarWeekRule weekRule, DayOfWeek firstDayOfWeek, YearWeekType yearWeekType)
        {
            if (culture == null)
            {
                throw new ArgumentNullException(nameof(culture));
            }
            if (weekOfYear < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(weekOfYear));
            }

            var dateTime = new DateTime(year, 1, 1).AddDays(weekOfYear * TimeSpec.DaysPerWeek);
            int currentYear;
            int currentWeek;
            GetWeekOfYear(dateTime, culture, weekRule, firstDayOfWeek, yearWeekType, out currentYear, out currentWeek);

            // end date of week
            while (currentWeek != weekOfYear)
            {
                dateTime = dateTime.AddDays(-1);
                GetWeekOfYear(dateTime, culture, weekRule, firstDayOfWeek, yearWeekType, out currentYear, out currentWeek);
            }

            // end of previous week
            while (currentWeek == weekOfYear)
            {
                dateTime = dateTime.AddDays(-1);
                GetWeekOfYear(dateTime, culture, weekRule, firstDayOfWeek, yearWeekType, out currentYear, out currentWeek);
            }

            return dateTime.AddDays(1);
        } // GetStartOfYearWeek

        #endregion Week

        #region Day

        // ----------------------------------------------------------------------
        public static DateTime DayStart(DateTime dateTime)
        {
            return dateTime.Date;
        } // DayStart

        // ----------------------------------------------------------------------
        public static DayOfWeek NextDay(DayOfWeek day)
        {
            return AddDays(day, 1);
        } // NextMonth

        // ----------------------------------------------------------------------
        public static DayOfWeek PreviousDay(DayOfWeek day)
        {
            return AddDays(day, -1);
        } // PreviousDay

        // ----------------------------------------------------------------------
        public static DayOfWeek AddDays(DayOfWeek day, int days)
        {
            if (days == 0)
            {
                return day;
            }
            var weeks = (Math.Abs(days) / TimeSpec.DaysPerWeek) + 1;

            var offset = weeks * TimeSpec.DaysPerWeek + (int)day;
            var targetOffset = offset + days;
            return (DayOfWeek)(targetOffset % TimeSpec.DaysPerWeek);
        } // AddMonths

        #endregion Day
    } // class TimeTool
}