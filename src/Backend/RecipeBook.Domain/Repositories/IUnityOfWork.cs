namespace RecipeBook.Domain.Repositories;

public interface IUnityOfWork
{
    Task Commit();
}
