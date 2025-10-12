# Este Dockerfile assume que ele está na RAIZ do seu repositório,
# ao lado do arquivo SLAProjectHub.sln.

# Estágio Base (Execução)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Estágio de Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
# Definimos /src como o diretório de trabalho
WORKDIR /src

# 1. COPIA SOMENTE OS ARQUIVOS NECESSÁRIOS PARA O RESTORE
# Copia o arquivo de solução e os arquivos .csproj de cada projeto
COPY ["SLAProjectHub.sln", "."]
COPY ["ProductClientHub.API/SLAProjectHub.API.csproj", "ProductClientHub.API/"]
# CORREÇÃO FINAL APLICADA AQUI: Usando o nome EXATO que o Docker está procurando.
COPY ["ProductClientHub.Communication/ProductClientHub.Communication.csproj", "ProductClientHub.Communication/"]
COPY ["ProductClientHub.Exceptions/SLAProjectHub.Exceptions.csproj", "ProductClientHub.Exceptions/"]

# 2. RESTAURAÇÃO: Usa o arquivo .sln para resolver todas as dependências
RUN dotnet restore "SLAProjectHub.sln"

# 3. COPIA O RESTANTE DO CÓDIGO (incluindo código-fonte)
COPY . .

# 4. BUILD
WORKDIR "/src/ProductClientHub.API"
# Constrói o projeto principal SLAProjectHub.API
RUN dotnet build "SLAProjectHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Estágio de Publicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SLAProjectHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Estágio Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Garante a execução do DLL correto
ENTRYPOINT ["dotnet", "SLAProjectHub.API.dll"]
