FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY /Core/Core.csproj ./Core/
COPY /DatabaseAccess/DatabaseAccess.csproj ./DatabaseAccess/
COPY /WebApplication4/WebApplication4.csproj ./WebApplication4/

RUN dotnet restore /app/WebApplication4/

COPY ./ ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
#ENV ASPNETCORE_URLS http://*:$PORT
ENTRYPOINT ["dotnet", "WebApplication4.dll"]