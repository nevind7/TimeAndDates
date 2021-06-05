using System;
using TimeAndDates;

namespace TimeAndDatesClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var timeRange = new TimeRange(DateTime.Now, DateTime.Now.AddHours(4));



            var timeBlock = new TimeBlock(DateTime.Now, DateTime.Now.AddHours(4));


            Console.WriteLine(timeRange.DurationDescription);
        }
    }
}
