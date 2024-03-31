FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Presentation/Presentation.csproj", "Presentation/"]
COPY ["Infrastructure.DataAccess/Infrastructure.DataAccess.csproj", "Infrastructure.DataAccess/"]
COPY ["Application.Abstractions/Application.Abstractions.csproj", "Application.Abstractions/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Application.Dto/Application.Dto.csproj", "Application.Dto/"]
COPY ["Application/Application/Application.csproj", "Application/Application/"]
COPY ["Application/Application.Contracts/Application.Contracts.csproj", "Application/Application.Contracts/"]
RUN dotnet restore "Presentation/Presentation.csproj"
RUN dotnet dev-certs https
COPY . .
WORKDIR "/src/Presentation"
RUN dotnet build "Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.dll"]
