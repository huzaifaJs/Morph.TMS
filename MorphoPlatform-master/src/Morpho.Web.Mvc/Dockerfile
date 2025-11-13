FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli
RUN ln -s ~/.dotnet/tools/libman /usr/local/bin/libman

WORKDIR /src
COPY ["src/Morpho.Web.Mvc/Morpho.Web.Mvc.csproj", "src/Morpho.Web.Mvc/"]
COPY ["src/Morpho.Web.Core/Morpho.Web.Core.csproj", "src/Morpho.Web.Core/"]
COPY ["src/Morpho.Application/Morpho.Application.csproj", "src/Morpho.Application/"]
COPY ["src/Morpho.Core/Morpho.Core.csproj", "src/Morpho.Core/"]
COPY ["src/Morpho.EntityFrameworkCore/Morpho.EntityFrameworkCore.csproj", "src/Morpho.EntityFrameworkCore/"]
WORKDIR "/src/src/Morpho.Web.Mvc"
RUN dotnet restore 

WORKDIR /src
COPY ["src/Morpho.Web.Mvc", "src/Morpho.Web.Mvc"]
COPY ["src/Morpho.Web.Core", "src/Morpho.Web.Core"]
COPY ["src/Morpho.Application", "src/Morpho.Application"]
COPY ["src/Morpho.Core", "src/Morpho.Core"]
COPY ["src/Morpho.EntityFrameworkCore", "src/Morpho.EntityFrameworkCore"]
WORKDIR "/src/src/Morpho.Web.Mvc"
RUN libman restore
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "Morpho.Web.Mvc.dll"]
