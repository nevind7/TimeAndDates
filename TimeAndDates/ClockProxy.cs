using TimeAndDates.Interfaces;

namespace TimeAndDates
{
    // ------------------------------------------------------------------------
    public static class ClockProxy
    {

        // ----------------------------------------------------------------------
        public static IClock Clock
        {
            get
            {
                if (_clock != null) return _clock;

                lock ( Mutex )
                {
                    _clock ??= new SystemClock();
                }
                return _clock;
            }
            set
            {
                lock ( Mutex )
                {
                    _clock = value;
                }
            }
        } // Clock

        // ----------------------------------------------------------------------
        // members
        private static readonly object Mutex = new();
        private static volatile IClock _clock;

    } // class ClockProxy
}