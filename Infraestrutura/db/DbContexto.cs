using System;
using Microsoft.EntityFrameworkCore;
using minimal_api.Dominio.Entidades;

namespace minimal_api.Infraestrutura.db;

public class DbContexto : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrador>().HasData(
            new Administrador
            {
                Id = 1,
                Email = "Administrador@teste.com",
                Senha = "123456",
                Perfil = "Adm"
            }
        );
    }
    // private readonly IConfiguration _configuracaoAppSettings;
    // public DbContexto(IConfiguration configuracaoAppSettings)
    // {
    //     _configuracaoAppSettings = _configuracaoAppSettings;
    // }
    public DbContexto(DbContextOptions<DbContexto> options) : base(options) { }

    public DbSet<Administrador> Administradores { get; set; } = default!;
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     if (!optionsBuilder.IsConfigured)
    //     {


    //         var stringConexao = _configuracaoAppSettings.GetConnectionString("mysql")?.ToString();
    //         if (!string.IsNullOrEmpty(stringConexao))
    //         {
    //             optionsBuilder.UseMySql(
    //                 stringConexao, ServerVersion.AutoDetect(stringConexao)
    //             );
    //         }
    //     }
    // }
}
