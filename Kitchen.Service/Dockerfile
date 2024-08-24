FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY Kitchen.Service/*.csproj ./Kitchen.Service/
RUN dotnet restore ./Kitchen.Service/Kitchen.Service.csproj

COPY . .
WORKDIR /app/Kitchen.Service
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/Kitchen.Service/out .

EXPOSE 80

ENTRYPOINT ["dotnet", "Kitchen.Service.dll"]