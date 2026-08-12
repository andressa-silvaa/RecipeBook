using Mapster;
using RecipeBook.Communication;
using RecipeBook.Exception.ExceptionsBase;

namespace RecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase
{
    public void Execute(RequestRegisterUser request) 
    {
        ValidateAndThrowOnFailures(request);
        var user = request.Adapt<Domain.Entities.User>();
    }
    private void ValidateAndThrowOnFailures(RequestRegisterUser request) 
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ValidationException(errors);
        }
    }
}
