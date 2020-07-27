FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
COPY . /app
WORKDIR /app
EXPOSE 5000
ENTRYPOINT ["dotnet", "run"]
