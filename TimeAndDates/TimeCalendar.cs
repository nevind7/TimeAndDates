using System;
using System.Globalization;

using TimeAndDates.Enums;
using TimeAndDates.Interfaces;
using TimeAndDates.Utilities;

namespace TimeAndDates
{
    // ------------------------------------------------------------------------
    public class TimeCalendar : ITimeCalendar
    {
        // ----------------------------------------------------------------------
        public static readonly TimeSpan DefaultStartOffset = TimeSpec.NoDuration;

        public static readonly TimeSpan DefaultEndOffset = TimeSpec.MinNegativeDuration;

        // ----------------------------------------------------------------------
        public TimeCalendar() :
            this(new TimeCalendarConfig())
        {
        } // TimeCalendar

        // ----------------------------------------------------------------------
        public TimeCalendar(TimeCalendarConfig config)
        {
            if (config.StartOffset < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException("config");
            }
            if (config.EndOffset > TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException("config");
            }

            _culture = config.Culture ?? CultureInfo.CurrentCulture;
            _startOffset = config.StartOffset ?? DefaultStartOffset;
            _endOffset = config.EndOffset ?? DefaultEndOffset;
            _yearBaseMonth = config.YearBaseMonth ?? TimeSpec.CalendarYearStartMonth;
            _fiscalYearBaseMonth = config.FiscalYearBaseMonth ?? TimeSpec.FiscalYearBaseMonth;
            _fiscalFirstDayOfYear = config.FiscalFirstDayOfYear ?? DayOfWeek.Sunday;
            _yearWeekType = config.YearWeekType ?? YearWeekType.Calendar;
            _dayNameType = config.DayNameType ?? CalendarNameType.Full;
            _monthNameType = config.MonthNameType ?? CalendarNameType.Full;
        } // TimeCalendar

        // ----------------------------------------------------------------------
        public CultureInfo Culture => _culture; // Culture

        // ----------------------------------------------------------------------
        public TimeSpan StartOffset => _startOffset; // StartOffset

        // ----------------------------------------------------------------------
        public TimeSpan EndOffset => _endOffset; // EndOffset

        // ----------------------------------------------------------------------
        public YearMonth YearBaseMonth => _yearBaseMonth; // YearBaseMonth

        // ----------------------------------------------------------------------
        public YearMonth FiscalYearBaseMonth => _fiscalYearBaseMonth; // FiscalYearBaseMonth

        // ----------------------------------------------------------------------
        public DayOfWeek FiscalFirstDayOfYear => _fiscalFirstDayOfYear; // FiscalFirstDayOfYear

        // ----------------------------------------------------------------------
        public virtual DayOfWeek FirstDayOfWeek => _culture.DateTimeFormat.FirstDayOfWeek; // FirstDayOfWeek


        // ----------------------------------------------------------------------
        public static TimeCalendar New(CultureInfo culture)
        {
            return new(new TimeCalendarConfig
            {
                Culture = culture
            });
        } // New

        // ----------------------------------------------------------------------
        public static TimeCalendar New(YearMonth yearBaseMonth)
        {
            return new(new TimeCalendarConfig
            {
                YearBaseMonth = yearBaseMonth
            });
        } // New

        // ----------------------------------------------------------------------
        public static TimeCalendar New(TimeSpan startOffset, TimeSpan endOffset)
        {
            return new(new TimeCalendarConfig
            {
                StartOffset = startOffset,
                EndOffset = endOffset
            });
        } // New

        // ----------------------------------------------------------------------
        public static TimeCalendar New(TimeSpan startOffset, TimeSpan endOffset, YearMonth yearBaseMonth)
        {
            return new(new TimeCalendarConfig
            {
                StartOffset = startOffset,
                EndOffset = endOffset,
                YearBaseMonth = yearBaseMonth,
            });
        } // New

        // ----------------------------------------------------------------------
        public static TimeCalendar New(CultureInfo culture, TimeSpan startOffset, TimeSpan endOffset)
        {
            return new(new TimeCalendarConfig
            {
                Culture = culture,
                StartOffset = startOffset,
                EndOffset = endOffset
            });
        } // New

        // ----------------------------------------------------------------------
        public static TimeCalendar New(CultureInfo culture, YearMonth yearBaseMonth, YearWeekType yearWeekType)
        {
            return new(new TimeCalendarConfig
            {
                Culture = culture,
                YearBaseMonth = yearBaseMonth,
                YearWeekType = yearWeekType
            });
        } // New

        // ----------------------------------------------------------------------
        public static TimeCalendar NewEmptyOffset()
        {
            return new(new TimeCalendarConfig
            {
                StartOffset = TimeSpan.Zero,
                EndOffset = TimeSpan.Zero
            });
        } // NewEmptyOffset

        // ----------------------------------------------------------------------
        public virtual DateTime MapStart(DateTime moment)
        {
            return moment.Add(_startOffset);
        } // MapStart

        // ----------------------------------------------------------------------
        public virtual DateTime MapEnd(DateTime moment)
        {
            return moment.Add(_endOffset);
        } // MapEnd

