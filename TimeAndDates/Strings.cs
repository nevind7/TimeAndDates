using System;
using System.Globalization;
using System.Resources;
using TimeAndDates.Enums;

namespace TimeAndDates
{
    internal sealed class Strings
    {
        #region Year

        // ----------------------------------------------------------------------
        public static string SystemYearName(int year)
        {
            return Format(Inst.GetString("SystemYearName"), year);
        } // SystemYearName

        // ----------------------------------------------------------------------
        public static string CalendarYearName(int year)
        {
            return Format(Inst.GetString("CalendarYearName"), year);
        } // CalendarYearName

        // ----------------------------------------------------------------------
        public static string FiscalYearName(int year)
        {
            return Format(Inst.GetString("FiscalYearName"), year);
        } // FiscalYearName

        // ----------------------------------------------------------------------
        public static string SchoolYearName(int year)
        {
            return Format(Inst.GetString("SchoolYearName"), year);
        } // SchoolYearName

        #endregion Year

        #region HalfYear

        // ----------------------------------------------------------------------
        public static string SystemHalfYearName(YearHalfYear yearHalfYear)
        {
            return Format(Inst.GetString("SystemHalfYearName"), (int)yearHalfYear);
        } // SystemHalfYearName

        // ----------------------------------------------------------------------
        public static string CalendarHalfYearName(YearHalfYear yearHalfYear)
        {
            return Format(Inst.GetString("CalendarHalfYearName"), (int)yearHalfYear);
        } // CalendarHalfYearName

        // ----------------------------------------------------------------------
        public static string FiscalHalfYearName(YearHalfYear yearHalfYear)
        {
            return Format(Inst.GetString("FiscalHalfYearName"), (int)yearHalfYear);
        } // FiscalHalfYearName

        // ----------------------------------------------------------------------
        public static string SchoolHalfYearName(YearHalfYear yearHalfYear)
        {
            return Format(Inst.GetString("SchoolHalfYearName"), (int)yearHalfYear);
        } // SchoolHalfYearName

        // ----------------------------------------------------------------------
        public static string SystemHalfYearOfYearName(YearHalfYear yearHalfYear, int year)
        {
            return Format(Inst.GetString("SystemHalfYearOfYearName"), (int)yearHalfYear, year);
        } // SystemHalfYearOfYearName

        // ----------------------------------------------------------------------
        public static string CalendarHalfYearOfYearName(YearHalfYear yearHalfYear, int year)
        {
            return Format(Inst.GetString("CalendarHalfYearOfYearName"), (int)yearHalfYear, year);
        } // CalendarHalfYearOfYearName

        // ----------------------------------------------------------------------
        public static string FiscalHalfYearOfYearName(YearHalfYear yearHalfYear, int year)
        {
            return Format(Inst.GetString("FiscalHalfYearOfYearName"), (int)yearHalfYear, year);
        } // FiscalHalfYearOfYearName

        // ----------------------------------------------------------------------
        public static string SchoolHalfYearOfYearName(YearHalfYear yearHalfYear, int year)
        {
            return Format(Inst.GetString("SchoolHalfYearOfYearName"), (int)yearHalfYear, year);
        } // SchoolHalfYearOfYearName

        #endregion HalfYear

        #region Quarter

        // ----------------------------------------------------------------------
        public static string SystemQuarterName(YearQuarter yearQuarter)
        {
            return Format(Inst.GetString("SystemQuarterName"), (int)yearQuarter);
        } // SystemQuarterName

        // ----------------------------------------------------------------------
        public static string CalendarQuarterName(YearQuarter yearQuarter)
        {
            return Format(Inst.GetString("CalendarQuarterName"), (int)yearQuarter);
        } // CalendarQuarterName

        // ----------------------------------------------------------------------
        public static string FiscalQuarterName(YearQuarter yearQuarter)
        {
            return Format(Inst.GetString("FiscalQuarterName"), (int)yearQuarter);
        } // FiscalQuarterName

        // ----------------------------------------------------------------------
        public static string SchoolQuarterName(YearQuarter yearQuarter)
        {
            return Format(Inst.GetString("SchoolQuarterName"), (int)yearQuarter);
        } // SchoolQuarterName

