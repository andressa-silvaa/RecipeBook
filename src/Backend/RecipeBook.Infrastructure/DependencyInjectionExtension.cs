using Microsoft.Extensions.DependencyInjection;
using RecipeBook.Domain.Security.PasswordHashing;
using RecipeBook.Infrastructure.Security.PasswordHashing;

namespace RecipeBook.Infrastructure;

public static class DependencyInjectionExtension
{
    extension(IServiceCollection services)
    {
        public void AddInfrastructure()
        {
            services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();
        }
    }
}
