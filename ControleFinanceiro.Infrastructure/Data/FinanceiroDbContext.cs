using Microsoft.EntityFrameworkCore;
using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Infrastructure.Data
{
    public class FinanceiroDbContext : DbContext
    {
        public FinanceiroDbContext(DbContextOptions<FinanceiroDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Cartao> Cartoes { get; set; }
        public DbSet<ContaPagar> ContasPagar { get; set; }
        public DbSet<ContaReceber> ContasReceber { get; set; }
        public DbSet<MovimentacaoFinanceira> MovimentacoesFinanceiras { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cartao>()
                .HasOne(c => c.Pessoa)
                .WithMany(p => p.Cartoes)
                .HasForeignKey(c => c.PessoaId);

            modelBuilder.Entity<ContaPagar>()
                .HasOne(c => c.Pessoa)
                .WithMany(p => p.ContasPagar)
                .HasForeignKey(c => c.PessoaId);

            modelBuilder.Entity<ContaReceber>()
                .HasOne(c => c.Pessoa)
                .WithMany(p => p.ContasReceber)
                .HasForeignKey(c => c.PessoaId);

            modelBuilder.Entity<MovimentacaoFinanceira>()
                .HasOne(m => m.Pessoa)
                .WithMany(p => p.MovimentacoesFinanceiras)
                .HasForeignKey(m => m.PessoaId);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
