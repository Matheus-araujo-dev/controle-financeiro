using ControleFinanceiro.Application.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<IExampleService, ExampleService>();

var app = builder.Build();

app.MapControllers();

app.Run();
