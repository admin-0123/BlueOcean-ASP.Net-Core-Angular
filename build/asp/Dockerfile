FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY ./Backend/ ./
RUN dotnet restore ./Virta.csproj
RUN dotnet publish ./Virta.csproj -c Release -o out

# RUN dotnet tool install -g dotnet-ef
# ENV PATH="${PATH}:/root/.dotnet/tools"

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app

COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "Virta.dll"]
