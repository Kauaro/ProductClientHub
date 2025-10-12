# Este Dockerfile assume que ele est� na RAIZ do seu reposit�rio,
# ao lado do arquivo SLAProjectHub.sln.

# Est�gio Base (Execu��o)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Est�gio de Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
# Definimos /src como o diret�rio de trabalho
WORKDIR /src

# 1. COPIA SOMENTE OS ARQUIVOS NECESS�RIOS PARA O RESTORE
# Copia o arquivo de solu��o e os arquivos .csproj de cada projeto
COPY ["SLAProjectHub.sln", "."]
COPY ["ProductClientHub.API/SLAProjectHub.API.csproj", "ProductClientHub.API/"]
# CORRE��O FINAL APLICADA AQUI: Usando o nome EXATO que o Docker est� procurando.
COPY ["ProductClientHub.Communication/ProductClientHub.Communication.csproj", "ProductClientHub.Communication/"]
COPY ["ProductClientHub.Exceptions/SLAProjectHub.Exceptions.csproj", "ProductClientHub.Exceptions/"]

# 2. RESTAURA��O: Usa o arquivo .sln para resolver todas as depend�ncias
RUN dotnet restore "SLAProjectHub.sln"

# 3. COPIA O RESTANTE DO C�DIGO (incluindo c�digo-fonte)
COPY . .

# 4. BUILD
WORKDIR "/src/ProductClientHub.API"
# Constr�i o projeto principal SLAProjectHub.API
RUN dotnet build "SLAProjectHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Est�gio de Publica��o
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SLAProjectHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Est�gio Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Garante a execu��o do DLL correto
ENTRYPOINT ["dotnet", "SLAProjectHub.API.dll"]
