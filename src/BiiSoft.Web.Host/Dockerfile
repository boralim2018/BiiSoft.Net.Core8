FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/BiiSoft.Web.Host/BiiSoft.Web.Host.csproj", "src/BiiSoft.Web.Host/"]
COPY ["src/BiiSoft.Web.Core/BiiSoft.Web.Core.csproj", "src/BiiSoft.Web.Core/"]
COPY ["src/BiiSoft.Application/BiiSoft.Application.csproj", "src/BiiSoft.Application/"]
COPY ["src/BiiSoft.Core/BiiSoft.Core.csproj", "src/BiiSoft.Core/"]
COPY ["src/BiiSoft.EntityFrameworkCore/BiiSoft.EntityFrameworkCore.csproj", "src/BiiSoft.EntityFrameworkCore/"]
WORKDIR "/src/src/BiiSoft.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/BiiSoft.Web.Host", "src/BiiSoft.Web.Host"]
COPY ["src/BiiSoft.Web.Core", "src/BiiSoft.Web.Core"]
COPY ["src/BiiSoft.Application", "src/BiiSoft.Application"]
COPY ["src/BiiSoft.Core", "src/BiiSoft.Core"]
COPY ["src/BiiSoft.EntityFrameworkCore", "src/BiiSoft.EntityFrameworkCore"]
WORKDIR "/src/src/BiiSoft.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "BiiSoft.Web.Host.dll"]
