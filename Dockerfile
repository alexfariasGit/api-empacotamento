# Use a imagem base do SDK do .NET 8.0 para compilar o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copie o arquivo de projeto e restaure as dependências
COPY ./EmbalagemApi/EmbalagemApi.csproj ./ 
RUN dotnet restore ./EmbalagemApi.csproj

# Copie o restante dos arquivos do projeto
COPY ./EmbalagemApi/ ./

# Compile e publique o projeto
RUN dotnet publish -c Release -o out

# Use a imagem base do runtime do .NET para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expor a porta padrão para a API
EXPOSE 80

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "EmbalagemApi.dll"]

