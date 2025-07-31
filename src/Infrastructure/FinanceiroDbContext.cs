using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Infrastructure
{
    public class FinanceiroDbContext : DbContext
    {
        public FinanceiroDbContext(DbContextOptions<FinanceiroDbContext> options)
            : base(options)
        {
        }

        // Add DbSet properties here as you create entities
    }
}
