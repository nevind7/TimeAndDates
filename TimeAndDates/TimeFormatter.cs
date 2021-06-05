using System;
using System.Globalization;
using System.Text;

using TimeAndDates.enums;
using TimeAndDates.Interfaces;

namespace TimeAndDates
{
    // ------------------------------------------------------------------------
    public class TimeFormatter : ITimeFormatter
    {
        // ----------------------------------------------------------------------
        public TimeFormatter() :
            this(CultureInfo.CurrentCulture)
        {
        } // TimeFormatter

        // ----------------------------------------------------------------------
        public TimeFormatter(CultureInfo culture = null,
            string contextSeparator = "; ", string endSeparator = " - ",
            string durationSeparator = " | ",
            string dateTimeFormat = null,
            string shortDateFormat = null,
            string longTimeFormat = null,
            string shortTimeFormat = null,
            DurationFormatType durationType = DurationFormatType.Compact,
            bool useDurationSeconds = false,
            bool useIsoIntervalNotation = false,
            string durationItemSeparator = " ",
            string durationLastItemSeparator = " ",
            string durationValueSeparator = " ",
            string intervalStartClosed = "[",
            string intervalStartOpen = "(",
            string intervalStartOpenIso = "]",
            string intervalEndClosed = "]",
            string intervalEndOpen = ")",
            string intervalEndOpenIso = "[")
        {
            culture ??= CultureInfo.CurrentCulture;
            Culture = culture;
            ListSeparator = culture.TextInfo.ListSeparator;
            ContextSeparator = contextSeparator;
            StartEndSeparator = endSeparator;
            DurationSeparator = durationSeparator;
            DurationItemSeparator = durationItemSeparator;
            DurationLastItemSeparator = durationLastItemSeparator;
            DurationValueSeparator = durationValueSeparator;
            IntervalStartClosed = intervalStartClosed;
            IntervalStartOpen = intervalStartOpen;
            IntervalStartOpenIso = intervalStartOpenIso;
            IntervalEndClosed = intervalEndClosed;
            IntervalEndOpen = intervalEndOpen;
            IntervalEndOpenIso = intervalEndOpenIso;
            DateTimeFormat = dateTimeFormat;
            ShortDateFormat = shortDateFormat;
            LongTimeFormat = longTimeFormat;
            ShortTimeFormat = shortTimeFormat;
            DurationType = durationType;
            UseDurationSeconds = useDurationSeconds;
            UseIsoIntervalNotation = useIsoIntervalNotation;
        } // TimeFormatter

