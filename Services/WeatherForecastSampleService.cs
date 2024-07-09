using System;
using WebApplication1.Interface;

namespace WebApplication1.Services
{
    public class WeatherForecastSampleService : IWeatherForecastServices
    {
        // find current hour
        readonly int currentHour = DateTime.Now.Hour;
        // set max hours used in CreateHourlyForecast()
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

        // return static data
        public IEnumerable<WeatherForecast> CreateWeatherForecast(DateTime startDate, int startRange = 0, int endRange = 7)
        {
            // create list to store forecasts
            List<WeatherForecast> weeklyForecasts = new List<WeatherForecast>();

            // collect forecasts for each day in between the range
            for(int i = startRange; i < endRange; i++)
            {
                weeklyForecasts.Add(CollectForecastData(startDate.AddDays(i)));
            }

            return weeklyForecasts;
        }

        // create hourly forecast based on the current hour 
        public IEnumerable<WeatherForecast> CreateHourlyForecast(DateTime date)
        {
            // create list to store forecasts
            List<WeatherForecast> hourlyForecasts = new List<WeatherForecast>();

            // calculate hours until the rest of the day
            int endRange = maxHours - currentHour;

            // collect forecasts for each hour
            for (int i = currentHour; i < currentHour + endRange; i++)
            {
                hourlyForecasts.Add(CollectForecastData(date.AddDays(i), i));
            }

            return hourlyForecasts;
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
                if (hour != 12)
                {
                    hour -= 12;
                }
                return hour.ToString() + "PM";
            }
            else if (hour == 24)
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
