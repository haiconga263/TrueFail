using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Common.Helpers
{
    public class TimeHelper
    {
        public static DateTime FirstDateOfWeek(int year, int weekOfYear)
        {
            if (weekOfYear < 1)
            {
                throw new Exception("format of weekOfYear isn't correct!");
            }

            // Get FirstDate of current year
            DateTime jan1 = new DateTime(year, 1, 1);

            // Get dayoffset of Monday
            int daysOffset = jan1.DayOfWeek - DayOfWeek.Monday;

            DateTime firstMonday = DateTime.MinValue;

            // If daysOffset == 0 => this is Monday and first Monday is 1/1/year
            if (daysOffset == 0)
            {
                firstMonday = jan1;
            }
            else
            {
                firstMonday = jan1.AddDays(daysOffset);
            }

            // calculate from day of current number week
            var result = firstMonday.AddDays((daysOffset == 0 ? weekOfYear - 1 : weekOfYear - 2) * 7);
            return result;
        }
        public static DateTime FirstDateOfMonth(int year, int monthOfYear)
        {
            if (monthOfYear < 1 || monthOfYear > 12)
            {
                throw new Exception("format of monthOfYear isn't correct!");
            }
            var result = new DateTime(year, monthOfYear, 1);
            return result;
        }
        public static DateTime LastDateOfMonth(int year, int monthOfYear)
        {
            if (monthOfYear < 1 || monthOfYear > 12)
            {
                throw new Exception("format of monthOfYear isn't correct!");
            }
            if (monthOfYear == 12)
            {
                return new DateTime(year, monthOfYear, 1).AddDays(30);
            }
            return new DateTime(year, monthOfYear + 1, 1).AddDays(-1);
        }
        public static DateTime FirstDateOfQuarter(int year, int quarterOfYear)
        {
            if (quarterOfYear < 1 || quarterOfYear > 4)
            {
                throw new Exception("format of quarterOfYear isn't correct!");
            }
            var result = new DateTime(year, (quarterOfYear - 1) * 3 + 1, 1);
            return result;
        }
        public static DateTime LastDateOfQuarter(int year, int quarterOfYear)
        {
            if (quarterOfYear < 1 || quarterOfYear > 4)
            {
                throw new Exception("format of quarterOfYear isn't correct!");
            }
            if (quarterOfYear == 4)
            {
                return new DateTime(year, 12, 31);
            }

            return new DateTime(year, quarterOfYear * 3 + 1, 1).AddDays(-1);
        }
        public static int GetWeekOfYear(DateTime dt)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(dt);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                dt = dt.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
        public static int GetQuarterOfYear(DateTime dt)
        {
            if (dt.Month >= 1 && dt.Month < 4)
            {
                return 1;
            }
            else if (dt.Month >= 4 && dt.Month < 7)
            {
                return 2;
            }
            else if (dt.Month >= 8 && dt.Month < 10)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }
    }
}
