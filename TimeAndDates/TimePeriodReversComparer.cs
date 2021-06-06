using TimeAndDates.Interfaces;

namespace TimeAndDates
{
    // ------------------------------------------------------------------------
    public sealed class TimePeriodReversComparer : ITimePeriodComparer
    {
        // ----------------------------------------------------------------------
        public TimePeriodReversComparer(ITimePeriodComparer baseComparer)
        {
            BaseComparer = baseComparer;
        } // TimePeriodReversComparer

        // ----------------------------------------------------------------------
        public ITimePeriodComparer BaseComparer { get; }

        // ----------------------------------------------------------------------
        public int Compare(ITimePeriod left, ITimePeriod right)
        {
            return -BaseComparer.Compare(left, right);
        } // Compare

        // ----------------------------------------------------------------------
        // members
    } // class TimePeriodReversComparer
}