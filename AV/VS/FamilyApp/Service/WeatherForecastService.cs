using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FamilyApp.DBContext;
using FamilyApp.Model;

namespace FamilyApp.Service
{
    public class WeatherForecastService : IWeatherForeCastService
    {
        private readonly FamilyAppContext _context;
        public WeatherForecastService(FamilyAppContext context)
        {
            _context = context;
        }

        public async Task<List<WeatherForecast>>
            GetWeatherEntriesAsync(string strCurrentUser)
        {
            return await _context.weatherForeCast.Where(
                x => x.UserName == strCurrentUser).ToListAsync();
        }

        public Task<WeatherForecast> CreateWeatherObservationAsync(WeatherForecast weatherForecast)
        {
            _context.weatherForeCast.Add(weatherForecast);
            _context.SaveChanges();
            return Task.FromResult(weatherForecast);
        }

        public Task<WeatherForecast> DeleteObservation(WeatherForecast weatherForecast)
        {
            _context.weatherForeCast.Remove(weatherForecast);
            _context.SaveChanges();
            return Task.FromResult(weatherForecast);
        }
    }
}
