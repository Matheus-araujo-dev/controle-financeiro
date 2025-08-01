# Controle Financeiro

Controle Financeiro é uma Web API ASP.NET Core para gerenciar finanças pessoais e empresariais. A API permite registrar receitas, despesas e categorias para que você acompanhe seu fluxo de caixa de forma organizada.

Este repositório contém uma solução ASP.NET Core com múltiplos projetos:

- **ControleFinanceiro.Api** – ponto de entrada da API com controllers e configuração.
- **ControleFinanceiro.Application** – serviços e casos de uso.
- **ControleFinanceiro.Domain** – entidades de domínio e interfaces de repositório.
- **ControleFinanceiro.Infrastructure** – implementações de acesso a dados.
- **ControleFinanceiro.Shared** – utilitários e constantes compartilhadas.

Os projetos incluem classes de exemplo e uma estrutura inicial para você expandir.

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
- Ferramentas de linha de comando do EF Core (execute `dotnet tool restore` para instalar o `dotnet-ef` localmente)

Todos os projetos da solução estão configurados para o *Target Framework* `net8.0`, garantindo compatibilidade total com o .NET 8.

## Configuração do banco e migrações

1. Atualize a string de conexão em `appsettings.json` no projeto da API para apontar para sua instância do SQL Server.
2. Crie a migração inicial:
   ```bash
   dotnet ef migrations add InitialCreate -s ControleFinanceiro.Api/ControleFinanceiro.Api.csproj -p ControleFinanceiro.Infrastructure/ControleFinanceiro.Infrastructure.csproj
   ```
3. Aplique a migração para criar o esquema do banco de dados:
   ```bash
   dotnet ef database update -s ControleFinanceiro.Api/ControleFinanceiro.Api.csproj -p ControleFinanceiro.Infrastructure/ControleFinanceiro.Infrastructure.csproj
   ```

## Build e execução da API

Restaure as dependências, compile a solução e execute o projeto da API:

```bash
cd ControleFinanceiro.Api
dotnet restore
dotnet build
dotnet run
```

A API escutará em `https://localhost:5001` por padrão. Para consultar e testar os endpoints disponíveis, abra `https://localhost:5001/swagger` no navegador, onde a documentação interativa do Swagger estará disponível.

## Relatórios financeiros

Para obter visões resumidas de receitas e despesas, a API disponibiliza o controlador `Relatorios`.
Use `/api/relatorios/movimentacoes` para retornar o total de entradas e saídas em um período
e `/api/relatorios/contas` para consultar o montante de contas a pagar e a receber.
Também é possível informar o parâmetro `tipo` em `/api/relatorios/movimentacoes/tipo` para filtrar por tipo de movimentação.

## Contribuindo

Sinta-se à vontade para fazer fork deste repositório, abrir issues e enviar pull requests. Relatos de bugs e ideias de funcionalidades são bem-vindos!
