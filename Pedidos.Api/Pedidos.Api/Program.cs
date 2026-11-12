
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Pedidos.Api.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            ReferenceHandler.IgnoreCycles;
    });


builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


builder.Services.AddDbContext<PedidosDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("PedidosConnection")
    ));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS
app.UseCors("PermitirTodo");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
