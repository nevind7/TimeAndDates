using System;

namespace TimeAndDates.Interfaces
{
    public interface IDurationProvider
    {
        // ----------------------------------------------------------------------
        TimeSpan GetDuration(DateTime start, DateTime end);
    } // interface IDurationProvider
}