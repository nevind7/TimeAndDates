using System;
using TimeAndDates;

using System.Text.Json;
using System.Text.Json.Serialization;


namespace TimeAndDatesClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Business myBusiness = new Business();
            myBusiness.WorkWeek.SetWeek();
         


            string jsonString = JsonSerializer.Serialize(myBusiness);

            Console.WriteLine(jsonString);
        }
    }

    public class Business
    {
        public Week WorkWeek { get; set; } = new Week();
    }
}