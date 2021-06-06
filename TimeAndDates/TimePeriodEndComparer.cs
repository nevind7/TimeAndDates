﻿using TimeAndDates.Interfaces;

namespace TimeAndDates
{
    public sealed class TimePeriodEndComparer : ITimePeriodComparer
    {
        // ----------------------------------------------------------------------
        public static ITimePeriodComparer Comparer = new TimePeriodEndComparer();

        public static ITimePeriodComparer ReverseComparer = new TimePeriodReversComparer(new TimePeriodEndComparer());

        // ----------------------------------------------------------------------
        public int Compare(ITimePeriod left, ITimePeriod right)
        {
            return left.End.CompareTo(right.End);
        } // Compare
    } // class TimePeriodEndComparer
}