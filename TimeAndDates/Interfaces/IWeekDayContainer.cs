using System.Collections.Generic;

namespace TimeAndDates.Interfaces
{
    public interface IWeekDayContainer : IList<IWeekDay>
    {
        // ----------------------------------------------------------------------
        bool ContainsDay(IWeekDay test);

        // ----------------------------------------------------------------------
        void AddAll(IEnumerable<IWeekDay> days);

    }
}