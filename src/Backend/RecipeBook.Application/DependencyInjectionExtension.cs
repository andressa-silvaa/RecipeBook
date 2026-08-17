using Microsoft.Extensions.DependencyInjection;
using RecipeBook.Application.UseCases.User.Register;

namespace RecipeBook.Application;

public class DependencyInjectionExtension
{
    public static void AddApplication(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }
}
