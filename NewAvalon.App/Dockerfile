#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NewAvalon.App/NewAvalon.App.csproj", "NewAvalon.App/"]
COPY ["src/Modules/Catalog/NewAvalon.Catalog.Business/NewAvalon.Catalog.Business.csproj", "src/Modules/Catalog/NewAvalon.Catalog.Business/"]
COPY ["src/Common/NewAvalon.Messaging.Contracts/NewAvalon.Messaging.Contracts.csproj", "src/Common/NewAvalon.Messaging.Contracts/"]
COPY ["src/Common/NewAvalon.Boundary/NewAvalon.Boundary.csproj", "src/Common/NewAvalon.Boundary/"]
COPY ["src/Modules/UserAdministration/NewAvalon.UserAdministration.Business/NewAvalon.UserAdministration.Business.csproj", "src/Modules/UserAdministration/NewAvalon.UserAdministration.Business/"]
COPY ["src/Modules/UserAdministration/NewAvalon.UserAdministration.Boundary/NewAvalon.UserAdministration.Boundary.csproj", "src/Modules/UserAdministration/NewAvalon.UserAdministration.Boundary/"]
COPY ["src/Common/NewAvalon.Abstractions/NewAvalon.Abstractions.csproj", "src/Common/NewAvalon.Abstractions/"]
COPY ["src/Common/NewAvalon.Domain/NewAvalon.Domain.csproj", "src/Common/NewAvalon.Domain/"]
COPY ["src/Modules/UserAdministration/NewAvalon.UserAdministration.Presentation/NewAvalon.UserAdministration.Presentation.csproj", "src/Modules/UserAdministration/NewAvalon.UserAdministration.Presentation/"]
COPY ["src/Common/NewAvalon.Presentation/NewAvalon.Presentation.csproj", "src/Common/NewAvalon.Presentation/"]
COPY ["src/Modules/UserAdministration/NewAvalon.UserAdministration.Domain/NewAvalon.UserAdministration.Domain.csproj", "src/Modules/UserAdministration/NewAvalon.UserAdministration.Domain/"]
COPY ["src/Common/NewAvalon.Infrastructure/NewAvalon.Infrastructure.csproj", "src/Common/NewAvalon.Infrastructure/"]
RUN dotnet restore "NewAvalon.App/NewAvalon.App.csproj"
COPY . .
WORKDIR "/src/NewAvalon.App"
RUN dotnet build "NewAvalon.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NewAvalon.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewAvalon.App.dll"]