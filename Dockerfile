FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

#Copy the csproj file and restore any dependencies via Nuget 
COPY *.csproj
RUN dotnet restore

#Copy the project files and build our release
COPY . ./
RUN dotnet publish -c Release  -o out

#Generate runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
EXPOSE 5000
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "HotelBookingApp.dll"]
