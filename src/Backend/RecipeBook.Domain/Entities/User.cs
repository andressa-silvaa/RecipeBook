namespace RecipeBook.Domain.Entities;

public class User
{
    public Guid Id { get; private set; } = Guid.CreateVersion7();
    public Boolean Active { get; set; } = true;
    public string Name { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
}
