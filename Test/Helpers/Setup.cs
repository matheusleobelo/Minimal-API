using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using minimal_api;
using minimal_api.Interfaces;
using Test.Mocks;

namespace Test.Helpers;

public class Setup
{
    public const string PORT = "5001";
    public static TestContext testContext = default!;
    public static WebApplicationFactory<Startup> http = default!;
    public static HttpClient client = default!;

    public static void ClassInit(TestContext testContext)
    {
        Setup.testContext = testContext;

        Setup.http = new WebApplicationFactory<Startup>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseSetting("https_port", Setup.PORT);
                builder.UseEnvironment("Testing");

                builder.ConfigureServices(services =>
                {
                    services.AddScoped<iVeiculoServicos, VeiculoServicoMock>();
                    services.AddScoped<iAdministradorServicos, AdministradorServicoMock>();
                });
            });

        client = Setup.http.CreateClient();
    }

    public static void ClassCleanup()
    {
        client?.Dispose();
        http?.Dispose();
    }
}
