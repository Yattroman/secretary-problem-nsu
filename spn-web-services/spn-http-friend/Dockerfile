FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["secretary-problem-nsu/spn-http-friend/spn-http-friend.csproj", "secretary-problem-nsu/spn-http-friend/"]
RUN dotnet restore "spn-http-friend/spn-http-friend.csproj"
COPY . .
WORKDIR "/src/spn-http-friend"
RUN dotnet build "spn-http-friend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "spn-http-friend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "spn-http-friend.dll"]
