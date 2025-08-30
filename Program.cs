using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Servicos;
using minimal_api.Infraestrutura.db;
using minimal_api.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<iAdministradorServicos, AdministradorServicos>();

builder.Services.AddDbContext<DbContexto>(options => {
    options.UseMySql(
    builder.Configuration.GetConnectionString("mysql"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    );
    });

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/login", ([FromBody]LoginDTO loginDTO,iAdministradorServicos  administradorServicos) =>
{
    if (administradorServicos.Login(loginDTO) != null)
        return Results.Ok("Login realizado com sucesso");
    else
        return Results.Unauthorized();
});

app.Run();

