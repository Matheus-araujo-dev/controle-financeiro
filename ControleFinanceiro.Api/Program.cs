using ControleFinanceiro.Infrastructure.Data;
using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ControleFinanceiro.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<FinanceiroDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
            builder.Services.AddScoped<ICartaoRepository, CartaoRepository>();
            builder.Services.AddScoped<IContaPagarRepository, ContaPagarRepository>();
            builder.Services.AddScoped<IContaReceberRepository, ContaReceberRepository>();
            builder.Services.AddScoped<IMovimentacaoFinanceiraRepository, MovimentacaoFinanceiraRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IFormaPagamentoRepository, FormaPagamentoRepository>();
            builder.Services.AddScoped<IContaBancariaRepository, ContaBancariaRepository>();

            builder.Services.AddScoped<IPessoaAppService, PessoaAppService>();
            builder.Services.AddScoped<ICartaoAppService, CartaoAppService>();
            builder.Services.AddScoped<IContaPagarAppService, ContaPagarAppService>();
            builder.Services.AddScoped<IContaReceberAppService, ContaReceberAppService>();
            builder.Services.AddScoped<IMovimentacaoFinanceiraAppService, MovimentacaoFinanceiraAppService>();
            builder.Services.AddScoped<ITransactionAppService, TransactionAppService>();
            builder.Services.AddScoped<IUsuarioAppService, UsuarioAppService>();
            builder.Services.AddScoped<IContaBancariaAppService, ContaBancariaAppService>();

            var jwtSection = builder.Configuration.GetSection("Jwt");
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSection["Issuer"],
                        ValidAudience = jwtSection["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]))
                    };
                });

            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
