using RecipeBook.Communication;

namespace RecipeBook.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    void Execute(RequestRegisterUser request);
}
