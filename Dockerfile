FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
COPY --from=publish publish/ .
ENTRYPOINT ["dotnet", "People.dll"]