        // ----------------------------------------------------------------------
        public static TimeFormatter Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (Mutex)
                {
                    _instance ??= new TimeFormatter();
                }
                return _instance;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                lock (Mutex)
                {
                    _instance = value;
                }
            }
        } // Instance

        // ----------------------------------------------------------------------
        public CultureInfo Culture { get; }

        // ----------------------------------------------------------------------
        public string ListSeparator { get; }

        // ----------------------------------------------------------------------
        public string ContextSeparator { get; }

        // ----------------------------------------------------------------------
        public string StartEndSeparator { get; }

        // ----------------------------------------------------------------------
        public string DurationSeparator { get; }

        // ----------------------------------------------------------------------
        public string DurationItemSeparator { get; }

        // ----------------------------------------------------------------------
        public string DurationLastItemSeparator { get; }

        // ----------------------------------------------------------------------
        public string DurationValueSeparator { get; }

        // ----------------------------------------------------------------------
        public string IntervalStartClosed { get; }

        // ----------------------------------------------------------------------
        public string IntervalStartOpen { get; }

        // ----------------------------------------------------------------------
        public string IntervalStartOpenIso { get; }

        // ----------------------------------------------------------------------
        public string IntervalEndClosed { get; }

        // ----------------------------------------------------------------------
        public string IntervalEndOpen { get; }

        // ----------------------------------------------------------------------
        public string IntervalEndOpenIso { get; }

        // ----------------------------------------------------------------------
        public string DateTimeFormat { get; }

        // ----------------------------------------------------------------------
        public string ShortDateFormat { get; }

        // ----------------------------------------------------------------------
        public string LongTimeFormat { get; }

        // ----------------------------------------------------------------------
        public string ShortTimeFormat { get; }

        // ----------------------------------------------------------------------
        public DurationFormatType DurationType { get; }

        // ----------------------------------------------------------------------
        public bool UseDurationSeconds { get; }

        // ----------------------------------------------------------------------
        public bool UseIsoIntervalNotation { get; }

        #region Collection

        // ----------------------------------------------------------------------
        public virtual string GetCollection(int count)
        {
            return $"Count = {count}";
        } // GetCollection

        // ----------------------------------------------------------------------
        public virtual string GetCollectionPeriod(int count, DateTime start, DateTime end, TimeSpan duration)
        {
            return $"{GetCollection(count)}{ListSeparator} {GetPeriod(start, end, duration)}";
        } // GetCollectionPeriod

        #endregion Collection

        #region DateTime

        // ----------------------------------------------------------------------
        public string GetDateTime(DateTime dateTime)
        {
            return !string.IsNullOrEmpty(DateTimeFormat) ? dateTime.ToString(DateTimeFormat) : dateTime.ToString(Culture);
        } // GetDateTime

        // ----------------------------------------------------------------------
        public string GetShortDate(DateTime dateTime)
        {
            return !string.IsNullOrEmpty(ShortDateFormat) ? dateTime.ToString(ShortDateFormat) : dateTime.ToString("d");
        } // GetShortDate

        // ----------------------------------------------------------------------
        public string GetLongTime(DateTime dateTime)
        {
            return !string.IsNullOrEmpty(LongTimeFormat) ? dateTime.ToString(LongTimeFormat) : dateTime.ToString("T");
        } // GetLongTime

        // ----------------------------------------------------------------------
        public string GetShortTime(DateTime dateTime)
        {
            return !string.IsNullOrEmpty(ShortTimeFormat) ? dateTime.ToString(ShortTimeFormat) : dateTime.ToString("t");
        } // GetShortTime

        #endregion DateTime

        #region Duration

        // ----------------------------------------------------------------------
        public string GetPeriod(DateTime start, DateTime end)
        {
            return GetPeriod(start, end, end - start);
        } // GetPeriod

        // ----------------------------------------------------------------------
        public string GetDuration(TimeSpan timeSpan)
        {
            return GetDuration(timeSpan, DurationType);
        } // GetDuration

        // ----------------------------------------------------------------------
        public string GetDuration(TimeSpan timeSpan, DurationFormatType durationFormatType)
        {
            switch (durationFormatType)
            {
                case DurationFormatType.Detailed:
                    var days = (int)timeSpan.TotalDays;
                    var hours = timeSpan.Hours;
                    var minutes = timeSpan.Minutes;
                    var seconds = UseDurationSeconds ? timeSpan.Seconds : 0;
                    return GetDuration(0, 0, days, hours, minutes, seconds);

                default:
                    return UseDurationSeconds ? $"{timeSpan.Days}.{timeSpan.Hours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}"
                        : $"{timeSpan.Days}.{timeSpan.Hours:00}:{timeSpan.Minutes:00}";
            }
        } // GetDuration

        // ----------------------------------------------------------------------
        public virtual string GetDuration(int years, int months, int days, int hours, int minutes, int seconds)
        {
            var sb = new StringBuilder();

            // years(s)
            if (years != 0)
            {
                sb.Append(years);
                sb.Append(DurationValueSeparator);
                sb.Append(years == 1 ? Strings.TimeSpanYear : Strings.TimeSpanYears);
            }

            // month(s)
            if (months != 0)
            {
                if (sb.Length > 0)
                {
                    sb.Append(days == 0 && hours == 0 && minutes == 0 && seconds == 0 ? DurationLastItemSeparator : DurationItemSeparator);
                }
                sb.Append(months);
                sb.Append(DurationValueSeparator);
                sb.Append(months == 1 ? Strings.TimeSpanMonth : Strings.TimeSpanMonths);
            }

            // day(s)
            if (days != 0)
            {
                if (sb.Length > 0)
                {
                    sb.Append(hours == 0 && minutes == 0 && seconds == 0 ? DurationLastItemSeparator : DurationItemSeparator);
                }
                sb.Append(days);
                sb.Append(DurationValueSeparator);
                sb.Append(days == 1 ? Strings.TimeSpanDay : Strings.TimeSpanDays);
            }

            // hour(s)
            if (hours != 0)
            {
                if (sb.Length > 0)
                {
                    sb.Append(minutes == 0 && seconds == 0 ? DurationLastItemSeparator : DurationItemSeparator);
                }
                sb.Append(hours);
                sb.Append(DurationValueSeparator);
                sb.Append(hours == 1 ? Strings.TimeSpanHour : Strings.TimeSpanHours);
            }

            // minute(s)
            if (minutes != 0)
            {
                if (sb.Length > 0)
                {
                    sb.Append(seconds == 0 ? DurationLastItemSeparator : DurationItemSeparator);
                }
                sb.Append(minutes);
                sb.Append(DurationValueSeparator);
                sb.Append(minutes == 1 ? Strings.TimeSpanMinute : Strings.TimeSpanMinutes);
            }

            // second(s)
            if (seconds != 0)
            {
                if (sb.Length > 0)
                {
                    sb.Append(DurationLastItemSeparator);
                }
                sb.Append(seconds);
                sb.Append(DurationValueSeparator);
                sb.Append(seconds == 1 ? Strings.TimeSpanSecond : Strings.TimeSpanSeconds);
            }

            return sb.ToString();
        } // GetDuration

        #endregion Duration

        #region Period

        // ----------------------------------------------------------------------
        public virtual string GetPeriod(DateTime start, DateTime end, TimeSpan duration)
        {
            if (end < start)
            {
                throw new ArgumentOutOfRangeException(nameof(end));
            }

            var hasTimeOfDay = TimeTool.HasTimeOfDay(start);

            // no duration - schow start date (optionally with the time part)
            if (duration == TimeSpec.MinPeriodDuration)
            {
                return hasTimeOfDay ? GetDateTime(start) : GetShortDate(start);
            }

            // within one day: show full start, end time and suration
            if (TimeCompare.IsSameDay(start, end))
            {
                return GetDateTime(start) + StartEndSeparator + GetLongTime(end) + DurationSeparator + GetDuration(duration);
            }

            // show start date, end date and duration (optionally with the time part)
            var endHasTimeOfDay = TimeTool.HasTimeOfDay(end);
            var hasTimeOfDays = hasTimeOfDay || endHasTimeOfDay;
            var part = hasTimeOfDays ? GetDateTime(start) : GetShortDate(start);
            var endPart = hasTimeOfDays ? GetDateTime(end) : GetShortDate(end);
            return part + StartEndSeparator + endPart + DurationSeparator + GetDuration(duration);
        } // GetPeriod

        // ----------------------------------------------------------------------
        public string GetCalendarPeriod(string start, string end, TimeSpan duration)
        {
            var timePeriod = start.Equals(end) ? start : start + StartEndSeparator + end;
            return timePeriod + DurationSeparator + GetDuration(duration);
        } // GetCalendarPeriod

        // ----------------------------------------------------------------------
        public string GetCalendarPeriod(string context, string start, string end, TimeSpan duration)
        {
            var timePeriod = start.Equals(end) ? start : start + StartEndSeparator + end;
            return context + ContextSeparator + timePeriod + DurationSeparator + GetDuration(duration);
        } // GetCalendarPeriod

        // ----------------------------------------------------------------------
        public string GetCalendarPeriod(string context, string endContext, string start, string end, TimeSpan duration)
        {
            var contextPeriod = context.Equals(endContext) ? context : context + StartEndSeparator + endContext;
            var timePeriod = start.Equals(end) ? start : start + StartEndSeparator + end;
            return contextPeriod + ContextSeparator + timePeriod + DurationSeparator + GetDuration(duration);
        } // GetCalendarPeriod

        #endregion Period

        #region Interval

        // ----------------------------------------------------------------------
        public string GetInterval(DateTime start, DateTime end,
            IntervalEdge edge, IntervalEdge endEdge, TimeSpan duration)
        {
            if (end < start)
            {
                throw new ArgumentOutOfRangeException(nameof(end));
            }

            var sb = new StringBuilder();

            // interval start
            switch (edge)
            {
                case IntervalEdge.Closed:
                    sb.Append(IntervalStartClosed);
                    break;

                case IntervalEdge.Open:
                    sb.Append(UseIsoIntervalNotation ? IntervalStartOpenIso : IntervalStartOpen);
                    break;
            }

            var addDuration = true;
            var hasTimeOfDay = TimeTool.HasTimeOfDay(start);

            // no duration - schow start date (optionally with the time part)
            if (duration == TimeSpec.MinPeriodDuration)
            {
                sb.Append(hasTimeOfDay ? GetDateTime(start) : GetShortDate(start));
                addDuration = false;
            }
            // within one day: show full start, end time and suration
            else if (TimeCompare.IsSameDay(start, end))
            {
                sb.Append(GetDateTime(start));
                sb.Append(StartEndSeparator);
                sb.Append(GetLongTime(end));
            }
            else
            {
                var endHasTimeOfDay = TimeTool.HasTimeOfDay(start);
                var hasTimeOfDays = hasTimeOfDay || endHasTimeOfDay;
                if (hasTimeOfDays)
                {
                    sb.Append(GetDateTime(start));
                    sb.Append(StartEndSeparator);
                    sb.Append(GetDateTime(end));
                }
                else
                {
                    sb.Append(GetShortDate(start));
                    sb.Append(StartEndSeparator);
                    sb.Append(GetShortDate(end));
                }
            }

            // interval end
            switch (endEdge)
            {
                case IntervalEdge.Closed:
                    sb.Append(IntervalEndClosed);
                    break;

                case IntervalEdge.Open:
                    sb.Append(UseIsoIntervalNotation ? IntervalEndOpenIso : IntervalEndOpen);
                    break;
            }

            // duration
            if (addDuration)
            {
                sb.Append(DurationSeparator);
                sb.Append(GetDuration(duration));
            }

            return sb.ToString();
        } // GetInterval

        #endregion Interval

        // ----------------------------------------------------------------------
        // members

        private static readonly object Mutex = new();
        private static volatile TimeFormatter _instance;
    } // class TimeFormatter
}