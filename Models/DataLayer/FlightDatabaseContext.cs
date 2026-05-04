using Group3Flight.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Group3Flight.Models.DataLayer
{
    public class FlightDatabaseContext : DbContext
    {
        public FlightDatabaseContext(DbContextOptions<FlightDatabaseContext> options)
            : base(options) { }
        public DbSet<Flight> Flight { get; set; } = null!;
        public DbSet<Airline> Airline { get; set; } = null!;
        public DbSet<Reservation> Reservation { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConfigureFlight());
            modelBuilder.ApplyConfiguration(new ConfigureAirline());
        }

    }
}
