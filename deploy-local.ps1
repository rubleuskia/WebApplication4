& dotnet publish -o ./deploy

Set-Location -Path "./deploy/"

& dotnet WebApplication4.dll