using Microsoft.Extensions.DependencyInjection;
using RecipeBook.Domain.Security.PasswordHashing;
using RecipeBook.Infrastructure.Security.PasswordHashing;

namespace RecipeBook.Infrastructure;

public class DependencyInjectionExtension
{
    public static void AddInfrastructure(IServiceCollection services) 
    {
        services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();
    }
}
