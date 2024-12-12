# inner-circle-auth-api

## Launch docker containers

1. You need to create an internal network for configuring interaction between different back-end services.  
You can do it using the following command in your terminal: `docker network create ic-backend-deb`.  
Note: If you already has this network, skip this step.

2. Execute the command `docker-compose up -d` from source folder

## Configurations

- MockForPullRequest - used in PR pipeline to run the service in isolation (no external deps) and run its Karate tests against it
- MockForDevelopment - used locally when you run the service in Visual Studio e.g. in Debug and don't want to spin up any external deps
- LocalEnvForDevelopment - used locally when you run the service in Visual Studio and you want to connect to its external deps from Local Env
- ProdForDevelopment - used locally when you run the service in Visual Studio and want to connect to its external deps from Prod specially dedicated Local Development Tenant
- ProdForDeployment - used when we run the service in Prod, it shouldn't contain any secrets, it should be a Release build, using real Prod external deps

## Database scheme 

This project is based on the [TourmalineCore.AspNetCore.JwtAuthentication](https://github.com/TourmalineCore/TourmalineCore.AspNetCore.JwtAuthentication/tree/master/JwtAuthentication.Identity) library which contains [Microsoft Identity Platform](https://learn.microsoft.com/en-us/entra/identity-platform/), so the database schema is inherited from them.

```mermaid
erDiagram
    AspNetRoleClaims{
        int4 Id PK
        int8 RoleId FK
        text ClaimType
        text ClaimValue
    }

    AspNetRoles{
        int8 Id PK
        varchar Name
        varchar NormalizedName
        text ConcurrencyStamp
    }

    AspNetUserClaims{
        int4 Id PK
        int8 UserId FK
        text ClaimType
        text ClaimValue
    }

    AspNetUserLogins{
        text LoginProvider PK
        text ProviderKey PK
        text ProviderDisplayName
        int8 UserId FK
    }

    AspNetUserRoles{
        int8 UserId PK, FK
        int8 RoleId PK, FK
    }

    AspNetUserTokens{
        int8 UserId PK, FK
        text LoginProvider PK
        text Name PK
        text Value
    }

    AspNetUsers{
        int8 Id PK
        bool IsBlocked
        varchar UserName
        varchar NormalizedUserName
        varchar Email
        varchar NormalizedEmail
        bool EmailConfirmed
        text PasswordHash
        text SecurityStamp
        text ConcurrencyStamp
        text PhoneNumber
        bool PhoneNumberConfirmed
        bool TwoFactorEnabled
        timestamptz LockoutEnd
        bool LockoutEnabled
        int4 AccessFailedCount
        int8 AccountId
    }

    RefreshToken{
        int8 Id
        uuid Value
        timestamptz ExpiresIn
        bool IsActive
        text ClientFingerPrint
        int8 UserId FK
        timestamptz ExpiredAtUtc
    }

    __EFMigrationsHistory{
        varchar MigrationId
        varchar ProductVersion
    }

    AspNetRoles ||--|{ AspNetRoleClaims : has
    AspNetRoles ||--|{ AspNetUserRoles : has
    AspNetRoles ||--|{ AspNetRoleClaims : has
    AspNetUsers ||--|{ AspNetUserRoles : has
    AspNetUsers ||--|{ AspNetUserLogins : has
    AspNetUsers ||--|{ AspNetUserClaims : has
    AspNetUsers ||--|{ RefreshToken : has
    AspNetUsers ||--|{ AspNetUserTokens : has
```