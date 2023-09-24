FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["zh-kod/zh-kod.csproj", "zh-kod/"]
RUN dotnet restore "zh-kod/zh-kod.csproj"
COPY . .
WORKDIR "/src/zh-kod"
RUN dotnet build "zh-kod.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "zh-kod.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "zh-kod.dll"]
