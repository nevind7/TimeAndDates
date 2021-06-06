using TimeAndDates.Interfaces;

namespace TimeAndDates
{
    // ------------------------------------------------------------------------
    public static class ClockProxy
    {
        // ----------------------------------------------------------------------
        public static IClock Clock
        {
            get => _clock ?? new SystemClock();
            set => _clock = value;
        } // Clock

        // ----------------------------------------------------------------------
        private static IClock _clock;
    } // class ClockProxy
}