using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyApp.Model;

namespace FamilyApp.Pages
{
    public partial class FetchData
    {
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }
        List<WeatherForecast> forecasts;
        WeatherForecast weatherForecast = new WeatherForecast();

        bool ShowPopup;

        protected override async Task OnInitializedAsync()
        {
            var user = (await authenticationStateTask).User;
            forecasts = await ForecastService.GetWeatherEntriesAsync(user.Identity.Name);
        }

        void ClosePopup()
        {
            ShowPopup = false;
        }

        void AddNewForeCast()
        {
            weatherForecast = new WeatherForecast();
            weatherForecast.Id = 0;
            ShowPopup = true;
            logger.LogInformation("Add new weather observation instantiated...");
        }

        async Task SaveObservation()
        {
            ShowPopup = false;
            var user = (await authenticationStateTask).User;
            if (weatherForecast.Id == 0)
            {
                WeatherForecast newWeatherObservation = new WeatherForecast();
                newWeatherObservation.Date = System.DateTime.Now;
                newWeatherObservation.Summary = weatherForecast.Summary;
                newWeatherObservation.TemperatureC = Convert.ToInt32(weatherForecast.TemperatureC);
                newWeatherObservation.UserName = user.Identity.Name;
                var result = ForecastService.CreateWeatherObservationAsync(newWeatherObservation);
                logger.LogInformation("New weather observation saved...");
            }
            else
            {
                logger.LogInformation("Weather observation saved...");
            }

            forecasts = await ForecastService.GetWeatherEntriesAsync(user.Identity.Name);
        }

        void EditForecast(WeatherForecast weatherObservation)
        {
            weatherForecast = weatherObservation;
            ShowPopup = true;
            logger.LogInformation("Weather observation edited...");
        }

        async Task DeleteObservation()
        {
            var user = (await authenticationStateTask).User;
            var result = ForecastService.DeleteObservation(weatherForecast);
            logger.LogInformation("Weather observation deleted...");
            forecasts = await ForecastService.GetWeatherEntriesAsync(user.Identity.Name);
        }
    }
}
