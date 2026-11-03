using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Testcontainers.MySql;

namespace WebApi.Tests;

public class RecipeBookApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MySqlContainer _container;
    public RecipeBookApplicationFactory()
    {
        _container = new MySqlBuilder("mysql:8.0")
            .WithDatabase("livro_receitas")
            .Build();
    }

    public async Task InitializeAsync() => await _container.StartAsync();

    Task IAsyncLifetime.DisposeAsync() => _container.StopAsync();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Tests")
            .ConfigureAppConfiguration((_, configuration) =>
            {
                var parameters = new Dictionary<string, string?>
                {
                    ["ConnectionStrings:DbConnection"] = _container.GetConnectionString()
                };
                configuration.AddInMemoryCollection(parameters);
            });
    }
}
