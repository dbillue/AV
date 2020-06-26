using FamilyApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyApp.Service
{
    interface IWeatherForeCastService
    {
        Task<List<WeatherForecast>> GetWeatherEntriesAsync(string strCurrentUser);

        Task<WeatherForecast> CreateWeatherObservationAsync(WeatherForecast weatherForeCast);

        Task<bool> DeleteObservation(WeatherForecast weatherForecast);
    }
}
