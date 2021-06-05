using System;

using TimeAndDates.enums;
using TimeAndDates.Interfaces;
using TimeAndDates.Utilities;

namespace TimeAndDates
{
    public class TimeBlock : ITimeBlock
    {
        // ----------------------------------------------------------------------
        public static readonly TimeBlock Anytime = new(true);

        // ----------------------------------------------------------------------
        public TimeBlock() :
            this(TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate)
        {
        } // TimeBlock

        // ----------------------------------------------------------------------
        internal TimeBlock(bool isReadOnly = false) :
            this(TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate, isReadOnly)
        {
        } // TimeBlock

        // ----------------------------------------------------------------------
        public TimeBlock(DateTime moment, bool isReadOnly = false) :
            this(moment, TimeSpec.MinPeriodDuration, isReadOnly)
        {
        } // TimeBlock

        // ----------------------------------------------------------------------
        public TimeBlock(DateTime start, DateTime end, bool isReadOnly = false)
        {
            if (start <= end)
            {
                _start = start;
                _end = end;
            }
            else
            {
                _end = start;
                _start = end;
            }
            _duration = _end - _start;
            IsReadOnly = isReadOnly;
        } // TimeBlock

        // ----------------------------------------------------------------------
        public TimeBlock(DateTime start, TimeSpan duration, bool isReadOnly = false)
        {
            if (duration < TimeSpec.MinPeriodDuration)
            {
                throw new ArgumentOutOfRangeException(nameof(duration));
            }
            _start = start;
            _duration = duration;
            _end = start.Add(duration);
            IsReadOnly = isReadOnly;
        } // TimeBlock

        // ----------------------------------------------------------------------
        public TimeBlock(TimeSpan duration, DateTime end, bool isReadOnly = false)
        {
            if (duration < TimeSpec.MinPeriodDuration)
            {
                throw new ArgumentOutOfRangeException(nameof(duration));
            }
            _end = end;
            _duration = duration;
            _start = end.Subtract(duration);
            IsReadOnly = isReadOnly;
        } // TimeBlock

        // ----------------------------------------------------------------------
        public TimeBlock(ITimePeriod copy)
        {
            if (copy == null)
            {
                throw new ArgumentNullException(nameof(copy));
            }
            _start = copy.Start;
            _end = copy.End;
            _duration = copy.Duration;
            IsReadOnly = copy.IsReadOnly;
        } // TimeBlock

        // ----------------------------------------------------------------------
        protected TimeBlock(ITimePeriod copy, bool isReadOnly)
        {
            if (copy == null)
            {
                throw new ArgumentNullException(nameof(copy));
            }
            _start = copy.Start;
            _end = copy.End;
            _duration = copy.Duration;
            IsReadOnly = isReadOnly;
        } // TimeBlock

        // ----------------------------------------------------------------------
        public bool IsReadOnly { get; }

        // ----------------------------------------------------------------------
        public bool IsAnytime => !HasStart && !HasEnd; // IsAnytime

        // ----------------------------------------------------------------------
        public bool IsMoment => _start.Equals(_end); // IsMoment

        // ----------------------------------------------------------------------
        public bool HasStart => _start != TimeSpec.MinPeriodDate; // HasStart

        // ----------------------------------------------------------------------
        public DateTime Start
        {
            get => _start;
            set
            {
                CheckModification();
                _start = value;
                _end = _start.Add(_duration);
            }
        } // Start

        // ----------------------------------------------------------------------
        public bool HasEnd => _end != TimeSpec.MaxPeriodDate; // HasEnd

        // ----------------------------------------------------------------------
        public DateTime End
        {
            get => _end;
            set
            {
                CheckModification();
                _end = value;
                _start = _end.Subtract(_duration);
            }
        } // End

        // ----------------------------------------------------------------------
        public TimeSpan Duration
        {
            get => _duration;
            set => DurationFromStart(value);
        } // Duration

        // ----------------------------------------------------------------------
        public string DurationDescription => TimeFormatter.Instance.GetDuration(Duration, DurationFormatType.Detailed); // DurationDescription

        // ----------------------------------------------------------------------
        public virtual TimeSpan GetDuration(IDurationProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            return provider.GetDuration(Start, End);
        } // GetDuration

        // ----------------------------------------------------------------------
        public virtual void Setup(DateTime newStart, DateTime newEnd)
        {
            CheckModification();
            if (newStart <= newEnd)
            {
                _start = newStart;
                _end = newEnd;
            }
            else
            {
                _end = newStart;
                _start = newEnd;
            }
            _duration = _end - _start;
        } // Setup

        // ----------------------------------------------------------------------
        public virtual void Setup(DateTime newStart, TimeSpan newDuration)
        {
            CheckModification();
            if (newDuration < TimeSpec.MinPeriodDuration)
            {
                throw new ArgumentOutOfRangeException(nameof(newDuration));
            }
            _start = newStart;
            _duration = newDuration;
            _end = _start.Add(_duration);
        } // Setup

        // ----------------------------------------------------------------------
        public ITimeBlock Copy()
        {
            return Copy(TimeSpan.Zero);
        } // Copy

        // ----------------------------------------------------------------------
        public virtual ITimeBlock Copy(TimeSpan offset)
        {
            return new TimeBlock(_start.Add(offset), _end.Add(offset), IsReadOnly);
        } // Copy

        // ----------------------------------------------------------------------
        public virtual void Move(TimeSpan offset)
        {
            CheckModification();
            if (offset == TimeSpan.Zero)
            {
                return;
            }
            _start = _start.Add(offset);
            _end = _end.Add(offset);
        } // Move

