#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["STCAPI/STCAPI.csproj", "STCAPI/"]
COPY ["STCAPI.Infrastructure.Implementation/STCAPI.Infrastructure.Implementation.csproj", "STCAPI.Infrastructure.Implementation/"]
COPY ["STAAPI.Infrastructure.Repository/STAAPI.Infrastructure.Repository.csproj", "STAAPI.Infrastructure.Repository/"]
COPY ["STCAPI.Core.Entities/STCAPI.Core.Entities.csproj", "STCAPI.Core.Entities/"]
COPY ["STCAPI.Core.ViewModel/STCAPI.Core.ViewModel.csproj", "STCAPI.Core.ViewModel/"]
COPY ["MailHelper/MailHelper.csproj", "MailHelper/"]
COPY ["CommonHelper/CommonHelper.csproj", "CommonHelper/"]
RUN dotnet restore "STCAPI/STCAPI.csproj"
COPY . .
WORKDIR "/src/STCAPI"
RUN dotnet build "STCAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "STCAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "STCAPI.dll"]