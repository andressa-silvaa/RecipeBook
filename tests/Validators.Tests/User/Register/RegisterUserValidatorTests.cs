using CommonTestUtilities.Requests;
using RecipeBook.Application.UseCases.User.Register;
using RecipeBook.Communication;
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
}