        // ----------------------------------------------------------------------
        public static string SystemQuarterOfYearName(YearQuarter yearQuarter, int year)
        {
            return Format(Inst.GetString("SystemQuarterOfYearName"), (int)yearQuarter, year);
        } // SystemQuarterOfYearName

        // ----------------------------------------------------------------------
        public static string CalendarQuarterOfYearName(YearQuarter yearQuarter, int year)
        {
            return Format(Inst.GetString("CalendarQuarterOfYearName"), (int)yearQuarter, year);
        } // CalendarQuarterOfYearName

        // ----------------------------------------------------------------------
        public static string FiscalQuarterOfYearName(YearQuarter yearQuarter, int year)
        {
            return Format(Inst.GetString("FiscalQuarterOfYearName"), (int)yearQuarter, year);
        } // FiscalQuarterOfYearName

        // ----------------------------------------------------------------------
        public static string SchoolQuarterOfYearName(YearQuarter yearQuarter, int year)
        {
            return Format(Inst.GetString("SchoolQuarterOfYearName"), (int)yearQuarter, year);
        } // SchoolQuarterOfYearName

        #endregion Quarter

        #region Month

        // ----------------------------------------------------------------------
        public static string MonthOfYearName(string monthName, string yearName)
        {
            return Format(Inst.GetString("MonthOfYearName"), monthName, yearName);
        } // MonthOfYearName

        #endregion Month

        #region Wek

        // ----------------------------------------------------------------------
        public static string WeekOfYearName(int weekOfYear, string yearName)
        {
            return Format(Inst.GetString("WeekOfYearName"), weekOfYear, yearName);
        } // WeekOfYearName

        #endregion Wek

        #region Time Formatter

        // ----------------------------------------------------------------------
        public static string TimeSpanYears => Inst.GetString("TimeSpanYears"); // TimeSpanYears

        // ----------------------------------------------------------------------
        public static string TimeSpanYear => Inst.GetString("TimeSpanYear"); // TimeSpanYear

        // ----------------------------------------------------------------------
        public static string TimeSpanMonths => Inst.GetString("TimeSpanMonths"); // TimeSpanMonths

        // ----------------------------------------------------------------------
        public static string TimeSpanMonth => Inst.GetString("TimeSpanMonth"); // TimeSpanMonth

        // ----------------------------------------------------------------------
        public static string TimeSpanWeeks => Inst.GetString("TimeSpanWeeks"); // TimeSpanWeeks

        // ----------------------------------------------------------------------
        public static string TimeSpanWeek => Inst.GetString("TimeSpanWeek"); // TimeSpanWeek

        // ----------------------------------------------------------------------
        public static string TimeSpanDays => Inst.GetString("TimeSpanDays"); // TimeSpanDays

        // ----------------------------------------------------------------------
        public static string TimeSpanDay => Inst.GetString("TimeSpanDay"); // TimeSpanDay

        // ----------------------------------------------------------------------
        public static string TimeSpanHours => Inst.GetString("TimeSpanHours"); // TimeSpanHours

        // ----------------------------------------------------------------------
        public static string TimeSpanHour => Inst.GetString("TimeSpanHour"); // TimeSpanHour

        // ----------------------------------------------------------------------
        public static string TimeSpanMinutes => Inst.GetString("TimeSpanMinutes"); // TimeSpanMinutes

        // ----------------------------------------------------------------------
        public static string TimeSpanMinute => Inst.GetString("TimeSpanMinute"); // TimeSpanMinute

        // ----------------------------------------------------------------------
        public static string TimeSpanSeconds => Inst.GetString("TimeSpanSeconds"); // TimeSpanSeconds

        // ----------------------------------------------------------------------
        public static string TimeSpanSecond => Inst.GetString("TimeSpanSecond"); // TimeSpanSecond

        #endregion Time Formatter

        // ----------------------------------------------------------------------
        private static string Format(string format, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, format, args);
        } // Format

        // ----------------------------------------------------------------------
        private static ResourceManager NewInst(Type singletonType)
        {
            if (singletonType == null)
            {
                throw new ArgumentNullException(nameof(singletonType));
            }
            if (singletonType.FullName == null)
            {
                throw new InvalidOperationException();
            }

            return new ResourceManager(singletonType.FullName, singletonType.Assembly);
        } // NewInst

        // ----------------------------------------------------------------------
        // members
        private static readonly ResourceManager Inst = NewInst(typeof(Strings));
    } // class Strings
}