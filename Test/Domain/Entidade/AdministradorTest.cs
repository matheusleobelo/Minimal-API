using System;
using minimal_api.Dominio.Entidades;

namespace Test.Domain.Entidade;
[TestClass]
public class AdministradorTest
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        //Arrange
        var adm = new Administrador();

        //Act
        adm.Id = 1;
        adm.Email = "bruno@gmail.com";
        adm.Senha = "123456";
        adm.Perfil = "Editor";

        //Assert
        Assert.AreEqual(1, adm.Id);
        Assert.AreEqual("bruno@gmail.com", adm.Email);
        Assert.AreEqual("123456", adm.Senha);
        Assert.AreEqual("Editor", adm.Perfil);
    }
}
