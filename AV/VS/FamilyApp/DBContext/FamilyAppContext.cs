using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FamilyApp.Model;

namespace FamilyApp.DBContext
{
    public class FamilyAppContext : IdentityDbContext
    {
        public FamilyAppContext(DbContextOptions<FamilyAppContext> options)
            : base(options)
        { }

        public DbSet<WeatherForecast> weatherForeCast {  get; set; }
    }
}
