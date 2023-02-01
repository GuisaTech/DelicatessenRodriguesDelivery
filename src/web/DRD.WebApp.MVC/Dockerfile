#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/web/DRD.WebApp.MVC/DRD.WebApp.MVC.csproj", "src/web/DRD.WebApp.MVC/"]
RUN dotnet restore "src/web/DRD.WebApp.MVC/DRD.WebApp.MVC.csproj"
COPY . .
WORKDIR "/src/src/web/DRD.WebApp.MVC"
RUN dotnet build "DRD.WebApp.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DRD.WebApp.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DRD.WebApp.MVC.dll"]