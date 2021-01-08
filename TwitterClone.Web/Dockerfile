#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["TwitterClone.Web/TwitterClone.Web.csproj", "TwitterClone.Web/"]
RUN dotnet restore "TwitterClone.Web/TwitterClone.Web.csproj"
COPY . .
WORKDIR "/src/TwitterClone.Web"
RUN dotnet build "TwitterClone.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TwitterClone.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TwitterClone.Web.dll"]