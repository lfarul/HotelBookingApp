FROM microsoft/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY published/HotelBooking.dll ./
EXPOSE 5000/tcp
ENTRYPOINT ["dotnet", "HotelBooking.dll"]
