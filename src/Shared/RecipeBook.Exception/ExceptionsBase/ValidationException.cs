namespace RecipeBook.Exception.ExceptionsBase;

public class ValidationException : RecipeBookException
{
    private readonly List<string> _errors;

    public ValidationException(List<string> errors) => _errors = errors;

    public List<string> GetErrorMessages() => _errors;
}
