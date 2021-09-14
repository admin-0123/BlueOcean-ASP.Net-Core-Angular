FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:8080

COPY ./Backend/*.csproj ./
RUN dotnet restore ./Virta.csproj

COPY ./Backend/ ./
RUN dotnet publish ./Virta.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Virta.dll"]
