namespace WebApplication1.Interface
{
    public interface IWeatherForecastServices
    {
        WeatherForecast CreateSingleWeatherForecast();
        IEnumerable<WeatherForecast> CreateWeatherForecast(DateTime startDate, int startRange = 0, int endRange = 7);
        IEnumerable<WeatherForecast> CreateHourlyForecast(DateTime date);
        WeatherForecast CreateHourForecast(DateTime date, int hour);
        string GetActualHour(int hour);
        WeatherForecast CollectForecastData(DateTime date, int? hour = null);
    }
}