        // ----------------------------------------------------------------------
        public ITimeBlock GetPreviousPeriod()
        {
            return GetPreviousPeriod(TimeSpan.Zero);
        } // GetPreviousPeriod

        // ----------------------------------------------------------------------
        public virtual ITimeBlock GetPreviousPeriod(TimeSpan offset)
        {
            return new TimeBlock(Duration, Start.Add(offset), IsReadOnly);
        } // GetPreviousPeriod

        // ----------------------------------------------------------------------
        public ITimeBlock GetNextPeriod()
        {
            return GetNextPeriod(TimeSpan.Zero);
        } // GetNextPeriod

        // ----------------------------------------------------------------------
        public virtual ITimeBlock GetNextPeriod(TimeSpan offset)
        {
            return new TimeBlock(End.Add(offset), Duration, IsReadOnly);
        } // GetNextPeriod

        // ----------------------------------------------------------------------
        public virtual void DurationFromStart(TimeSpan newDuration)
        {
            if (newDuration < TimeSpec.MinPeriodDuration)
            {
                throw new ArgumentOutOfRangeException(nameof(newDuration));
            }
            CheckModification();

            _duration = newDuration;
            _end = _start.Add(newDuration);
        } // DurationFromStart

        // ----------------------------------------------------------------------
        public virtual void DurationFromEnd(TimeSpan newDuration)
        {
            if (newDuration < TimeSpec.MinPeriodDuration)
            {
                throw new ArgumentOutOfRangeException(nameof(newDuration));
            }
            CheckModification();

            _duration = newDuration;
            _start = _end.Subtract(newDuration);
        } // DurationFromEnd

        // ----------------------------------------------------------------------
        public virtual bool IsSamePeriod(ITimePeriod test)
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
            }
            return _start == test.Start && _end == test.End;
        } // IsSamePeriod

        // ----------------------------------------------------------------------
        public virtual bool HasInside(DateTime test)
        {
            return TimePeriodCalc.HasInside(this, test);
        } // HasInside

        // ----------------------------------------------------------------------
        public virtual bool HasInside(ITimePeriod test)
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
            }
            return TimePeriodCalc.HasInside(this, test);
        } // HasInside

        // ----------------------------------------------------------------------
        public virtual bool IntersectsWith(ITimePeriod test)
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
            }
            return TimePeriodCalc.IntersectsWith(this, test);
        } // IntersectsWith

        // ----------------------------------------------------------------------
        public virtual ITimeBlock GetIntersection(ITimePeriod period)
        {
            if (period == null)
            {
                throw new ArgumentNullException(nameof(period));
            }
            if (!IntersectsWith(period))
            {
                return null;
            }
            var periodStart = period.Start;
            var periodEnd = period.End;
            return new TimeBlock(
                periodStart > _start ? periodStart : _start,
                periodEnd < _end ? periodEnd : _end,
                IsReadOnly);
        } // GetIntersection

        // ----------------------------------------------------------------------
        public virtual bool OverlapsWith(ITimePeriod test)
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
            }
            return TimePeriodCalc.OverlapsWith(this, test);
        } // OverlapsWith

        // ----------------------------------------------------------------------
        public virtual PeriodRelation GetRelation(ITimePeriod test)
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
            }
            return TimePeriodCalc.GetRelation(this, test);
        } // GetRelation

        // ----------------------------------------------------------------------
        public virtual int CompareTo(ITimePeriod other, ITimePeriodComparer comparer)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }
            return comparer.Compare(this, other);
        } // CompareTo

        // ----------------------------------------------------------------------
        public virtual void Reset()
        {
            CheckModification();
            _start = TimeSpec.MinPeriodDate;
            _duration = TimeSpec.MaxPeriodDuration;
            _end = TimeSpec.MaxPeriodDate;
        } // Reset

        // ----------------------------------------------------------------------
        public string GetDescription(ITimeFormatter formatter = null)
        {
            return Format(formatter ?? TimeFormatter.Instance);
        } // GetDescription

        // ----------------------------------------------------------------------
        protected virtual string Format(ITimeFormatter formatter)
        {
            return formatter.GetPeriod(_start, _end, _duration);
        } // Format

        // ----------------------------------------------------------------------
        public override string ToString()
        {
            return GetDescription();
        } // ToString

        // ----------------------------------------------------------------------
        public sealed override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return IsEqual(obj);
        } // Equals

        // ----------------------------------------------------------------------
        protected virtual bool IsEqual(object obj)
        {
            return HasSameData(obj as TimeBlock);
        } // IsEqual

        // ----------------------------------------------------------------------
        private bool HasSameData(TimeBlock comp)
        {
            return _start == comp._start && _end == comp._end && IsReadOnly == comp.IsReadOnly;
        } // HasSameData

        // ----------------------------------------------------------------------
        public sealed override int GetHashCode()
        {
            return HashTool.AddHashCode(GetType().GetHashCode(), ComputeHashCode());
        } // GetHashCode

        // ----------------------------------------------------------------------
        protected virtual int ComputeHashCode()
        {
            return HashTool.ComputeHashCode(IsReadOnly, _start, _end, _duration);
        } // ComputeHashCode

        // ----------------------------------------------------------------------
        protected void CheckModification()
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException("TimeBlock is read-only");
            }
        } // CheckModification

        // ----------------------------------------------------------------------
        // members
        private DateTime _start;

        private TimeSpan _duration;
        private DateTime _end;  // cache
    } // class TimeBlock
}