using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TimeAndDates.Interfaces;

namespace TimeAndDates
{
    public class WeekDayCollection : IWeekDayCollection
    {
        private readonly List<IWeekDay> _weekDays = new();

        public bool IsReadOnly => false; // IsReadOnly
        public int Count => _weekDays.Count; // Count

        public IEnumerator<IWeekDay> GetEnumerator()
        {
            return _weekDays.GetEnumerator();
        }

        // ----------------------------------------------------------------------
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        } // IEnumerable.GetEnumerator

        // ----------------------------------------------------------------------
        public virtual void Add(IWeekDay item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            if (_weekDays.Exists(t => t.DayOfWeek == item.DayOfWeek))
            {
                Remove(item.DayOfWeek);
            }

            _weekDays.Add(item);
        } // Add

        public virtual void Clear()
        {
            _weekDays.Clear();
        }

        public IWeekDay GetHours(DayOfWeek day)
        {
            return _weekDays.Find(t => t.DayOfWeek == day);
        }

        public virtual bool Contains(IWeekDay item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            return _weekDays.Contains(item);
        }

        public virtual void CopyTo(IWeekDay[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            _weekDays.CopyTo(array, arrayIndex);
        }

        public virtual bool Remove(DayOfWeek item)
        {
            var dayToRemove = _weekDays.First(t => t.DayOfWeek == item);
            return _weekDays.Remove(dayToRemove);
        }

        public virtual bool Remove(IWeekDay item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            return _weekDays.Remove(item);
        }

        public virtual int IndexOf(IWeekDay item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            return _weekDays.IndexOf(item);
        }

        public virtual void Insert(int index, IWeekDay item)
        {
            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            _weekDays.Insert(index, item);
        }

        public virtual void RemoveAt(int index)
        {
            _weekDays.RemoveAt(index);
        }

        public IWeekDay this[int index]
        {
            get => _weekDays[index];
            set => _weekDays[index] = value;
        }

        //public DayOfWeek DayOfWeek { get; set; }
        //public TimeRange Hours { get; set; }

        public virtual bool ContainsDay(IWeekDay test)
        {
            return _weekDays.Exists(t => t.DayOfWeek == test.DayOfWeek);
        }

        public virtual void AddAll(IEnumerable<IWeekDay> days)
        {
            if (_weekDays == null)
            {
                throw new ArgumentNullException("weekDays");
            }

            foreach (IWeekDay weekday in days)
            {
                Add(weekday);
            }
        }

        public virtual bool HasInsidePeriods(IWeekDay test)
        {
            if (test == null)
            {
                throw new ArgumentNullException("test");
            }

            foreach (IWeekDay weekday in _weekDays)
            {
                if (test.Hours.HasInside(weekday.Hours))
                {
                    return true;
                }
            }

            return false;
        }

        public virtual bool HasOverlaps()
        {
            bool hasOverlaps = false;

            if (Count == 2)
            {
                hasOverlaps = this[0].Hours.OverlapsWith(this[1].Hours);
            }
            else if (Count > 2)
            {
                hasOverlaps = new TimeLineMomentCollection(this).HasOverlaps();
            }

            return hasOverlaps;
        }

        public virtual bool HasOverlapPeriods(IWeekDay test)
        {
            if (test == null)
            {
                throw new ArgumentNullException("test");
            }

            foreach (IWeekDay weekday in _weekDays)
            {
                if (test.Hours.OverlapsWith(weekday.Hours))
                {
                    return true;
                }
            }

            return false;
        }

        public virtual bool HasIntersectionPeriods(DateTime test)
        {
            foreach (IWeekDay weekday in _weekDays)
            {
                if (weekday.Hours.HasInside(test))
                {
                    return true;
                }
            }

            return false;
        }

        public virtual bool HasIntersectionPeriods(IWeekDay test)
        {
            if (test == null)
            {
                throw new ArgumentNullException("test");
            }

            foreach (IWeekDay weekday in _weekDays)
            {
                if (weekday.Hours.IntersectsWith(test.Hours))
                {
                    return true;
                }
            }

            return false;
        }

        public virtual IWeekDayCollection InsidePeriods(IWeekDay test)
        {
            if (test == null)
            {
                throw new ArgumentNullException("test");
            }

            WeekDayCollection insidePeriods = new WeekDayCollection();

            foreach (IWeekDay weekday in _weekDays)
            {
                if (test.Hours.HasInside(weekday.Hours))
                {
                    insidePeriods.Add(weekday);
                }
            }

            return insidePeriods;
        }

        public virtual IWeekDayCollection OverlapPeriods(IWeekDay test)
        {
            if (test == null)
            {
                throw new ArgumentNullException("test");
            }

            WeekDayCollection overlapPeriods = new WeekDayCollection();

            foreach (IWeekDay weekday in _weekDays)
            {
                if (test.Hours.OverlapsWith(weekday.Hours))
                {
                    overlapPeriods.Add(weekday);
                }
            }

            return overlapPeriods;
        }

        public virtual IWeekDayCollection IntersectionPeriods(DateTime test)
        {
            WeekDayCollection intersectionPeriods = new WeekDayCollection();

            foreach (IWeekDay weekday in _weekDays)
            {
                if (weekday.Hours.HasInside(test))
                {
                    intersectionPeriods.Add(weekday);
                }
            }

            return intersectionPeriods;
        }

        public virtual IWeekDayCollection IntersectionPeriods(IWeekDay test)
        {
            if (test == null)
            {
                throw new ArgumentNullException("test");
            }

            WeekDayCollection intersectionPeriods = new WeekDayCollection();

            foreach (IWeekDay weekday in _weekDays)
            {
                if (test.Hours.IntersectsWith(weekday.Hours))
                {
                    intersectionPeriods.Add(weekday);
                }
            }

            return intersectionPeriods;
        }
    }
}