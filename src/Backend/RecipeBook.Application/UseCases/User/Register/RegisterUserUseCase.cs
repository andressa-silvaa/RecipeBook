using RecipeBook.Communication;

namespace RecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase
{
    public void Execute(RequestRegisterUser request) 
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

    }
}
