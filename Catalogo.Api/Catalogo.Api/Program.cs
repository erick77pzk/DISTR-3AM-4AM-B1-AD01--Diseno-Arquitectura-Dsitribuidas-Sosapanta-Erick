
using Catalogo.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// CORS 
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
builder.Services.AddDbContext<CatalogoDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("CategoriaConnection")
    
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


app.UseCors("PermitirTodo");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();