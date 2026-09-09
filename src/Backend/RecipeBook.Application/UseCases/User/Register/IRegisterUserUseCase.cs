using RecipeBook.Communication;

namespace RecipeBook.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    Task Execute(RequestRegisterUser request);
}
