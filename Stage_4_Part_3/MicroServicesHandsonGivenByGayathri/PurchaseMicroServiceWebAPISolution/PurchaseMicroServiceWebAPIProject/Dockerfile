#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["PurchaseMicroServiceWebAPIProject/PurchaseMicroServiceWebAPIProject.csproj", "PurchaseMicroServiceWebAPIProject/"]
RUN dotnet restore "PurchaseMicroServiceWebAPIProject/PurchaseMicroServiceWebAPIProject.csproj"
COPY . .
WORKDIR "/src/PurchaseMicroServiceWebAPIProject"
RUN dotnet build "PurchaseMicroServiceWebAPIProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PurchaseMicroServiceWebAPIProject.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PurchaseMicroServiceWebAPIProject.dll"]