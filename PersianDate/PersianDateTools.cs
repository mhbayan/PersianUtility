using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersianUtility
{
    public delegate string PersianConverter(DateTime date);

    public static class DateTools
    {
        /// <summary>
        /// Get persian date with time as string
        /// </summary>
        /// <param name="Date">System DateTime</param>
        /// <returns>Persian date with time like this : 1397/1/1 12:00:00</returns>
        public static string GetFullPersianDateTime(DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return string.Format("{0}/{1}/{2} {3}", pc.GetYear(date), pc.GetMonth(date), pc.GetDayOfMonth(date), date.ToString("HH:mm:ss"));
        }

        /// <summary>
        /// Get persian date with short time as string
        /// </summary>
        /// <param name="Date">System DateTime</param>
        /// <returns>Persian date with time like this : 1397/1/1 12:00</returns>
        public static string GetShortPersianDateTime(DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return string.Format("{0}/{1}/{2} {3}", pc.GetYear(date), pc.GetMonth(date), pc.GetDayOfMonth(date), date.ToString("HH:mm"));
        }

        /// <summary>
        /// Get persian date as string
        /// </summary>
        /// <param name="Date">System DateTime</param>
        /// <returns>Persian date with time like this : 1397/1/1</returns>
        public static string GetShortPersianDate(DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return string.Format("{0}/{1}/{2}", pc.GetYear(date), pc.GetMonth(date), pc.GetDayOfMonth(date));
        }

        /// <summary>
        /// Convert Persian Date To Local DateTime
        /// </summary>
        /// <param name="persianDate">Persian DateTime String In This Format : yyyy/mm/dd hh:mm:ss</param>
        /// <returns>DateTime In Local</returns>
        public static DateTime GetLocal(string persianDate)
        {
            try
            {

                PersianCalendar pc = new PersianCalendar();
                DateTime PersianDate = DateTime.Parse(persianDate);
                DateTime dt = new DateTime(PersianDate.Year, PersianDate.Month, PersianDate.Day, PersianDate.Hour, PersianDate.Minute, PersianDate.Second, PersianDate.Millisecond, pc);
                return dt;
            }
            catch (Exception ex)
            {
                return DateTime.Now;
                throw;
            }

        }

        /// <summary>
        /// Convert unix time to normal local DateTime
        /// </summary>
        /// <param name="unixTime">unixTime as milli seconds in Local</param>
        ///<param name="dateTimeType">normal or UTC</param>
        /// <returns>Normal local or UTC dateTime</returns>
        public static DateTime UnixToDateTime(double unixTime, DateTimeType? dateTimeType = DateTimeType.Local)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 1, 0, 0, 0, Calendar.CurrentEra);
            DateTime normalTime = new DateTime();
            if (dateTimeType == DateTimeType.Local)
            {
                normalTime = dtDateTime.ToLocalTime().AddMilliseconds(unixTime);
            }
            if (dateTimeType == DateTimeType.UTC)
            {
                normalTime = dtDateTime.ToUniversalTime().AddMilliseconds(unixTime);
            }

            return normalTime;

        }


        /// <summary>
        /// Convert unix time to normal or UTC persian string DateTime
        /// </summary>
        /// <param name="unixTime">nixTime as milli seconds in Type (normal or UTC)</param>
        /// <param name="dateTimeType">normal or UTC</param>
        /// <param name="persianConverter">Persian date converter</param>
        /// <returns>String Persian Date</returns>
        public static string UnixToDateTime(double unixTime, PersianConverter persianConverter, DateTimeType? dateTimeType = DateTimeType.Local)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 1, 0, 0, 0, Calendar.CurrentEra);
            DateTime normalTime = new DateTime();
            if (dateTimeType == DateTimeType.Local)
            {
                normalTime = dtDateTime.ToLocalTime().AddMilliseconds(unixTime);
            }
            if (dateTimeType == DateTimeType.UTC)
            {
                normalTime = dtDateTime.ToUniversalTime().AddMilliseconds(unixTime);
            }

            return persianConverter(normalTime);

        }



        public enum DateTimeType
        {
            Local,
            UTC
        }
    }
}
