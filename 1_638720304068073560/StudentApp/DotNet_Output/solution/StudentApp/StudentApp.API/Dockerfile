FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["StudentApp.API/StudentApp.API.csproj", "StudentApp.API/"]
RUN dotnet restore "StudentApp.API/StudentApp.API.csproj"
COPY . .
WORKDIR "/src/StudentApp.API"
RUN dotnet build "StudentApp.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "StudentApp.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "StudentApp.API.dll"]