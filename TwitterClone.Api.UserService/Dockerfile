#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["TwitterClone.Api.UserService/TwitterClone.Api.UserService.csproj", "TwitterClone.Api.UserService/"]
RUN dotnet restore "TwitterClone.Api.UserService/TwitterClone.Api.UserService.csproj"
COPY . .
WORKDIR "/src/TwitterClone.Api.UserService"
RUN dotnet build "TwitterClone.Api.UserService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TwitterClone.Api.UserService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TwitterClone.Api.UserService.dll"]