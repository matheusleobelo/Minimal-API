using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.ModelView;
using minimal_api.Dominio.Servicos;
using minimal_api.Infraestrutura.db;
using minimal_api.Interfaces;

#region builder
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<iAdministradorServicos, AdministradorServicos>();
builder.Services.AddScoped<iVeiculoServicos, VeiculoServicos>();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Minimal API - Veículos",
        Version = "v1",
        Description = "API de veículos com autenticação e EF Core"
    });
});

builder.Services.AddDbContext<DbContexto>(options => {
    options.UseMySql(
    builder.Configuration.GetConnectionString("mysql"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    );
    });

var app = builder.Build();
#endregion

#region Home
app.MapGet("/", () => Results.Json(new Home())).WithTags("Home");
#endregion 

#region Administrador
app.MapPost("/Administrador/login", ([FromBody] LoginDTO loginDTO, iAdministradorServicos administradorServicos) =>
{
    if (administradorServicos.Login(loginDTO) != null)
        return Results.Ok("Login realizado com sucesso");
    else
        return Results.Unauthorized();
}).WithTags("Administrador");

app.MapPost("/Administrador", ([FromBody] AdministradorDTO administradorDTO, iAdministradorServicos administradorServicos) => 
{
    var validacao = new ErrosDeValidacao
    {
        Mensagens = new List<string>()
    };

    if (string.IsNullOrEmpty(administradorDTO.Email))
        validacao.Mensagens.Add("Email não pode ser vazio");

    if (string.IsNullOrEmpty(administradorDTO.Senha))
        validacao.Mensagens.Add("Senha não pode ser vazio");

    if (string.IsNullOrEmpty(administradorDTO.Perfil.ToString()))
        validacao.Mensagens.Add("Perfil não pode ser vazio");

    if (validacao.Mensagens.Count > 0)
        return Results.BadRequest(validacao);

    var administrador = new Administrador
    {
        Email = administradorDTO.Email,
        Senha = administradorDTO.Senha,
        Perfil = administradorDTO.Perfil.ToString()
    };
    administradorServicos.Incluir(administrador);
        
    return Results.Created($"/Administrador/{administrador.Id}", administrador);
}).WithTags("Administrador");

#endregion

#region Veiculo

ErrosDeValidacao validaDTO(VeiculoDTO veiculoDTO)
{
    var validacao = new ErrosDeValidacao
    {
        Mensagens = new List<string>()
    };

    if (string.IsNullOrEmpty(veiculoDTO.Nome) || veiculoDTO.Nome == "string")
        validacao.Mensagens.Add("O nome não pode ser vazio");

    if (string.IsNullOrEmpty(veiculoDTO.Marca) || veiculoDTO.Marca == "string")
        validacao.Mensagens.Add("A marca não pode ficar em branco");

    if (veiculoDTO.Ano < 1950)
        validacao.Mensagens.Add("O ano do veículo deve ser maior ou igual a 1950");

    return validacao;
}

app.MapPost("/veiculos", ([FromBody]VeiculoDTO veiculoDTO,iVeiculoServicos  veiculoServicos) =>
{
    var validacao = validaDTO(veiculoDTO);
    if (validacao.Mensagens.Count > 0)
        return Results.BadRequest(validacao);

    var veiculo = new Veiculo
    {
        Nome = veiculoDTO.Nome,
        Marca = veiculoDTO.Marca,
        Ano = veiculoDTO.Ano
    };
    veiculoServicos.Incluir(veiculo);

    return Results.Created($"/veiculo/{veiculo.Id}", veiculo);
}).WithTags("Veiculos");

app.MapGet("/veiculos", ([FromQuery] int? pagina, iVeiculoServicos veiculoServicos) =>
{
    var veiculo = veiculoServicos.Todos();
    return Results.Ok(veiculo);
}).WithTags("Veiculos");

app.MapGet("/veiculos/{id}", ([FromRoute] int id, iVeiculoServicos veiculoServicos) =>
{
    var veiculo = veiculoServicos.BuscarPorId(id);
    if (veiculo == null) return Results.NotFound();
    return Results.Ok(veiculo);
}).WithTags("Veiculos");

app.MapPut("/veiculos/{id}", ([FromRoute] int id,VeiculoDTO veiculoDTO , iVeiculoServicos veiculoServicos) =>
{
    var validacao = validaDTO(veiculoDTO); 
    if (validacao.Mensagens.Count > 0)
        return Results.BadRequest(validacao);

    var veiculo = veiculoServicos.BuscarPorId(id);
    if (veiculo == null) return Results.NotFound();

    veiculo.Nome = veiculoDTO.Nome;
    veiculo.Marca = veiculoDTO.Marca;
    veiculo.Ano = veiculoDTO.Ano;

    veiculoServicos.Atualizar(veiculo);

    return Results.Ok(veiculo);
}).WithTags("Veiculos");

app.MapDelete("/veiculos/{id}", ([FromRoute] int id, iVeiculoServicos veiculoServicos) =>
{
    var veiculo = veiculoServicos.BuscarPorId(id);
    if (veiculo == null) return Results.NotFound();

    veiculoServicos.Apagar(veiculo);

    return Results.NoContent();
}).WithTags("Veiculos");
#endregion

#region App
app.UseSwagger();
app.UseSwaggerUI();
app.Run();
#endregion