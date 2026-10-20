using Moq;
using RecipeBook.Domain.Repositories;

namespace CommonTestUtilities.Repositories;

public class IUnityOfWorkBuilder
{
    public static IUnityOfWork Build()
    {
        var mock = new Mock<IUnityOfWork>();
        return mock.Object;
    }
}
