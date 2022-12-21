using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.ConstrainedExecution;

namespace Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(new { Id = 50L, 
                IsBlocked = false, 
                UserName = "ceo@tourmalinecore.com",
                NormalizedUserName = "CEO@TOURMALINECORE.COM",
                EmailConfirmed = false,
                PasswordHash = "AQAAAAEAACcQAAAAEEim42ocT0sL9H818pD8yMxUDtA3GUCe+SDwlxl0geUX56G2GEvu7G6DKHjXMHowlA==",
                SecurityStamp = "LCZLJS5K7YP2FO2NFI2P6CQZVJLWEYZZ",
                ConcurrencyStamp = "15cfcf9e-94f3-4b65-884c-e03f1588fa20",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                AccountId = 1L
            });
        }
    }
}
