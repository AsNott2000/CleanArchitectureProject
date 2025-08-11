using System.Security.Cryptography;
using System.Text;
using CleanArchitectureProject.Domain.Services;

namespace CleanArchitectureProject.Infrastructure.Security;

public class SimplePasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes); 
    }

    public bool Verify(string password, string hash)
        => string.Equals(Hash(password), hash, StringComparison.OrdinalIgnoreCase);
}
