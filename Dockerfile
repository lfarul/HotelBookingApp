FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "HotelBookingApp.dll"]

#Zmiana1
#Zmiana2
#Zmiana3


#FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
#WORKDIR /app

#Copy the csproj file and restore any dependencies via Nuget 
#COPY *.csproj
#RUN dotnet restore

#Copy the project files and build our release
#COPY . ./
#RUN dotnet publish -c Release  -o out

#Generate runtime image
#FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
#WORKDIR /app
#EXPOSE 5000
#COPY --from=build-env /app/out .
#ENTRYPOINT ["dotnet", "HotelBookingApp.dll"]



