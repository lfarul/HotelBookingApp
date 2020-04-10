FROM microsoft/dotnet/core/aspnet:3.0
COPY published/HotelBooking.dll
EXPOSE 5000/tcp
ENTRYPOINT ["dotnet", "HotelBooking.dll"]
