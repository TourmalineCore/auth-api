using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(
            new
            {
                Id = 1L,
                IsBlocked = false,
                UserName = "ceo@tourmalinecore.com",
                NormalizedUserName = "CEO@TOURMALINECORE.COM",
                EmailConfirmed = false,
                PasswordHash =
                    "AQAAAAEAACcQAAAAEEim42ocT0sL9H818pD8yMxUDtA3GUCe+SDwlxl0geUX56G2GEvu7G6DKHjXMHowlA==",
                SecurityStamp = "LCZLJS5K7YP2FO2NFI2P6CQZVJLWEYZZ",
                ConcurrencyStamp = "15cfcf9e-94f3-4b65-884c-e03f1588fa20",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                AccountId = 1L
            },
            new
            {
                Id = 3L,
                IsBlocked = false,
                UserName = "trial@tourmalinecore.com",
                NormalizedUserName = "TRIAL@TOURMALINECORE.COM",
                EmailConfirmed = false,
                PasswordHash = "AQAAAAIAAYagAAAAEMjyazORDOYnqIl3xzIAWWw1SBxxuOZ9yuh84b/RLk46r3JlVzkeMjbXZGOmHSu9mw==",
                SecurityStamp = "90c670cf-30e0-4de8-84db-83fd5ad03e94",
                ConcurrencyStamp = "598bfed1-7f02-4ba9-bf1d-2d4a6b5c9fda",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                AccountId = 3L
            },
            new
            {
                Id = 2L,
                IsBlocked = false,
                UserName = "inner-circle-admin@tourmalinecore.com",
                NormalizedUserName = "INNER-CIRCLE-ADMIN@TOURMALINECORE.COM",
                EmailConfirmed = false,
                PasswordHash =
                    "AQAAAAEAACcQAAAAEHeZpxgabtPpnFcKHWwBDedWSCuW7l6V0Kz/RmxCayiauzadB9IZHoqilHnSOPhrFQ==",
                SecurityStamp = "BSBX3FSL64CHWY2LDLEJLXO27HC43KX4",
                ConcurrencyStamp = "6ef5ca6b-8ff7-4431-84f1-c04b1b4198b3",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                AccountId = 2L
            });
    }
}