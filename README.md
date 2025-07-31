# Controle Financeiro

Controle Financeiro é uma Web API ASP.NET Core para gerenciar finanças pessoais e empresariais. A API permite registrar receitas, despesas e categorias para que você acompanhe seu fluxo de caixa de forma organizada.

## Arquitetura

A solução está organizada em quatro camadas principais:

1. **Domain** – Modelos de negócio e interfaces centrais.
2. **Application** – Serviços e casos de uso que orquestram a lógica de domínio.
3. **Infrastructure** – Persistência de dados usando Entity Framework Core e SQL Server.
4. **API** – Camada ASP.NET Core que expõe endpoints HTTP.

Essa separação mantém as regras de negócio isoladas das preocupações de infraestrutura e torna a base de código mais fácil de manter.

## Pré-requisitos

- **.NET SDK 8.0 ou superior** – [Baixar aqui](https://dotnet.microsoft.com/download)
- Instância do **SQL Server** (LocalDB ou SQL Server completo)
- Ferramentas de linha de comando do EF Core (`dotnet tool install --global dotnet-ef`)

## Configuração do banco e migrações

1. Atualize a string de conexão em `appsettings.json` no projeto da API para apontar para sua instância do SQL Server.
2. Crie a migração inicial:
   ```bash
   dotnet ef migrations add InitialCreate -s src/ControleFinanceiro.Api/ControleFinanceiro.Api.csproj -p src/ControleFinanceiro.Infrastructure/ControleFinanceiro.Infrastructure.csproj
   ```
3. Aplique a migração para criar o esquema do banco de dados:
   ```bash
   dotnet ef database update -s src/ControleFinanceiro.Api/ControleFinanceiro.Api.csproj -p src/ControleFinanceiro.Infrastructure/ControleFinanceiro.Infrastructure.csproj
   ```

## Build e execução da API

Restaure as dependências e execute o projeto a partir da camada de API:

```bash
cd src/ControleFinanceiro.Api
dotnet restore
dotnet run
```

A API escutará em `https://localhost:5001` por padrão. Você pode explorar os endpoints disponíveis no Swagger UI em `https://localhost:5001/swagger`.

## Contribuindo

Sinta-se à vontade para fazer fork deste repositório, abrir issues e enviar pull requests. Relatos de bugs e ideias de funcionalidades são bem-vindos!

