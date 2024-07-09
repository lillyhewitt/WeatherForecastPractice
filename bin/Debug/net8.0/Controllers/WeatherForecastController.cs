using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication1.Interface;
using WebApplication1.Services;
using WebApplication1.Validations;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        // private readonly WeatherForecastService _display;
        private readonly IWeatherForecastServices _displaySample;
        private readonly IDateValidations _dateValidator;
        private readonly ITimeValidations _timeValidator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastServices weatherServices, IDateValidations dateServices, ITimeValidations timeServices)
        {
            _logger = logger;
            // _display = new WeatherForecastService();
            // _displaySample = new WeatherForecastSampleService();
            _displaySample = weatherServices;
            _dateValidator = dateServices;
            _timeValidator = timeServices;
        }

        // display weather forecasts of the upcoming week
        [HttpGet("/WeeklyForecasts")]
        public IEnumerable<WeatherForecast> GetWeekOfForecasts()
        {
            // return _display.CreateWeatherForecast(DateTime.Now);
            return _displaySample.CreateWeatherForecast(DateTime.Now);
        }

        // display weather forecasts within a specified range
        [HttpGet("/ForecastsRange")]
        public IActionResult GetWeekOfForecastsOverRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            // make sure start date is before the end date
            if (startDate > endDate)
            {
                return BadRequest("End date cannot be before start date");
            }
            // make sure start date is within the six month limit
            if (!_dateValidator.IsDateWithinMonths(startDate, 6))
            {
                return BadRequest("Start date cannot exceed the 6 month limit.");
            }
            // make sure end date is within the six month limit
            if (!_dateValidator.IsDateWithinMonths(endDate, 6))
            {
                return BadRequest("End date cannot exceed the 6 month limit.");
            }

            // find range of specified date ranges 
            int range = (endDate - startDate).Days + 1;
            // date compare instead DateTime.compare
            //int range = DateTime.Compare(startDate, endDate) + 1;

            // display each forecast per date
            // var result = _display.CreateWeatherForecast(startDate, 0, range);
            var result = _displaySample.CreateWeatherForecast(startDate, 0, range);
            return Ok(result);
        }

        // display a single weather forecast for the current day
        [HttpGet("/SingleForecast")]
        public WeatherForecast GetOneForecast()
        {
            // display a single random weather forecast for the current day
            // return _display.CreateSingleWeatherForecast();
            return _displaySample.CreateSingleWeatherForecast();
        }

        // pass in a date and return that date plus the next day
        [HttpGet("/SingleForecastsWithinTwoYears")]
        public IActionResult GetTwoForecastsWithinTwoYears([FromQuery] DateTime startDate)
        {
            // make sure start date is within two years
            if (!_dateValidator.IsDateWithinYears(startDate, 2))
            {
                return BadRequest("Dates cannot exceed the 2 year limit.");
            }

            // display 2 weather forecasts including start date and the next day
            // var result = _display.CreateWeatherForecast(startDate, 0, 2);
            var result = _displaySample.CreateWeatherForecast(startDate, 0, 2);
            return Ok(result);
        }

        // get hourly forecast
        [HttpGet("/HourlyForecast")]
        public IEnumerable<WeatherForecast> GetHourlyForecast([FromQuery] DateTime? date)
        {
            // find next 24 hours of weather 
            // return _display.CreateHourlyForecast(date ?? DateTime.Now);
            return _displaySample.CreateHourlyForecast(date ?? DateTime.Now);
        }

        // specific hour of specified day forecast
        [HttpGet("/HourForecast")]
        public IActionResult GetHourlyForecast([FromQuery] DateTime date, [FromQuery] int hour)
        {
            // check if specified hour is between 1-24
            if(!_timeValidator.IsValid(hour))
            {
                return BadRequest("Hour must be between 1-24.");
            }

            // find the specific hourly forecast for the specified day
            // var result = _display.CreateHourForecast(date, hour);
            var result = _displaySample.CreateHourForecast(date, hour);
            return Ok(result);
        }
    }
}

