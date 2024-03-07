dotnet build "RefactorReportService\RefactorReportService\RefactorReportService.csproj" -c Debug --runtime linux-x64
dotnet publish "RefactorReportService\RefactorReportService\RefactorReportService.csproj" -c Debug --runtime linux-x64
docker-compose up -d --build web