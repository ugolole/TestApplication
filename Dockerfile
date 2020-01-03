# FROM microsoft/dotnet:2.1.1-aspnetcore-runtime AS base

# WORKDIR /app
# EXPOSE 80
# EXPOSE 443

# FROM microsoft/dotnet:2.1.1-sdk AS build

# WORKDIR /src
# COPY ["TestApplication.csproj", "TestApplication/"]
# COPY ["TestApplication.Domain/TestApplication.Domain.csproj", "TestApplication.Domain/"]
# RUN dotnet restore "TestApplication.csproj"
# COPY . .
# WORKDIR "/src/TestApplication"
# RUN dotnet build "TestApplication.csproj" -c Release -o /app

# FROM build AS publish
# RUN dotnet publish "TestApplication.csproj" -c Release -o /app



# FROM node as nodebuilder
# RUN mkdir /usr/src/app
# WORKDIR /usr/src/app
# ENV PATH /usr/src/app/node_modules/.bin:$PATH
# COPY TestApplication/ClientApp/package.json /usr/src/app/package.json
# RUN npm install
# COPY TestApplication/ClientApp/. /usr/src/app
# RUN npm run build

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app .
# RUN mkdir -p /app/ClientApp/dist
# COPY --from=nodebuilder /usr/src/app/dist/. /app/ClientApp/dist/
# ENTRYPOINT ["dotnet", "TestApplication.dll"]


FROM mcr.microsoft.com/dotnet/core/aspnet:2.1.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1.1-buster AS build
WORKDIR /src
COPY ["TestApplication/TestApplication.csproj", "TestApplication/"]
RUN dotnet restore "TestApplication/TestApplication.csproj"
COPY . .
WORKDIR "/src/TestApplication"
RUN dotnet build "TestApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestApplication.dll"]