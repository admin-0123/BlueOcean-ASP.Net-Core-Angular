FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY ./Backend/*.csproj ./
RUN dotnet restore ./Virta.csproj

COPY ./Backend/. ./
RUN dotnet publish ./Virta.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Virta.dll"]
