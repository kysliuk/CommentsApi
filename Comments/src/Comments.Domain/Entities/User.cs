using Comments.SharedKernel;
using Res = Comments.Domain.Properties.Resources;

namespace Comments.Domain;

public class User : EntityBase
{
    public string UserName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;

    public void SetPasswordHash(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash)) throw new ArgumentException(Res.Auth_HashRequired);
        PasswordHash = hash;
        Touch();
    }

    protected User() { }

    public User(string userName, string email, string passwordHash)
    {
        UserName = userName;
        Email = email;
        PasswordHash = passwordHash;
    }
}
