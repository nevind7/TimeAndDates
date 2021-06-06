using System;
using TimeAndDates.Interfaces;
using TimeAndDates.Utilities;

namespace TimeAndDates
{
// ------------------------------------------------------------------------
	public abstract class MinuteTimeRange : CalendarTimeRange
	{

		// ----------------------------------------------------------------------
		protected MinuteTimeRange( int year, int month, int day, int hour, int minute, int minuteCount ) :
			this( year, month, day, hour, minute, minuteCount, new TimeCalendar() )
		{
		} // MinuteTimeRange

		// ----------------------------------------------------------------------
		protected MinuteTimeRange( int year, int month, int day, int hour, int minute, int minuteCount, ITimeCalendar calendar ) :
			base( GetPeriodOf( year, month, day, hour, minute, minuteCount ), calendar )
		{
			this.minute = new DateTime( year, month, day, hour, minute, 0 );
			this.minuteCount = minuteCount;
			endMinute = this.minute.AddMinutes( minuteCount );
		} // MinuteTimeRange

		// ----------------------------------------------------------------------
		public int StartYear => minute.Year; // StartYear

		// ----------------------------------------------------------------------
		public int StartMonth => minute.Month; // StartMonth

		// ----------------------------------------------------------------------
		public int StartDay => minute.Day; // StartDay

		// ----------------------------------------------------------------------
		public int StartHour => minute.Hour; // StartHour

		// ----------------------------------------------------------------------
		public int StartMinute => minute.Minute; // StartMinute

		// ----------------------------------------------------------------------
		public int EndYear => endMinute.Year; // EndYear

		// ----------------------------------------------------------------------
		public int EndMonth => endMinute.Month; // EndMonth

		// ----------------------------------------------------------------------
		public int EndDay => endMinute.Day; // EndDay

		// ----------------------------------------------------------------------
		public int EndHour => endMinute.Hour; // EndHour

		// ----------------------------------------------------------------------
		public int EndMinute => endMinute.Minute; // EndMinute

		// ----------------------------------------------------------------------
		public int MinuteCount => minuteCount; // MinuteCount

		// ----------------------------------------------------------------------
		protected override bool IsEqual( object obj )
		{
			return base.IsEqual( obj ) && HasSameData( obj as MinuteTimeRange );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( MinuteTimeRange comp )
		{
			return minute == comp.minute && minuteCount == comp.minuteCount && endMinute == comp.endMinute;
		} // HasSameData

		// ----------------------------------------------------------------------
		protected override int ComputeHashCode()
		{
			return HashTool.ComputeHashCode( base.ComputeHashCode(), minute, minuteCount, endMinute );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		private static TimeRange GetPeriodOf( int year, int month, int day, int hour, int minute, int minuteCount )
		{
			if ( minuteCount < 1 )
			{
				throw new ArgumentOutOfRangeException( "minuteCount" );
			}

			DateTime start = new DateTime( year, month, day, hour, minute, 0 );
			DateTime end = start.AddMinutes( minuteCount );
			return new TimeRange( start, end );
		} // GetPeriodOf

		// ----------------------------------------------------------------------
		// members
		private readonly DateTime minute;
		private readonly int minuteCount;
		private readonly DateTime endMinute; // cache

	} // class MinuteTimeRange
}