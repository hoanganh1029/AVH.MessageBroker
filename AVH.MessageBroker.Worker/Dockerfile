FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["AVH.MessageBroker.Worker/AVH.MessageBroker.Worker.csproj", "AVH.MessageBroker.Worker/"]
COPY ["AVH.MessageBroker.AzureMessaging/AVH.MessageBroker.AzureMessaging.csproj", "AVH.MessageBroker.AzureMessaging/"]
COPY ["AVH.MessageBroker.Common/AVH.MessageBroker.Common.csproj", "AVH.MessageBroker.Common/"]
COPY ["AVH.MessageBroker.Framework/AVH.MessageBroker.Framework.csproj", "AVH.MessageBroker.Framework/"]
COPY ["AVH.MessageBroker.Models/AVH.MessageBroker.Models.csproj", "AVH.MessageBroker.Models/"]
COPY ["AVH.MessageBroker.Services/AVH.MessageBroker.Services.csproj", "AVH.MessageBroker.Services/"]

RUN dotnet restore "AVH.MessageBroker.Worker/AVH.MessageBroker.Worker.csproj"
COPY . .
WORKDIR "/src/AVH.MessageBroker.Worker"
RUN dotnet build "AVH.MessageBroker.Worker.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "AVH.MessageBroker.Worker.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
ENV DOTNET_ENVIRONMENT="Development"
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AVH.MessageBroker.Worker.dll"]
