using CommonTestUtilities.Requests;
using RecipeBook.Application.UseCases.User.Register;
using RecipeBook.Exception;
using Shouldly;

namespace Validators.Tests.User.Register;

public class RegisterUserValidatorTests
{
    [Fact]
    public void Success()
    {
        //AAA

        //Arrange
        var request = RequestRegisterUserBuilder.Build();

        var validator = new RegisterUserValidator();

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenNameIsEmpty()
    {
        //AAA

        //Arrange
        var request = RequestRegisterUserBuilder.Build();
        request.Name = string.Empty;

        var validator = new RegisterUserValidator();

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(erros => {
            erros.Count.ShouldBe(1);
            erros.ShouldContain(erros => erros.ErrorMessage.Equals(ResourceMessagesExceptions.VALIDATION_NAME_REQUIRED));
        }); 
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenEmailIsEmpty()
    {
        //AAA

        //Arrange
        var request = RequestRegisterUserBuilder.Build();
        request.Email = string.Empty;

        var validator = new RegisterUserValidator();

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(erros => {
            erros.Count.ShouldBe(1);
            erros.ShouldContain(erros => erros.ErrorMessage.Equals(ResourceMessagesExceptions.VALIDATION_EMAIL_REQUIRED));
        });
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenEmailIsInvalid()
    {
        //AAA

        //Arrange
        var request = RequestRegisterUserBuilder.Build();
        request.Email = "invalid email";

        var validator = new RegisterUserValidator();

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(erros => {
            erros.Count.ShouldBe(1);
            erros.ShouldContain(erros => erros.ErrorMessage.Equals(ResourceMessagesExceptions.VALIDATION_EMAIL_INVALID));
        });
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenPasswordIsEmpty()
    {
        //AAA

        //Arrange
        var request = RequestRegisterUserBuilder.Build();
        request.Password = string.Empty;

        var validator = new RegisterUserValidator();

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(erros => {
            erros.Count.ShouldBe(1);
            erros.ShouldContain(erros => erros.ErrorMessage.Equals(ResourceMessagesExceptions.VALIDATION_PASSWORD_REQUIRED));
        });
    }
}
