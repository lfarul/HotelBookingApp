FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY publish/HotelBooking.dll ./
EXPOSE 5000/tcp
ENTRYPOINT ["dotnet", "HotelBooking.dll"]
