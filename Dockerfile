FROM mcr microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY published/HotelBooking.dll
EXPOSE 5000/tcp
ENTRYPOINT ["dotnet", "HotelBooking.dll"]
