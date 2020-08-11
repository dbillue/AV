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

        public DbSet<BirthState> birthState { get; set; }
        public DbSet<Person> person { get; set; }
        public DbSet<Pet> pet { get; set; }
        public DbSet<PetTypes> petType { get; set; }
    }
}
