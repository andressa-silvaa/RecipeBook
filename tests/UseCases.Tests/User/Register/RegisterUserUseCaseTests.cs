using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Security;
using RecipeBook.Application.UseCases.User.Register;
using RecipeBook.Exception;
using RecipeBook.Exception.ExceptionsBase;
using Shouldly;

namespace UseCases.Tests.User.Register;

public class RegisterUserUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserBuilder.Build();

        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.ShouldNotBeNull();
        result.Tokens.ShouldNotBeNull();
        result.Name.ShouldBe(request.Name);
        result.Tokens.AccessToken.ShouldBeNullOrEmpty();
        result.Tokens.RefreshToken.ShouldBeNullOrEmpty();

    }

    [Fact]
    public async Task Validate_ShouldThrowException_WhenNameIsEmpty()
    {
        var request = RequestRegisterUserBuilder.Build();
        request.Name = string.Empty;
        var useCase = CreateUseCase();


        var exception = await useCase.Execute(request).ShouldThrowAsync<ValidationException>();
        exception.GetErrorMessages().ShouldSatisfyAllConditions(errorMessages =>
        {
            errorMessages.Count.ShouldBe(1);
            errorMessages.ShouldContain(ResourceMessagesExceptions.VALIDATION_NAME_REQUIRED);
        });

    }

    [Fact]
    public async Task Validate_ShouldThrowException_WhenEmailAlreadyExists()
    {
        var request = RequestRegisterUserBuilder.Build();
        var useCase = CreateUseCase(request.Email);


        var exception = await useCase.Execute(request).ShouldThrowAsync<ValidationException>();
        exception.GetErrorMessages().ShouldSatisfyAllConditions(errorMessages =>
        {
            errorMessages.Count.ShouldBe(1);
            errorMessages.ShouldContain(ResourceMessagesExceptions.VALIDATION_EMAIL_ALREADY_EXISTS);
        });

    }

    private RegisterUserUseCase CreateUseCase(string? emailThatAlreadyExists = null)
    {
        var unityOfWork = IUnityOfWorkBuilder.Build();
        var userWriteOnlyRepository = IUserWriteOnlyRepositoryBuilder.Build();
        var passwordHasher = new IPasswordHasherBuilder().Build();
        var userReadOnlyRepositoryBuilder = new IUserReadOnlyRepositoryBuilder();

        if (!string.IsNullOrEmpty(emailThatAlreadyExists))
        {
            userReadOnlyRepositoryBuilder.ExistActiveUserWithEmail(emailThatAlreadyExists);
        }

        return new RegisterUserUseCase(passwordHasher, userWriteOnlyRepository, userReadOnlyRepositoryBuilder.Build(), unityOfWork);
    }
}
