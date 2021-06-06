using System;
using System.Collections.Generic;

namespace TimeAndDates.Interfaces
{
    public interface IWeek
    {
        void SetWeek();

        void SetWeek(IEnumerable<IWeekDay> days);

        void AddDay(IWeekDay day);

        void RemoveDay(DayOfWeek day);

        IWeekDay GetHoursForDay(DayOfWeek day);

        IWeekDay Monday();

        IWeekDay Tuesday();

        IWeekDay Wednesday();

        IWeekDay Thursday();

        IWeekDay Friday();

        IWeekDay Saturday();

        IWeekDay Sunday();
    }
}