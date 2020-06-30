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

        public async Task<List<WeatherForecast>> GetWeatherEntriesAsync(string strCurrentUser)
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

        public Task<bool> DeleteObservation(WeatherForecast weatherForecast)
        {
            var id = weatherForecast.Id;
            var observationToDelete = _context.weatherForeCast.Where(obs => obs.Id == id).FirstOrDefault<WeatherForecast>();
            _context.weatherForeCast.Remove(observationToDelete);
            _context.SaveChanges();
            return Task.FromResult(true);
        }
    }
}
