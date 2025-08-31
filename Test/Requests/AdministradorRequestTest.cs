using System;
using System.Text.Json;
using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.ModelView;
using Test.Helpers;

namespace Test.Requests;

[TestClass]
public class AdministradorRequestTest
{
    [ClassInitialize]
    public static void ClassInit(TestContext testContext)
    {
        Setup.ClassInit(testContext);
    }

    [ClassCleanup]
    public static void ClassCleanup(TestContext testContext)
    {
        Setup.ClassCleanup();
    }

    [TestMethod]
    public async Task TestandoSalvarAdministrador()
    {
        // Arrange
        var loginDTO = new LoginDTO
        {
            Email = "adm@teste.com",
            Senha = "123456"
        };

        var content = new StringContent(JsonSerializer.Serialize(loginDTO), System.Text.Encoding.UTF8, "application/json");

        //Act
        var response = await Setup.client.PostAsync("/Administrador/login", content);

        // Assert
        Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        
        var result = await response.Content.ReadAsStringAsync();

        var admLogado = JsonSerializer.Deserialize<AdministradorLogado>(
            result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );

        // Asserts
        Assert.IsNotNull(admLogado?.Perfil ?? "");
        Assert.IsNotNull(admLogado?.Email ?? "");
        Assert.IsNotNull(admLogado?.Token ?? "");

    }
}
