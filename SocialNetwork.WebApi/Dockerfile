﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5051

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SocialNetwork.WebApi/SocialNetwork.WebApi.csproj", "SocialNetwork.WebApi/"]
RUN dotnet restore "SocialNetwork.WebApi/SocialNetwork.WebApi.csproj"
COPY . .
WORKDIR "/src/SocialNetwork.WebApi"
RUN dotnet build "SocialNetwork.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SocialNetwork.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SocialNetwork.WebApi.dll"]
