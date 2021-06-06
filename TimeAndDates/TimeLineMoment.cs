using System;

using TimeAndDates.Interfaces;

namespace TimeAndDates
{
    // ------------------------------------------------------------------------
    public class TimeLineMoment : ITimeLineMoment
    {
        // ----------------------------------------------------------------------
        public TimeLineMoment(DateTime moment)
        {
            Moment = moment;
        } // TimeLineMoment

        // ----------------------------------------------------------------------
        public DateTime Moment { get; }

        // ----------------------------------------------------------------------
        public int BalanceCount => StartCount - EndCount; // BalanceCount

        // ----------------------------------------------------------------------
        public int StartCount { get; private set; }

        // ----------------------------------------------------------------------
        public int EndCount { get; private set; }

        // ----------------------------------------------------------------------
        public bool IsEmpty => StartCount == 0 && EndCount == 0; // IsEmpty

        // ----------------------------------------------------------------------
        public void AddStart()
        {
            StartCount++;
        } // AddStart

        // ----------------------------------------------------------------------
        public void RemoveStart()
        {
            if (StartCount == 0)
            {
                throw new InvalidOperationException();
            }
            StartCount--;
        } // RemoveStart

        // ----------------------------------------------------------------------
        public void AddEnd()
        {
            EndCount++;
        } // AddEnd

        // ----------------------------------------------------------------------
        public void RemoveEnd()
        {
            if (EndCount == 0)
            {
                throw new InvalidOperationException();
            }
            EndCount--;
        } // RemoveEnd

        // ----------------------------------------------------------------------
        public override string ToString()
        {
            return $"{Moment} [{StartCount}/{EndCount}]";
        } // ToString

        // ----------------------------------------------------------------------
        // members
    } // class TimeLineMoment
}