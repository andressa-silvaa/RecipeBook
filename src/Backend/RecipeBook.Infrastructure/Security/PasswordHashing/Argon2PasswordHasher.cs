using Konscious.Security.Cryptography;
using RecipeBook.Domain.Security.PasswordHashing;
using System.Security.Cryptography;
using System.Text;

namespace RecipeBook.Infrastructure.Security.PasswordHashing;

internal sealed class Argon2PasswordHasher : IPasswordHasher
{
    private const int DEGREE_OF_PARALLELISM = 1;
    private const int INTERATIONS = 2;
    private const int MEMORY_SIZE = 20 * 1024;
    private const int SALT_SIZE = 16;
    private const int HASH_SIZE = 32;

    public string HashPassword(string password)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var salt = RandomNumberGenerator.GetBytes(SALT_SIZE);

        var hashAlgorithm = new Argon2id(passwordBytes)
        {
            DegreeOfParallelism = DEGREE_OF_PARALLELISM,
            Iterations = INTERATIONS,
            MemorySize = MEMORY_SIZE,
            Salt = salt,
        };
        
        var hash = hashAlgorithm.GetBytes(HASH_SIZE);
        var combinedBytes = new byte[hash.Length + salt.Length];

        salt.CopyTo(combinedBytes);
        hash.CopyTo(combinedBytes, index: salt.Length);

       return Convert.ToBase64String(combinedBytes);

    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        throw new NotImplementedException();
    }
}
