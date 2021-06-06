using System;
using TimeAndDates.Interfaces;

namespace TimeAndDates
{
    public class DateRange
    {
        public IWeek Week { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public bool InRange => Start.Date >= DateTime.Today.Date && End.Date <= DateTime.Today.Date;
        public bool Valid => Start < End;

        public DateRange()
        {
            Start = DateTime.Today.Date;
            End = DateTime.Today.AddMonths(4).Date;
        }

        public DateRange(DateTime end)
        {
            Start = DateTime.Today.Date;
            End = end;
        }

        public DateRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public void SetWorkWeek( IWeek week)
        {
            Week = week;
        }

        public int DaysRemaining()
        {
            var daysRemaining = 0;

            if (Valid)
            {
                daysRemaining = (int)((End - DateTime.Today).TotalDays) - 1;
            }

            return daysRemaining > 0 ? daysRemaining : 0;
        }

        public int DaysUntilStart()
        {
            var daysRemaining = 0;

            if (Valid)
            {
                daysRemaining = (int)((Start - DateTime.Today).TotalDays);
            }

            return daysRemaining > 0 ? daysRemaining : 0;
        }
    }
}