FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY *.sln .
COPY BSA-WebAPI2/*.csproj ./BSA-WebAPI2/
COPY BSA-WebAPI.Tests/*.csproj ./BSA-WebAPI.Tests/
COPY BSA-WebAPI.IntegratedTests/*.csproj ./BSA-WebAPI.IntegratedTests/
RUN dotnet restore

COPY BSA-WebAPI2/. ./BSA-WebAPI2/
WORKDIR /app/BSA-WebAPI2
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
EXPOSE 4040
ENV ASPNETCORE_URLS=http://*:4040
ENTRYPOINT ["dotnet", "BSA-WebAPI.dll"]