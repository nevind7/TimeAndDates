using System;
using TimeAndDates.Interfaces;
using TimeAndDates.Utilities;

namespace TimeAndDates
{
    // ------------------------------------------------------------------------
	public abstract class HourTimeRange : CalendarTimeRange
	{

		// ----------------------------------------------------------------------
		protected HourTimeRange( int year, int month, int day, int hour, int hourCount ) :
			this( year, month, day, hour, hourCount, new TimeCalendar() )
		{
		} // HourTimeRange

		// ----------------------------------------------------------------------
		protected HourTimeRange( int year, int month, int day, int hour, int hourCount, ITimeCalendar calendar ) :
			base( GetPeriodOf( year, month, day, hour, hourCount ), calendar )
		{
			this.hour = new DateTime( year, month, day, hour, 0, 0 );
			this.hourCount = hourCount;
			endHour = this.hour.AddHours( hourCount );
		} // HourTimeRange

		// ----------------------------------------------------------------------
		public int StartYear => hour.Year; // StartYear

		// ----------------------------------------------------------------------
		public int StartMonth => hour.Month; // StartMonth

		// ----------------------------------------------------------------------
		public int StartDay => hour.Day; // StartDay

		// ----------------------------------------------------------------------
		public int StartHour => hour.Hour; // StartHour

		// ----------------------------------------------------------------------
		public int EndYear => endHour.Year; // EndYear

		// ----------------------------------------------------------------------
		public int EndMonth => endHour.Month; // EndMonth

		// ----------------------------------------------------------------------
		public int EndDay => endHour.Day; // EndDay

		// ----------------------------------------------------------------------
		public int EndHour => endHour.Hour; // EndHour

		// ----------------------------------------------------------------------
		public int HourCount => hourCount; // HourCount

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetMinutes()
		{
			TimePeriodCollection minutes = new TimePeriodCollection();
			for ( int hour = 0; hour < hourCount; hour++ )
			{
				DateTime curHour = this.hour.AddHours( hour );
				for ( int minute = 0; minute < TimeSpec.MinutesPerHour; minute++ )
				{
					minutes.Add( new Minute( curHour.AddMinutes( minute ), Calendar ) );
				}
			}
			return minutes;
		} // GetMinutes

		// ----------------------------------------------------------------------
		protected override bool IsEqual( object obj )
		{
			return base.IsEqual( obj ) && HasSameData( obj as HourTimeRange );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( HourTimeRange comp )
		{
			return hour == comp.hour && hourCount == comp.hourCount && endHour == comp.endHour;
		} // HasSameData

		// ----------------------------------------------------------------------
		protected override int ComputeHashCode()
		{
			return HashTool.ComputeHashCode( base.ComputeHashCode(), hour, hourCount, endHour );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		private static TimeRange GetPeriodOf( int year, int month, int day, int hour, int hourCount )
		{
			if ( hourCount < 1 )
			{
				throw new ArgumentOutOfRangeException( "hourCount" );
			}

			DateTime start = new DateTime( year, month, day, hour, 0, 0 );
			DateTime end = start.AddHours( hourCount );
			return new TimeRange( start, end );
		} // GetPeriodOf

		// ----------------------------------------------------------------------
		// members
		private readonly DateTime hour;
		private readonly int hourCount;
		private readonly DateTime endHour; // cache

	} // class HourTimeRange
}