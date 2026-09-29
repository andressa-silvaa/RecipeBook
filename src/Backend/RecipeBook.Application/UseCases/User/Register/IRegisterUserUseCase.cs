using RecipeBook.Communication;
using RecipeBook.Communication.Responses;

namespace RecipeBook.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestRegisterUser request);
}
