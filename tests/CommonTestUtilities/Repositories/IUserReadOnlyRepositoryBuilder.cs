using Moq;
using RecipeBook.Domain.Repositories.User;

namespace CommonTestUtilities.Repositories;

public class IUserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _userReadOnlyRepositoryMock;

    public IUserReadOnlyRepositoryBuilder()
    {
        _userReadOnlyRepositoryMock = new Mock<IUserReadOnlyRepository>();
    }

    public void ExistActiveUserWithEmail(string email)
    {
        _userReadOnlyRepositoryMock.Setup(repository => repository.ExistActiveUserWithEmail(email)).ReturnsAsync(true);
    }
    
    public IUserReadOnlyRepository Build() => _userReadOnlyRepositoryMock.Object;
}
