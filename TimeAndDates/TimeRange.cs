using System;
using TimeAndDates.Enums;
using TimeAndDates.Interfaces;
using TimeAndDates.Utilities;

namespace TimeAndDates
{
    public class TimeRange : ITimeRange
    {
        // ----------------------------------------------------------------------
        public static readonly TimeRange Anytime = new(true);

        // ----------------------------------------------------------------------
        public TimeRange() :
            this(TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate)
        {
        } // TimeRange

        // ----------------------------------------------------------------------
        internal TimeRange(bool isReadOnly = false) :
            this(TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate, isReadOnly)
        {
        } // TimeRange

        // ----------------------------------------------------------------------
        public TimeRange(DateTime moment, bool isReadOnly = false) :
            this(moment, moment, isReadOnly)
        {
        } // TimeRange

        // ----------------------------------------------------------------------
        public TimeRange(DateTime start, DateTime end, bool isReadOnly = false)
        {
            if (start <= end)
            {
                this.start = start;
                _end = end;
            }
            else
            {
                _end = start;
                this.start = end;
            }
            IsReadOnly = isReadOnly;
        } // TimeRange

        // ----------------------------------------------------------------------
        public TimeRange(DateTime start, TimeSpan duration, bool isReadOnly = false)
        {
            if (duration >= TimeSpan.Zero)
            {
                this.start = start;
                _end = start.Add(duration);
            }
            else
            {
                this.start = start.Add(duration);
                _end = start;
            }
            IsReadOnly = isReadOnly;
        } // TimeRange

        // ----------------------------------------------------------------------
        public TimeRange(ITimePeriod copy)
        {
            if (copy == null)
            {
                throw new ArgumentNullException(nameof(copy));
            }
            start = copy.Start;
            _end = copy.End;
            IsReadOnly = copy.IsReadOnly;
        } // TimeRange

        // ----------------------------------------------------------------------
        protected TimeRange(ITimePeriod copy, bool isReadOnly)
        {
            if (copy == null)
            {
                throw new ArgumentNullException(nameof(copy));
            }
            start = copy.Start;
            _end = copy.End;
            IsReadOnly = isReadOnly;
        } // TimeRange

        // ----------------------------------------------------------------------
        public bool IsReadOnly { get; }

        // ----------------------------------------------------------------------
        public bool IsAnytime => !HasStart && !HasEnd; // IsAnytime

        // ----------------------------------------------------------------------
        public bool IsMoment => start.Equals(_end); // IsMoment

        // ----------------------------------------------------------------------
        public bool HasStart => start != TimeSpec.MinPeriodDate; // HasStart

        // ----------------------------------------------------------------------
        public DateTime Start
        {
            get => start;
            set
            {
                CheckModification();
                if (value > _end)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                start = value;
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
                if (value < start)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _end = value;
            }
        } // End

        // ----------------------------------------------------------------------
        public TimeSpan Duration
        {
            get => _end.Subtract(start);
            set
            {
                CheckModification();
                _end = start.Add(value);
            }
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
                start = newStart;
                _end = newEnd;
            }
            else
            {
                _end = newStart;
                start = newEnd;
            }
        } // Setup

        // ----------------------------------------------------------------------
        public virtual bool IsSamePeriod(ITimePeriod test)
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
            }
            return start == test.Start && _end == test.End;
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
        public ITimeRange Copy()
        {
            return Copy(TimeSpan.Zero);
        } // Copy

        // ----------------------------------------------------------------------
        public virtual ITimeRange Copy(TimeSpan offset)
        {
            return new TimeRange(start.Add(offset), _end.Add(offset), IsReadOnly);
        } // Copy

        // ----------------------------------------------------------------------
        public virtual void Move(TimeSpan offset)
        {
            CheckModification();
            if (offset == TimeSpan.Zero)
            {
                return;
            }
            start = start.Add(offset);
            _end = _end.Add(offset);
        } // Move

        // ----------------------------------------------------------------------
        public virtual void ExpandStartTo(DateTime moment)
        {
            CheckModification();
            if (start > moment)
            {
                start = moment;
            }
        } // ExpandStartTo

        // ----------------------------------------------------------------------
        public virtual void ExpandEndTo(DateTime moment)
        {
            CheckModification();
            if (_end < moment)
            {
                _end = moment;
            }
        } // ExpandEndTo

        // ----------------------------------------------------------------------
        public void ExpandTo(DateTime moment)
        {
            ExpandStartTo(moment);
            ExpandEndTo(moment);
        } // ExpandTo

        // ----------------------------------------------------------------------
        public void ExpandTo(ITimePeriod period)
        {
            if (period == null)
            {
                throw new ArgumentNullException(nameof(period));
            }
            ExpandStartTo(period.Start);
            ExpandEndTo(period.End);
        } // ExpandTo

        // ----------------------------------------------------------------------
        public virtual void ShrinkStartTo(DateTime moment)
        {
            CheckModification();
            if (HasInside(moment) && start < moment)
            {
                start = moment;
            }
        } // ShrinkStartTo

        // ----------------------------------------------------------------------
        public virtual void ShrinkEndTo(DateTime moment)
        {
            CheckModification();
            if (HasInside(moment) && _end > moment)
            {
                _end = moment;
            }
        } // ShrinkEndTo

        // ----------------------------------------------------------------------
        public void ShrinkTo(ITimePeriod period)
        {
            if (period == null)
            {
                throw new ArgumentNullException(nameof(period));
            }
            ShrinkStartTo(period.Start);
            ShrinkEndTo(period.End);
        } // ShrinkTo

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
        public virtual ITimeRange GetIntersection(ITimePeriod period)
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
            return new TimeRange(
                periodStart > start ? periodStart : start,
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
            start = TimeSpec.MinPeriodDate;
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
            return formatter.GetPeriod(start, _end, Duration);
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
            return HasSameData(obj as TimeRange);
        } // IsEqual

        // ----------------------------------------------------------------------
        private bool HasSameData(TimeRange comp)
        {
            return start == comp.start && _end == comp._end && IsReadOnly == comp.IsReadOnly;
        } // HasSameData

        // ----------------------------------------------------------------------
        public sealed override int GetHashCode()
        {
            return HashTool.AddHashCode(GetType().GetHashCode(), ComputeHashCode());
        } // GetHashCode

        // ----------------------------------------------------------------------
        protected virtual int ComputeHashCode()
        {
            return HashTool.ComputeHashCode(IsReadOnly, start, _end);
        } // ComputeHashCode

        // ----------------------------------------------------------------------
        protected void CheckModification()
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException("TimeRange is read-only");
            }
        } // CheckModification

        // ----------------------------------------------------------------------
        // members

        private DateTime start;
        private DateTime _end;
    } // class TimeRange
}