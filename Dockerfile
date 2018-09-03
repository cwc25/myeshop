# Build Stage
FROM microsoft/aspnetcore-build:2.0.5-2.1.4 AS build-env
WORKDIR /src

#   Copy only .csproj and restore
COPY ./EventBus/*.csproj ./EventBus/
RUN dotnet restore ./EventBus/

#   Copy only .csproj and restore
COPY ./EventBusRabbitMQ/*.csproj ./EventBusRabbitMQ/
RUN dotnet restore ./EventBusRabbitMQ/

#   Copy only .csproj and restore111
COPY ./Order.API/*.csproj ./Order.API/
RUN dotnet restore ./Order.API/

#   Build EventBus Project
COPY ./EventBus/ ./EventBus/
RUN dotnet build ./EventBus/

#   Build EventBusRabbitMQ Project
COPY ./EventBusRabbitMQ/ ./EventBusRabbitMQ/
RUN dotnet build ./EventBusRabbitMQ/

#   Build Order API project
COPY ./Order.API/ ./Order.API/
RUN dotnet build ./Order.API/

#   publish
RUN dotnet publish ./Order.API/ -o /publish --configuration Release

# Publish Stage
FROM microsoft/aspnetcore:2.0.5
WORKDIR /app
COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "Order.API.dll"]
