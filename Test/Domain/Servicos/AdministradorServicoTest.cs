using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Servicos;
using minimal_api.Infraestrutura.db;

namespace Test.Domain.Servicos
{
    [TestClass]
    public class AdministradorServicoTest
    {
      private DbContexto CriarContextoDeTeste()
{
    var options = new DbContextOptionsBuilder<DbContexto>()
        .UseInMemoryDatabase(databaseName: "DbMinimalApiTest")
        .Options;

    return new DbContexto(options);
}

[TestMethod]
public void TestandoSalvarAdministrador()
{
    // Arrange
    var adm = new Administrador
    {
        Id = 1,
        Email = "bruno@gmail.com",
        Senha = "123456",
        Perfil = "Editor"
    };

    var context = CriarContextoDeTeste();

    // Limpa dados antes do teste
    context.Administradores.RemoveRange(context.Administradores);
    context.SaveChanges();

    var administradorServico = new AdministradorServicos(context);

    // Act
    administradorServico.Incluir(adm);
    administradorServico.BuscarPorId(adm.Id);

    // Assert
    Assert.AreEqual(1, adm.Id);
    Assert.AreEqual(1, administradorServico.Todos(1).Count());
}

    }
}
