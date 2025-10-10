# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos de projeto e restaura dependências
COPY ProductClientHub.API/SLAProjectHub.API.csproj ProductClientHub.API/
COPY ProductClientHub.Communication/SLAProjectHub.Communication.csproj ProductClientHub.Communication/
COPY ProductClientHub.Exceptions/SLAProjectHub.Exceptions.csproj ProductClientHub.Exceptions/
COPY SLAProjectHub.sln ./

RUN dotnet restore SLAProjectHub.sln

# Copia o restante do código
COPY . .

# Publica o projeto (gera o build final otimizado)
RUN dotnet publish ProductClientHub.API/SLAProjectHub.API.csproj -c Release -o /app/publish

# Etapa 2: Runtime (executa a API)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
EXPOSE 8081
VOLUME ["/data"]
ENTRYPOINT ["dotnet", "SLAProjectHub.API.dll"]
