Set-Location -Path "./WebApplication4/wwwroot/angular"

& npm i
& npm run build

Set-Location -Path "./../../../"

& dotnet publish -o ./deploy

Set-Location -Path "./deploy/"

& dotnet WebApplication4.dll