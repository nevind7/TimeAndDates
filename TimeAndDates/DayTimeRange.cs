using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeAndDates.Interfaces;
using TimeAndDates.Utilities;

namespace TimeAndDates
{
   // ------------------------------------------------------------------------
	public abstract class DayTimeRange : CalendarTimeRange
	{

		// ----------------------------------------------------------------------
		protected DayTimeRange( int year, int month, int day, int dayCount ) :
			this( year, month, day, dayCount, new TimeCalendar() )
		{
		} // DayTimeRange

		// ----------------------------------------------------------------------
		protected DayTimeRange( int year, int month, int day, int dayCount, ITimeCalendar calendar ) :
			base( GetPeriodOf( year, month, day, dayCount ), calendar )
		{
			this.day = new DateTime( year, month, day );
			this.dayCount = dayCount;
			endDay = calendar.MapEnd( this.day.AddDays( dayCount ) );
		} // DayTimeRange

		// ----------------------------------------------------------------------
		public int StartYear => day.Year; // StartYear

		// ----------------------------------------------------------------------
		public int StartMonth => day.Month; // StartMonth

		// ----------------------------------------------------------------------
		public int StartDay => day.Day; // StartDay

		// ----------------------------------------------------------------------
		public int EndYear => endDay.Year; // EndYear

		// ----------------------------------------------------------------------
		public int EndMonth => endDay.Month; // EndMonth

		// ----------------------------------------------------------------------
		public int EndDay => endDay.Day; // EndDay

		// ----------------------------------------------------------------------
		public int DayCount => dayCount; // DayCount

		// ----------------------------------------------------------------------
		public DayOfWeek StartDayOfWeek => Calendar.GetDayOfWeek( day ); // StartDayOfWeek

		// ----------------------------------------------------------------------
		public string StartDayName => Calendar.GetDayName( StartDayOfWeek ); // StartDayName

		// ----------------------------------------------------------------------
		public DayOfWeek EndDayOfWeek => Calendar.GetDayOfWeek( endDay ); // EndDayOfWeek

		// ----------------------------------------------------------------------
		public string EndDayName => Calendar.GetDayName( EndDayOfWeek ); // EndDayName

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetHours()
		{
			TimePeriodCollection hours = new TimePeriodCollection();
			DateTime date = day;
			for ( int day = 0; day < dayCount; day++ )
			{
				DateTime curDay = date.AddDays( day );
				for ( int hour = 0; hour < TimeSpec.HoursPerDay; hour++ )
				{
					hours.Add( new Hour( curDay.AddHours( hour ), Calendar ) );
				}
			}
			return hours;
		} // GetHours

		// ----------------------------------------------------------------------
		protected override bool IsEqual( object obj )
		{
			return base.IsEqual( obj ) && HasSameData( obj as DayTimeRange );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( DayTimeRange comp )
		{
			return 
				day == comp.day && 
				dayCount == comp.dayCount && 
				endDay == comp.endDay;
		} // HasSameData

		// ----------------------------------------------------------------------
		protected override int ComputeHashCode()
		{
			return HashTool.ComputeHashCode( base.ComputeHashCode(), day, dayCount, endDay );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		private static TimeRange GetPeriodOf( int year, int month, int day, int dayCount )
		{
			if ( dayCount < 1 )
			{
				throw new ArgumentOutOfRangeException( "dayCount" );
			}

			DateTime start = new DateTime( year, month, day );
			DateTime end = start.AddDays( dayCount );
			return new TimeRange( start, end );
		} // GetPeriodOf

		// ----------------------------------------------------------------------
		// members
		private readonly DateTime day;
		private readonly int dayCount;
		private readonly DateTime endDay; // cache

	} // class DayTimeRange
}
