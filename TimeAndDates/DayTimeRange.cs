using System;

using TimeAndDates.Interfaces;
using TimeAndDates.Utilities;

namespace TimeAndDates
{
    // ------------------------------------------------------------------------
    public abstract class DayTimeRange : CalendarTimeRange
    {
        // ----------------------------------------------------------------------
        protected DayTimeRange()
        {
        } // DayTimeRange



        // ----------------------------------------------------------------------
        protected DayTimeRange(int year, int month, int day, int dayCount) :
            this(year, month, day, dayCount, new TimeCalendar())
        {
        } // DayTimeRange

        // ----------------------------------------------------------------------
        protected DayTimeRange(int year, int month, int day, int dayCount, ITimeCalendar calendar) :
            base(GetPeriodOf(year, month, day, dayCount), calendar)
        {
            this._startDay = new DateTime(year, month, day);
            this._dayCount = dayCount;
            _endDay = calendar.MapEnd(this._startDay.AddDays(dayCount));
        } // DayTimeRange

        // ----------------------------------------------------------------------
        public int StartYear => _startDay.Year; // StartYear

        // ----------------------------------------------------------------------
        public int StartMonth => _startDay.Month; // StartMonth

        // ----------------------------------------------------------------------
        public int StartDay => _startDay.Day; // StartDay

        // ----------------------------------------------------------------------
        public int EndYear => _endDay.Year; // EndYear

        // ----------------------------------------------------------------------
        public int EndMonth => _endDay.Month; // EndMonth

        // ----------------------------------------------------------------------
        public int EndDay => _endDay.Day; // EndDay

        // ----------------------------------------------------------------------
        public int DayCount => _dayCount; // DayCount

        // ----------------------------------------------------------------------
        public DayOfWeek StartDayOfWeek
        {
            get => Calendar.GetDayOfWeek(_startDay);
            set => _startDayOfWeek = value;
        }

        // ----------------------------------------------------------------------
        public string StartDayName => Calendar.GetDayName(StartDayOfWeek); // StartDayName

        // ----------------------------------------------------------------------
        public DayOfWeek EndDayOfWeek => Calendar.GetDayOfWeek(_endDay); // EndDayOfWeek

        // ----------------------------------------------------------------------
        public string EndDayName => Calendar.GetDayName(EndDayOfWeek); // EndDayName

        // ----------------------------------------------------------------------
        public ITimePeriodCollection GetHours()
        {
            TimePeriodCollection hours = new TimePeriodCollection();
            DateTime date = _startDay;
            for (int day = 0; day < _dayCount; day++)
            {
                DateTime curDay = date.AddDays(day);
                for (int hour = 0; hour < TimeSpec.HoursPerDay; hour++)
                {
                    hours.Add(new Hour(curDay.AddHours(hour), Calendar));
                }
            }
            return hours;
        } // GetHours

        // ----------------------------------------------------------------------
        protected override bool IsEqual(object obj)
        {
            return base.IsEqual(obj) && HasSameData(obj as DayTimeRange);
        } // IsEqual

        // ----------------------------------------------------------------------
        private bool HasSameData(DayTimeRange comp)
        {
            return
                _startDay == comp._startDay &&
                _dayCount == comp._dayCount &&
                _endDay == comp._endDay;
        } // HasSameData

        // ----------------------------------------------------------------------
        protected override int ComputeHashCode()
        {
            return HashTool.ComputeHashCode(base.ComputeHashCode(), _startDay, _dayCount, _endDay);
        } // ComputeHashCode

        // ----------------------------------------------------------------------
        private static TimeRange GetPeriodOf(int year, int month, int day, int dayCount)
        {
            if (dayCount < 1)
            {
                throw new ArgumentOutOfRangeException("dayCount");
            }

            DateTime start = new DateTime(year, month, day);
            DateTime end = start.AddDays(dayCount);
            return new TimeRange(start, end);
        } // GetPeriodOf

        // ----------------------------------------------------------------------
        // members
        private readonly DateTime _startDay;
        private DayOfWeek _startDayOfWeek;
        private readonly int _dayCount;
        private readonly DateTime _endDay; // cache
    } // class DayTimeRange
}