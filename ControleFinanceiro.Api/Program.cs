using ControleFinanceiro.Infrastructure.Data;
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

            var app = builder.Build();

            app.Run();
        }
    }
}
