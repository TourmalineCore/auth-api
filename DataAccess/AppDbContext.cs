using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TourmalineCore.AspNetCore.JwtAuthentication.Identity;

namespace DataAccess
{
    //Use next command in Package Manager Console to update Dev env DB
    //PM> $env:ASPNETCORE_ENVIRONMENT = 'Debug'; Update-Database

    public class AppDbContext : TourmalineDbContext<User, long>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public AppDbContext()
            : base(new DbContextOptions<AppDbContext>())
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
        public static void ConfigureContextOptions(DbContextOptionsBuilder options, string connection)
        {
            options.UseNpgsql(connection);
            options.EnableSensitiveDataLogging();
        }
    }
}