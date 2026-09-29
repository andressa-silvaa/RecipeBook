using Mapster;
using RecipeBook.Communication;
using RecipeBook.Communication.Responses;
using RecipeBook.Domain.Repositories;
using RecipeBook.Domain.Repositories.User;
using RecipeBook.Domain.Security.PasswordHashing;
using RecipeBook.Exception.ExceptionsBase;

namespace RecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUnityOfWork _unityOfWork;

    public RegisterUserUseCase(IPasswordHasher passwordHasher, IUserWriteOnlyRepository userWriteOnlyRepository, IUnityOfWork unityOfWork) 
    { 
        _passwordHasher = passwordHasher;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _unityOfWork = unityOfWork; 
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUser request) 
    {
        ValidateAndThrowOnFailures(request);
        var user = request.Adapt<Domain.Entities.User>();
        user.Password = _passwordHasher.HashPassword(request.Password); 

        await _userWriteOnlyRepository.Add(user);
        await _unityOfWork.Commit();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name
        };
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
