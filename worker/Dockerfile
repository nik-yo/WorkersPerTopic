FROM mcr.microsoft.com/dotnet/aspnet:8.0

COPY ./WorkerApp/WorkerApp/bin/Release/net8.0 app/
WORKDIR /app

ENTRYPOINT [ "dotnet", "WorkerApp.dll" ]