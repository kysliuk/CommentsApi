using Comments.Application;
using Microsoft.AspNetCore.Identity;

namespace Comments.Infrastructure;

public sealed class PasswordHasherAdapter : IPasswordHasher
{
    private readonly PasswordHasher<object> _hasher = new();

    public string Hash(string password) => _hasher.HashPassword(null!, password);

    public bool Verify(string hashedPassword, string providedPassword)
    {
        var res = _hasher.VerifyHashedPassword(null!, hashedPassword, providedPassword);
        return res is PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded;
    }
}
