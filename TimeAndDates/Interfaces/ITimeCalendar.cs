using System;
using System.Globalization;
using TimeAndDates.Enums;

namespace TimeAndDates.Interfaces
{
    // ------------------------------------------------------------------------
	public interface ITimeCalendar : ITimePeriodMapper
	{

			// ----------------------------------------------------------------------
		CultureInfo Culture { get; }

		// ----------------------------------------------------------------------
		TimeSpan StartOffset { get; }

		// ----------------------------------------------------------------------
		TimeSpan EndOffset { get; }

		// ----------------------------------------------------------------------
		YearMonth YearBaseMonth { get; }

		// ----------------------------------------------------------------------
		YearMonth FiscalYearBaseMonth { get; }

		// ----------------------------------------------------------------------
		DayOfWeek FiscalFirstDayOfYear { get; }

		// ----------------------------------------------------------------------
		DayOfWeek FirstDayOfWeek { get; }

	    int GetYear ( DateTime time );

		// ----------------------------------------------------------------------
		int GetMonth( DateTime time );

		// ----------------------------------------------------------------------
		int GetHour( DateTime time );

		// ----------------------------------------------------------------------
		int GetMinute( DateTime time );

		// ----------------------------------------------------------------------
		int GetDayOfMonth( DateTime time );

		// ----------------------------------------------------------------------
		DayOfWeek GetDayOfWeek( DateTime time );

		// ----------------------------------------------------------------------
		int GetDaysInMonth( int year, int month );

		// ----------------------------------------------------------------------
		string GetMonthName( int month );

		// ----------------------------------------------------------------------
		string GetDayName( DayOfWeek dayOfWeek );

		// ----------------------------------------------------------------------
		int GetWeekOfYear( DateTime time );

		// ----------------------------------------------------------------------
		DateTime GetStartOfYearWeek( int year, int weekOfYear );


	} // class ITimeCalendar
}