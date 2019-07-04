﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoAPI.Common
{
    static class DateConvertor
    {
        public static DateTime StringToDateObject(string date)
        {
            // DateTime dateObject = DateTime.ParseExact(date, "yyyy-MM-dd", null);
            DateTime dateObject = DateTime.Parse(date);

            return dateObject.Date;
        }

        public static string DateObjectToString(DateTime DOB)
        {
            string stringStartDate = DOB.ToShortDateString();

            return stringStartDate;

        }
        /// <summary>
        /// Extension method to indicate whether an employee has an 
        /// anniversary within the next 30 days
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public static bool CompareStartDate(this DateTime startDate)
        {
            bool hasAnniversary = false;
            var currentDayOfYear = DateTime.Now.DayOfYear;
            var empStartDate = startDate.DayOfYear;
            if (empStartDate - currentDayOfYear <= 30 && empStartDate > currentDayOfYear)
            {
                hasAnniversary = true;
            }
            return hasAnniversary;
        }

        

    }
}