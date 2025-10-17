using Desafio_POO.Data;
using Desafio_POO.Endpoints;
using Desafio_POO.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// DI

// SQLITE
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

// Services
builder.Services.AddScoped<ImoveisService>();
builder.Services.AddScoped<ProprietariosService>();
builder.Services.AddScoped<InquilinosService>();


var app = builder.Build();

app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "v1");
});

app.UseHttpsRedirection();


// Mapea endpoints
app.MapGet("/", () => Results.Redirect("/swagger"))
    .ExcludeFromDescription(); // remove do docs no swagger

app.MapImoveisEndpoints();
app.MapProprietariosEndpoints();
app.MapInquilinosEndpoints();

app.Run();
