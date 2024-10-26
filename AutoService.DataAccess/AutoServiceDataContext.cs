using AutoService.DataAccess.Configurations;
using AutoService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoService.DataAccess
{
    public class AutoServiceDataContext : DbContext
    {
        public AutoServiceDataContext(DbContextOptions<AutoServiceDataContext> options) : base(options)
        {    
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CarConfiguration());
            modelBuilder.ApplyConfiguration(new UserCarConfiguration());
        }

        public DbSet<UserEntity> users {  get; set; }
        public DbSet<CarEntity> cars { get; set; }
        public DbSet<UserCarEntity> users_cars { get; set; }
    }
}
