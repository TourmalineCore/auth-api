using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using TourmalineCore.AspNetCore.JwtAuthentication.Identity;

namespace DataAccess;

public class AppDbContext : TourmalineDbContext<User, long>
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }

  public AppDbContext() : base(new DbContextOptions<AppDbContext>())
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
