using CooperativeProcurement.Core.Interfaces;
using CooperativeProcurement.Core.Services;
using CooperativeProcurement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// MVC и Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Регистрация зависимостей (DI)
builder.Services.AddSingleton<IPurchaseOrderRepository, JsonPurchaseOrderRepository>();
builder.Services.AddScoped<PurchaseOrderService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();