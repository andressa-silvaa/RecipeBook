using RecipeBook.Domain.Repositories;

namespace RecipeBook.Infrastructure.DataAccess;

internal class UnityOfWork: IUnityOfWork
{
    private readonly RecipeBookDbContext _dbContext;

    public UnityOfWork(RecipeBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
