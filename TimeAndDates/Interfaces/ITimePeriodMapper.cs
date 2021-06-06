using System;

namespace TimeAndDates.Interfaces
{
    // ------------------------------------------------------------------------
    public interface ITimePeriodMapper
    {

        // ----------------------------------------------------------------------
        DateTime MapStart( DateTime moment );

        // ----------------------------------------------------------------------
        DateTime MapEnd( DateTime moment );

        // ----------------------------------------------------------------------
        DateTime UnmapStart( DateTime moment );

        // ----------------------------------------------------------------------
        DateTime UnmapEnd( DateTime moment );

    } // interface ITimePeriodMapper
}