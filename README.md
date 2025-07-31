# Controle Financeiro

This repository contains a basic setup for a finance control API using ASP.NET Core and Entity Framework Core.

## Database

1. Update the connection string in `src/ControleFinanceiro.Api/appsettings.json` if necessary.
2. Create the initial migration and update the database:

```bash
dotnet ef migrations add InitialCreate -p src/Infrastructure -s src/ControleFinanceiro.Api

dotnet ef database update -p src/Infrastructure -s src/ControleFinanceiro.Api
```

These commands will generate the migration files inside `src/Infrastructure` and apply them to the configured database.
