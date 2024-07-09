using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WebApplication1.Interface;

namespace WebApplication1.Services
{
    public class WeatherForecastService : IWeatherForecastServices
    {
        // find the current hour make readonly
        private readonly int currentHour = DateTime.Now.Hour;
        const int maxHours = 24;

        // initialize string of weather descriptions
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        // create weather forecast of the current day
        public WeatherForecast CreateSingleWeatherForecast()
        {
            return CollectForecastData(DateTime.Now.AddDays(0));
        }
        /*
        // create a week of weather forecasts based on specified date
        public IEnumerable<WeatherForecast> CreateWeatherForecast(DateTime startDate)
        {
            return CreateWeatherForecast(startDate, 0, 7);
        }

        // create range of weather forecasts based on specified date and end range
        // use optional parameters
        public IEnumerable<WeatherForecast> CreateWeatherForecast(DateTime startDate, int endRange)
        {
            return CreateWeatherForecast(startDate, 0, endRange);
        }*/

        // create range of weather forcasts based on specified date and start and end ranges
        public IEnumerable<WeatherForecast> CreateWeatherForecast(DateTime startDate, int startRange = 0, int endRange = 7)
        {
            // display 5 sequential weather forecasts based on the upcoming week
            return Enumerable
                .Range(startRange, endRange)
                .Select(index => CollectForecastData(startDate.AddDays(index)))
                .ToArray();
        }

        // create hourly forecast based on the current hour 
        public IEnumerable<WeatherForecast> CreateHourlyForecast(DateTime date)
        {
            // calculate hours until the rest of the day
            int endRange = maxHours - currentHour;

            // display hourly forecast for the remainder of the day
            return Enumerable
                .Range(currentHour, endRange)
                .Select(index => CollectForecastData(date.AddDays(0), index))
                .ToArray();
        }

        // create hourly forecast based on the current hour 
        public WeatherForecast CreateHourForecast(DateTime date, int hour)
        {
            // display weather forecast of the current day
            return CollectForecastData(date.AddDays(0), hour);
        }

        // find AM/PM Format
        public string GetActualHour(int hour)
        {
            // check if hour is past the AM 
            if (hour > 11)
            {
                if(hour != 12)
                {
                    hour -= 12;
                }
                return hour.ToString() + "PM";
            }
            else if(hour == 24)
            {
                hour = 12;
            }
            return hour.ToString() + "AM";
        }

        // create forecast data based on date and time
        public WeatherForecast CollectForecastData(DateTime date, int? hour = null)
        {
            return new WeatherForecast
            {
                Date = DateOnly.FromDateTime(date),
                Hour = GetActualHour(hour ?? currentHour),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }
    }
}
