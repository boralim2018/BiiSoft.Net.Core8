using Abp.Extensions;
using Abp.ObjectComparators.LongComparators;
using Abp.Timing.Timezone;
using Castle.Core.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Extensions
{
    public enum DateInterval
    {
        Year = 1, 
        Month = 2,
        Day = 3,
        Hour = 4,
        Minute = 5,
        Second = 6,
        Millisecond = 7
    }

    /// <summary>
    /// DateTime Extension, add more method for DateTime Data Type
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Round method do round .Net Time Precisions from 7 digits to 6 digits.
        /// Then return new DateTime of 6 digits of precisions accepted by ISO.        
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>
        /// New DateTime from Rounded Millisecond Precisions.
        /// Ex: 
        /// .Net:2022/09/13 09:07:34.1234567 => ISO:2022/09/13 09:07:34.123457
        /// .Net:2022/09/13 09:07:34.1234565 => ISO:2022/09/13 09:07:34.123456
        /// </returns>
        public static DateTime Round(this DateTime dateTime)
        {
            return new DateTime((long)Math.Round(dateTime.Ticks / 10m, 0) * 10, dateTime.Kind);
        }

        /// <summary>
        /// Convert Utc DateTime to another DateTime of provided zone
        /// </summary>
        /// <param name="utcDate"></param>
        /// <param name="timeZoneId"></param>
        /// <returns></returns>
        public static DateTime ToZone(this DateTime utcDate, string timeZoneId)
        {
            return TimeZoneInfo.ConvertTime(utcDate, GetTimeZone(timeZoneId));
        }

        /// <summary>
        /// This method convert a giving DateTime to new Date with TimeZone with zero hour
        /// </summary>
        /// <param name="utcDate"></param>
        /// <param name="timeZoneId">Keep blank to get UTC From Date: YYYY-MM-DD 00:00:00.0000000</param>
        /// <returns>
        /// Return DateTime for TimeZone with zero hours 2022-09-01 00:00:00.0000000 as Utc
        /// </returns>
        public static DateTime StartDateZone(this DateTime utcDate, string timeZoneId)
        {
            var zoneInfo = GetTimeZone(timeZoneId);
            var dateWithZone = TimeZoneInfo.ConvertTime(utcDate, zoneInfo);
            var fromDate = dateWithZone.Date;           
            return TimeZoneInfo.ConvertTimeToUtc(fromDate, zoneInfo);
        }

        /// <summary>
        /// This method convert a giving DateTime to new Date with TimeZone with max hour
        /// </summary>
        /// <param name="utcDate"></param>
        /// <param name="timeZoneId">Keep blank to get UTC From Date: YYYY-MM-DD 23:59:59.9999999</param>
        /// <returns>
        /// Return DateTime for TimeZone with max hours 2022-09-01 23:59:59.9999999 as Utc
        /// </returns>
        public static DateTime EndDateZone(this DateTime utcDate, string timeZoneId)
        {
            var zoneInfo = GetTimeZone(timeZoneId);
            var dateWithZone = TimeZoneInfo.ConvertTime(utcDate, zoneInfo);
            var toDate = dateWithZone.End();
            return TimeZoneInfo.ConvertTimeToUtc(toDate, zoneInfo);
        }

        /// <summary>
        /// return the latest ticks of the date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>
        /// return the latest ticks of the date
        /// Ex: 2022-09-01 23:59:59.9999999
        /// </returns>
        public static DateTime End(this DateTime date)
        {
            return date.Date.AddDays(1).AddTicks(-1);
        }

        /// <summary>
        /// get TimeZoneInfo from provided time zone id
        /// the valid time zone id is get from Microsoft link :
        /// https://docs.microsoft.com/en-us/windows-hardware/manufacture/desktop/default-time-zones?view=windows-11
        /// Ex: 
        /// var zoneId = "SE Asia Standard Time";
        /// var zoneId = "Singapore Standard Time";
        /// var zoneInfos = TimeZoneInfo.GetSystemTimeZones();
        /// </summary>
        /// <param name="timeZoneId"></param>
        /// <returns>
        /// return TimeZoneInfo
        /// </returns>
        public static TimeZoneInfo GetTimeZone(string timeZoneId) {
            TimeZoneInfo zoneInfo = null;
            try
            {
                //zoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                zoneInfo = TimezoneHelper.FindTimeZoneInfo(timeZoneId);
                
            }
            catch (TimeZoneNotFoundException)
            {
                Console.WriteLine("The registry does not define the provided zone.");
            }
            catch (InvalidTimeZoneException)
            {
                Console.WriteLine("Registry data on the provided zone has been corrupted.");
            }

            return zoneInfo;
        }

        public static double MicroSecond(this DateTime date)
        {
            var newDate = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond);
            return date.Subtract(newDate).TotalMilliseconds * 1000;
        }

        public static decimal Diff(this DateTime fromDate, DateTime toDate, DateInterval interval, int rounding = 2)
        {
            decimal result;
            switch (interval)
            {
                case DateInterval.Year:
                    var dayInYear = DateTime.IsLeapYear(toDate.Year) ? 366 : 365;
                    result = toDate.Year - fromDate.Year +
                             (toDate.Month - fromDate.Month) / 12M +
                             (decimal)(toDate.Day - fromDate.Day) / dayInYear +
                             (decimal)(toDate.Hour - fromDate.Hour) / dayInYear / 24 +
                             (decimal)(toDate.Minute - fromDate.Minute) / dayInYear / 24 / 60 +
                             (decimal)(toDate.Second - fromDate.Second) / dayInYear / 24 / 3600 +
                             (decimal)(toDate.Millisecond - fromDate.Millisecond) / dayInYear / 24 / 3600000 +
                             (decimal)(toDate.MicroSecond() - fromDate.MicroSecond()) / dayInYear / 24 / 3600000000;
                    break;
                case DateInterval.Month:
                    var dayInMonth = DateTime.DaysInMonth(toDate.Year, toDate.Month);
                    result = (decimal)(toDate.Year - fromDate.Year) * 12 +
                             toDate.Month - fromDate.Month +
                             (decimal)(toDate.Day - fromDate.Day) / dayInMonth +
                             (decimal)(toDate.Hour - fromDate.Hour) / dayInMonth / 24 +
                             (decimal)(toDate.Minute - fromDate.Minute) / dayInMonth / 24 / 60 +
                             (decimal)(toDate.Second - fromDate.Second) / dayInMonth / 24 / 3600 +
                             (decimal)(toDate.Millisecond - fromDate.Millisecond) / dayInMonth / 24 / 3600000 +
                             (decimal)(toDate.MicroSecond() - fromDate.MicroSecond()) / dayInMonth / 24 / 3600000000;
                             
                    break;
                case DateInterval.Day:
                    result = (decimal)(toDate - fromDate).TotalDays;
                    break;
                case DateInterval.Hour:
                    result = (decimal)(toDate - fromDate).TotalHours;
                    break;
                case DateInterval.Minute:
                    result = (decimal)(toDate - fromDate).TotalMinutes;
                    break;
                case DateInterval.Second:
                    result = (decimal)(toDate - fromDate).TotalSeconds;
                    break;
                default: 
                    result =  (decimal)(toDate - fromDate).TotalMilliseconds; 
                    break;
            }

            var roundedValue = Math.Round(result, rounding);

            return roundedValue;
        }
    }

}
