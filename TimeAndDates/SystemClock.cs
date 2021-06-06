using System;
using TimeAndDates.Interfaces;

namespace TimeAndDates
{
    // ------------------------------------------------------------------------
    public class SystemClock : IClock
    {

        // ----------------------------------------------------------------------
        internal SystemClock()
        {
        } // SystemClock

        // ----------------------------------------------------------------------
        public DateTime Now => DateTime.Now; // Now

    } // class SystemClock
}