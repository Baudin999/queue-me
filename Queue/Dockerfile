FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Queue/Queue.csproj", "Queue/"]
RUN dotnet restore "Queue/Queue.csproj"
COPY . .
WORKDIR "/src/Queue"
RUN dotnet build "Queue.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Queue.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Queue.dll"]
