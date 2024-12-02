using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebProgramming.Models
{
    public class BarberContext : DbContext
    {
        public BarberContext(DbContextOptions<BarberContext> options) : base(options) { }

        public DbSet<Service> Services { get; set; }
        public DbSet<WorkingHours> WorkingHours { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Service>()
                .Property(s => s.Price)
                .HasColumnType("decimal(18,2)"); // 18 basamak, 2 ondalık basamak
        }
    }


}
