using Mapster;
using RecipeBook.Communication;
using RecipeBook.Domain.Security.PasswordHashing;
using RecipeBook.Exception.ExceptionsBase;

namespace RecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IPasswordHasher _passwordHasher;
    public RegisterUserUseCase(IPasswordHasher passwordHasher) 
    { 
        _passwordHasher = passwordHasher;
    }

    public void Execute(RequestRegisterUser request) 
    {
        ValidateAndThrowOnFailures(request);
        var user = request.Adapt<Domain.Entities.User>();
        user.Password = _passwordHasher.HashPassword(request.Password);
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
