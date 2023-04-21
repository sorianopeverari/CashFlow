using CashFlow.Domain.Repositories;
using CashFlow.Infra.Repositories.PgDW;
using CashFlow.Infra.Repositories.PgRDS;
using CashFlow.Domain.Business;
using Microsoft.FeatureManagement;
using Microsoft.OpenApi.Models;
using CashFlow.Api.Filters;
using CashFlow.Domain.Business.Providers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddFeatureManagement();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo 
{ 
    Title = "CashFlow", 
    Version = "v1.0.0" 
}));
builder.Services.AddTransient<ITransactionBusiness, TransactionBusiness>();
builder.Services.AddTransient<IBalanceBusiness, BalanceBusiness>();
builder.Services.AddTransient<ITransactionRepository, TransactionPgRDSRepository>();
builder.Services.AddTransient<IBalanceRepository, BalancePgDWRepository>();
builder.Services.AddTransient<IDateTimeProvider, DateTimeProvider>();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CashFlow v1.0.0");
});
app.MapControllers();

app.Run();
