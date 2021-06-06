﻿using TimeAndDates.Interfaces;

namespace TimeAndDates
{
    // ------------------------------------------------------------------------
    public sealed class TimePeriodDurationComparer : ITimePeriodComparer
    {

        // ----------------------------------------------------------------------
        public static ITimePeriodComparer Comparer = new TimePeriodDurationComparer();
        public static ITimePeriodComparer ReverseComparer = new TimePeriodReversComparer( new TimePeriodDurationComparer() );

        // ----------------------------------------------------------------------
        public int Compare( ITimePeriod left, ITimePeriod right )
        {
            return left.Duration.CompareTo( right.Duration );
        } // Compare

    } // class TimePeriodDurationComparer

}