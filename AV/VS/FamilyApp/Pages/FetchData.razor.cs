using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FamilyApp.Model;
using FamilyApp.Utils;

namespace FamilyApp.Pages
{
    public partial class FetchData
    {
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }
        List<WeatherForecast> forecasts;
        WeatherForecast weatherForecast = new WeatherForecast();
        SeriLog_Logger seriLogger = new SeriLog_Logger();

        bool ShowPopup;
        bool error;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var user = (await authenticationStateTask).User;
                forecasts = await ForecastService.GetWeatherEntriesAsync(user.Identity.Name);
                seriLogger.WriteInformation("Current authenticated user: " + user.Identity.Name);
                seriLogger.WriteInformation("Weather observation count: " + forecasts.Count.ToString());
            } catch (Exception ex) {
                error = true;
                seriLogger.WriteError(ex.Message);
            }
        }

        void ClosePopup()
        {
            ShowPopup = false;
        }

        void AddNewForeCast()
        {
            try
            {
                weatherForecast = new WeatherForecast();
                weatherForecast.Id = 0;
                ShowPopup = true;
                logger.LogInformation("Add new weather observation instantiated...");
            } catch (Exception ex) {
                error = true;
                seriLogger.WriteError(ex.Message);
            }
        }

        async Task SaveObservation()
        {
            try
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
                    seriLogger.WriteInformation("New weather observation saved.");
                }
                else
                {
                    logger.LogInformation("Weather observation saved...");
                    seriLogger.WriteInformation("Weather observation saved.");
                }

                forecasts = await ForecastService.GetWeatherEntriesAsync(user.Identity.Name);
            } catch (Exception ex) {
                error = true;
                seriLogger.WriteError(ex.Message);
            }
        }

        void EditForecast(WeatherForecast weatherObservation)
        {
            try
            {
                weatherForecast = weatherObservation;
                ShowPopup = true;
            } catch (Exception ex) {
                error = true;
                seriLogger.WriteError(ex.Message);
            }
        }

        async Task DeleteObservation()
        {
            try
            {
                var user = (await authenticationStateTask).User;
                var result = ForecastService.DeleteObservation(weatherForecast);
                forecasts = await ForecastService.GetWeatherEntriesAsync(user.Identity.Name);
                seriLogger.WriteInformation("Weather obeservation deleted.");
            } catch (Exception ex) {
                error = true;
                seriLogger.WriteError(ex.Message);
            }
        }
    }
}
