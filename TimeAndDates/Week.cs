using System;
using System.Collections.Generic;

using TimeAndDates.Interfaces;

namespace TimeAndDates
{
    public class Week : IWeek
    {
        private WeekDayCollection _weekDays { get; }
        public WeekDayCollection WeekDays => _weekDays;

        // ----------------------------------------------------------------------
        public Week()
        {
            _weekDays = new WeekDayCollection();
        } // Week

        public IWeekDay GetHoursForDay(DayOfWeek day)
        {
            return _weekDays.GetHours(day);
        }

        public void SetWeek()
        {
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                _weekDays.Add(new WeekDay(day));
            }
        }

        public void SetWeek(IEnumerable<IWeekDay> days)
        {
            _weekDays.AddAll(days);
        }

        public void AddDay(IWeekDay day)
        {
            _weekDays.Add(day);
        }

        public void RemoveDay(DayOfWeek day)
        {
            _weekDays.Remove(day);
        }

        public IWeekDay Monday()
        {
            return _weekDays.GetHours(DayOfWeek.Monday);
        }

        public IWeekDay Tuesday()
        {
            return _weekDays.GetHours(DayOfWeek.Tuesday);
        }

        public IWeekDay Wednesday()
        {
            return _weekDays.GetHours(DayOfWeek.Wednesday);
        }

        public IWeekDay Thursday()
        {
            return _weekDays.GetHours(DayOfWeek.Thursday);
        }

        public IWeekDay Friday()
        {
            return _weekDays.GetHours(DayOfWeek.Friday);
        }

        public IWeekDay Saturday()
        {
            return _weekDays.GetHours(DayOfWeek.Saturday);
        }

        public IWeekDay Sunday()
        {
            return _weekDays.GetHours(DayOfWeek.Sunday);
        }
    }
}