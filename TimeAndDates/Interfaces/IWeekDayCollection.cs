using System;

namespace TimeAndDates.Interfaces
{
    public interface IWeekDayCollection : IWeekDayContainer
    {
        // ----------------------------------------------------------------------
        bool HasInsidePeriods(IWeekDay test);

        // ----------------------------------------------------------------------
        bool HasOverlaps();

        // ----------------------------------------------------------------------
        bool Remove(DayOfWeek item);

        // ----------------------------------------------------------------------
        bool HasOverlapPeriods(IWeekDay test);

        // ----------------------------------------------------------------------
        bool HasIntersectionPeriods(DateTime test);

        // ----------------------------------------------------------------------
        bool HasIntersectionPeriods(IWeekDay test);

        // ----------------------------------------------------------------------
        IWeekDayCollection InsidePeriods(IWeekDay test);

        // ----------------------------------------------------------------------
        IWeekDayCollection OverlapPeriods(IWeekDay test);

        // ----------------------------------------------------------------------
        IWeekDayCollection IntersectionPeriods(DateTime test);

        // ----------------------------------------------------------------------
        IWeekDayCollection IntersectionPeriods(IWeekDay test);
    }
}