using FluentValidation;
using RecipeBook.Communication;
using RecipeBook.Exception;

namespace RecipeBook.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUser>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessagesExceptions.VALIDATION_EMAIL_REQUIRED);
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesExceptions.VALIDATION_NAME_REQUIRED);
        RuleFor(user => user.Password).NotEmpty().WithMessage(ResourceMessagesExceptions.VALIDATION_PASSWORD_REQUIRED);
        When(user => string.IsNullOrWhiteSpace(user.Email) == false, () =>
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesExceptions.VALIDATION_EMAIL_INVALID);
        });
    }
}
