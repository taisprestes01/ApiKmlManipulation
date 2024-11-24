FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ApiKmlManipulation ./ApiKmlManipulation

WORKDIR /app/ApiKmlManipulation
RUN dotnet restore ApiKmlManipulation.csproj
RUN dotnet publish ApiKmlManipulation.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/out ./

COPY ApiKmlManipulation/Data ./Data/

ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

ENTRYPOINT ["dotnet", "ApiKmlManipulation.dll"]
