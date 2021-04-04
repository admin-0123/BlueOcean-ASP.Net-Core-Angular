FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

COPY ./Api/*.csproj ./
RUN dotnet restore

COPY ./Api/. ./

RUN dotnet publish -o out

FROM mcr.microsoft.com/dotnet/sdk:5.0

WORKDIR /app
COPY --from=build /app/out .
COPY --from=build /app/Views ./Views
ENTRYPOINT ["dotnet", "VirtaApi.dll"]
