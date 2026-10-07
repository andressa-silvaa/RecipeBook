using Bogus;
using RecipeBook.Communication;

namespace CommonTestUtilities.Requests;

public class RequestRegisterUserBuilder
{
    public static RequestRegisterUser Build()
    {
        return new Faker<RequestRegisterUser>()
            .RuleFor(request => request.Name, f => f.Person.FirstName)
            .RuleFor(request => request.Email, (f, user) => f.Internet.Email(user.Name))
            .RuleFor(request => request.Password, f => f.Internet.Password());
    }
}
