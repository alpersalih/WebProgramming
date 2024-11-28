using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebProgramming.Models
{
    public class BarberContext : DbContext
    {
        public DbSet<Service> Services { get; set; }
        public DbSet<WorkingHours> WorkingHours { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("YourConnectionStringHere");
        }
    }

}
