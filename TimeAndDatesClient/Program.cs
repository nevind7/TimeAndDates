using System;
using TimeAndDates;
using TimeAndDates.Interfaces;

namespace TimeAndDatesClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var timeRange = new TimeRange(DateTime.Now, DateTime.Now.AddHours(7));

            Business myBusiness = new Business();

            var timeBlock = new TimeBlock(DateTime.Now, DateTime.Now.AddHours(4));

            myBusiness.BusinessHours.Add(timeRange);
            myBusiness.BusinessHours.Add(timeBlock);

            foreach (var hours in myBusiness.BusinessHours)
            {
                Console.WriteLine(hours.ToString());
            }

           
        }
    }

    public class Business
    {
        //public TimeRange 
        public TimePeriodCollection BusinessHours = new TimePeriodCollection();
    }


}
