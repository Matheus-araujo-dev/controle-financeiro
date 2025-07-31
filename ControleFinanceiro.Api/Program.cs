using ControleFinanceiro.Infrastructure.Data;
using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<FinanceiroDbContext>(options =>
                options.UseInMemoryDatabase("FinanceiroDb"));

            builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
            builder.Services.AddScoped<ICartaoRepository, CartaoRepository>();
            builder.Services.AddScoped<IContaPagarRepository, ContaPagarRepository>();
            builder.Services.AddScoped<IContaReceberRepository, ContaReceberRepository>();
            builder.Services.AddScoped<IMovimentacaoFinanceiraRepository, MovimentacaoFinanceiraRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

            builder.Services.AddScoped<IPessoaAppService, PessoaAppService>();
            builder.Services.AddScoped<ICartaoAppService, CartaoAppService>();
            builder.Services.AddScoped<IContaPagarAppService, ContaPagarAppService>();
            builder.Services.AddScoped<IContaReceberAppService, ContaReceberAppService>();
            builder.Services.AddScoped<IMovimentacaoFinanceiraAppService, MovimentacaoFinanceiraAppService>();
            builder.Services.AddScoped<ITransactionAppService, TransactionAppService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            app.Run();
        }
    }
}
