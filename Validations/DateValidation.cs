using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Globalization;
using WebApplication1.Interface;

namespace WebApplication1.Validations
{
    public class DateValidation : IDateValidations
    {
        // returns true if date is within a month limit
        public bool IsDateWithinMonths(DateTime date, int months)
        {
            // find dates six months before and after current date
            DateTime monthsAfterCurrentDate = DateTime.Now.AddMonths(months);

            // return false if date is not within month limit, return true if within limit
            if (date > monthsAfterCurrentDate)
            {
                return false;
            }
            return true;
        }

        // returns true if date is within a year limit
        public bool IsDateWithinYears(DateTime startDate, int years)
        {
            // find date two years from current date
            DateTime yearsAfterCurrentDate = DateTime.Now.AddYears(years);

            // return false if date is not within year limit, return true if within limit
            if (startDate > yearsAfterCurrentDate)
            {
                return false;
            }
            return true;
        }
    }
}
