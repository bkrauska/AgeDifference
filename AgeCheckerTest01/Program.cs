using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgeCheckerTest01 {
    enum AgeRangeType { Months, Weeks, Years }
    
    class Program {
        static void Main(string[] args) {
            var birthday = new DateTime(2008, 1, 12);
            var dateTimeNow = new DateTime(2011, 1, 11);

            Console.WriteLine("Original Function Outputs");
            Console.WriteLine("*************************");

            Console.WriteLine("Months: {0}", OriginalAgeDifference(AgeRangeType.Months, birthday, dateTimeNow));
            Console.WriteLine("Weeks: {0}", OriginalAgeDifference(AgeRangeType.Weeks, birthday, dateTimeNow));
            Console.WriteLine("Years: {0}", OriginalAgeDifference(AgeRangeType.Years, birthday, dateTimeNow));

            Console.WriteLine();

            Console.WriteLine("Fixed Function Outputs");
            Console.WriteLine("*************************");

            Console.WriteLine("Months: {0}", AgeDifference(AgeRangeType.Months, birthday, dateTimeNow));
            Console.WriteLine("Weeks: {0}", AgeDifference(AgeRangeType.Weeks, birthday, dateTimeNow));
            Console.WriteLine("Years: {0}", AgeDifference(AgeRangeType.Years, birthday, dateTimeNow));

            Console.Write("\nClick any key to continue...");
            Console.ReadKey();
        }

        static int AgeDifference(AgeRangeType ageType, System.DateTime startDate, System.DateTime endDate) {
            int diff = 0;

            var ageInYears = endDate.Year - startDate.Year;
            if (startDate > endDate.AddYears(-ageInYears))
                ageInYears -= 1;

            var newStart = startDate.AddYears(ageInYears);
            
            switch (ageType) {
                case AgeRangeType.Months:
                    var monthsOld = 0;
                    while (newStart.AddMonths(monthsOld + 1) <= endDate.Date) {
                        monthsOld++;
                    }
                    diff = ageInYears * 12 + monthsOld;
                    break;
                case AgeRangeType.Weeks:
                    var TS = endDate.Date - newStart.Date;
                    diff = ageInYears * 52 + (TS.Days / 7);
                    break;
                default:
                    var age = endDate.Year - startDate.Year;
                    if (startDate > endDate.AddYears(-age))
                        age -= 1;
                    diff = age;
                    break;
            }

            return diff;
        }

        static int OriginalAgeDifference(AgeRangeType ageType, System.DateTime startDate, System.DateTime endDate) {
            double diff = 0.0;

            System.TimeSpan TS = new System.TimeSpan(endDate.Ticks - startDate.Ticks);
            switch (ageType) {        
                case AgeRangeType.Months:
                    diff = TS.TotalDays / 365 * 12;
                    break;           
                case AgeRangeType.Weeks:
                    diff = TS.TotalDays / 365 * 52;
                    break;
                default:
                    diff = TS.TotalDays / 365;
                    break;  
            }

            return (int)diff; // return Math.floor
        }
    }
}
