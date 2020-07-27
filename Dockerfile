FROM microsoft/dotnet:latest
COPY . /app
WORKDIR /app
EXPOSE 5000
ENTRYPOINT ["dotnet", "run]
