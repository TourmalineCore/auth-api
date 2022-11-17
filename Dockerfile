FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["backend/TourmalineCore.Authentication.Service/TourmalineCore.Authentication.Service.csproj", "TourmalineCore.Authentication.Service/"]
COPY ["backend/Data/Data.csproj", "Data/"]
RUN dotnet restore "TourmalineCore.Authentication.Service/TourmalineCore.Authentication.Service.csproj"
WORKDIR "/src/src/Data"
COPY . .
WORKDIR "/src/src/TourmalineCore.Authentication.Service"
COPY . .
RUN dotnet build "backend/TourmalineCore.Authentication.Service/TourmalineCore.Authentication.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "backend/TourmalineCore.Authentication.Service/TourmalineCore.Authentication.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TourmalineCore.Authentication.Service.dll"]
