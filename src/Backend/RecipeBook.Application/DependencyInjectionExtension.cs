using Microsoft.Extensions.DependencyInjection;
using RecipeBook.Application.UseCases.User.Register;

namespace RecipeBook.Application;

public static class DependencyInjectionExtension
{
    extension(IServiceCollection services)
    {
        public void AddApplication()
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        }
    }
}
