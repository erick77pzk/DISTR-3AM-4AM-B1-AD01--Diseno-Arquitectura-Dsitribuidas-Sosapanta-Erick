
using Inventario.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Entity Framework Core
builder.Services.AddDbContext<InventarioDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("InventarioConnection")
    
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