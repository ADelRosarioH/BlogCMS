FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BlogCMS.WebAPI/BlogCMS.WebAPI.csproj", "BlogCMS.WebAPI/"]
RUN dotnet restore "BlogCMS.WebAPI/BlogCMS.WebAPI.csproj"
COPY . .
WORKDIR "/src/BlogCMS.WebAPI"
RUN dotnet build "BlogCMS.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlogCMS.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogCMS.WebAPI.dll"]