        // ----------------------------------------------------------------------
        public virtual DateTime UnmapStart(DateTime moment)
        {
            return moment.Subtract(_startOffset);
        } // UnmapStart

        // ----------------------------------------------------------------------
        public virtual DateTime UnmapEnd(DateTime moment)
        {
            return moment.Subtract(_endOffset);
        } // UnmapEnd

        // ----------------------------------------------------------------------
        public virtual int GetYear(DateTime time)
        {
            return _culture.Calendar.GetYear(time);
        } // GetYear

        // ----------------------------------------------------------------------
        public virtual int GetMonth(DateTime time)
        {
            return _culture.Calendar.GetMonth(time);
        } // GetMonth

        // ----------------------------------------------------------------------
        public virtual int GetHour(DateTime time)
        {
            return _culture.Calendar.GetHour(time);
        } // GetHour

        // ----------------------------------------------------------------------
        public virtual int GetMinute(DateTime time)
        {
            return _culture.Calendar.GetMinute(time);
        } // GetMinute

        // ----------------------------------------------------------------------
        public virtual int GetDayOfMonth(DateTime time)
        {
            return _culture.Calendar.GetDayOfMonth(time);
        } // GetDayOfMonth

        // ----------------------------------------------------------------------
        public virtual DayOfWeek GetDayOfWeek(DateTime time)
        {
            return _culture.Calendar.GetDayOfWeek(time);
        } // GetDayOfWeek

        // ----------------------------------------------------------------------
        public virtual int GetDaysInMonth(int year, int month)
        {
            return _culture.Calendar.GetDaysInMonth(year, month);
        } // GetDaysInMonth

        // ----------------------------------------------------------------------
        public virtual string GetMonthName(int month)
        {
            switch (_monthNameType)
            {
                case CalendarNameType.Abbreviated:
                    return _culture.DateTimeFormat.GetAbbreviatedMonthName(month);

                default:
                    return _culture.DateTimeFormat.GetMonthName(month);
            }
        } // GetMonthName

        // ----------------------------------------------------------------------
        public virtual string GetDayName(DayOfWeek dayOfWeek)
        {
            switch (_dayNameType)
            {
                case CalendarNameType.Abbreviated:
                    return _culture.DateTimeFormat.GetAbbreviatedDayName(dayOfWeek);

                default:
                    return _culture.DateTimeFormat.GetDayName(dayOfWeek);
            }
        } // GetDayName

        // ----------------------------------------------------------------------
        public virtual int GetWeekOfYear(DateTime time)
        {
            TimeTool.GetWeekOfYear(time, _culture, _yearWeekType, out _, out var weekOfYear);
            return weekOfYear;
        } // GetWeekOfYear

        // ----------------------------------------------------------------------
        public virtual DateTime GetStartOfYearWeek(int year, int weekOfYear)
        {
            return TimeTool.GetStartOfYearWeek(year, weekOfYear, _culture, _yearWeekType);
        } // GetStartOfYearWeek

        // ----------------------------------------------------------------------
        public sealed override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return IsEqual(obj);
        } // Equals

        // ----------------------------------------------------------------------
        protected virtual bool IsEqual(object obj)
        {
            return HasSameData(obj as TimeCalendar);
        } // IsEqual

        // ----------------------------------------------------------------------
        private bool HasSameData(TimeCalendar comp)
        {
            return _culture.Equals(comp._culture) &&
                _startOffset == comp._startOffset &&
                _endOffset == comp._endOffset &&
                _yearBaseMonth == comp._yearBaseMonth &&
                _fiscalYearBaseMonth == comp._fiscalYearBaseMonth &&
                _yearWeekType == comp._yearWeekType &&
                _dayNameType == comp._dayNameType &&
                _monthNameType == comp._monthNameType;
        } // HasSameData

        // ----------------------------------------------------------------------
        public sealed override int GetHashCode()
        {
            return HashTool.AddHashCode(GetType().GetHashCode(), ComputeHashCode());
        } // GetHashCode

        // ----------------------------------------------------------------------
        protected virtual int ComputeHashCode()
        {
            return HashTool.ComputeHashCode(
                _culture,
                _startOffset,
                _endOffset,
                _yearBaseMonth,
                _fiscalYearBaseMonth,
                _fiscalFirstDayOfYear,
                _yearWeekType,
                _dayNameType,
                _monthNameType);
        } // ComputeHashCode

        // ----------------------------------------------------------------------
        // members
        private readonly CultureInfo _culture;

        private readonly TimeSpan _startOffset;
        private readonly TimeSpan _endOffset;
        private readonly YearMonth _yearBaseMonth;
        private readonly YearMonth _fiscalYearBaseMonth;
        private readonly DayOfWeek _fiscalFirstDayOfYear;
        private readonly YearWeekType _yearWeekType;
        private readonly CalendarNameType _dayNameType;
        private readonly CalendarNameType _monthNameType;
    } // class TimeCalendar
}