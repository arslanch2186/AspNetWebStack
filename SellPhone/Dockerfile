#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SellPhone/SellPhone.csproj", "SellPhone/"]
COPY ["SellPhone.Services/SellPhone.Services.csproj", "SellPhone.Services/"]
COPY ["SellPhone.Db/SellPhone.Db.csproj", "SellPhone.Db/"]
COPY ["SellPhone.Models/SellPhone.Models.csproj", "SellPhone.Models/"]
RUN dotnet restore "SellPhone/SellPhone.csproj"
COPY . .
WORKDIR "/src/SellPhone"
RUN dotnet build "SellPhone.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SellPhone.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SellPhone.dll"]