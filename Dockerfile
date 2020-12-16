#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["m183-shovel-knight-security.csproj", "./"]
RUN dotnet restore "m183-shovel-knight-security.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "m183-shovel-knight-security.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "m183-shovel-knight-security.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "m183-shovel-knight-security.dll", "dev-certs https --trust"]
