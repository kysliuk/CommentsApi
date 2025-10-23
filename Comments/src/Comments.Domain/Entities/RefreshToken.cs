using Comments.SharedKernel;

namespace Comments.Domain;

public class RefreshToken : EntityBase
{
    public Guid UserId { get; private set; }
    public string Token { get; private set; } = default!;
    public DateTime ExpiresAt { get; private set; }
    public bool IsRevoked { get; private set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

    public void Revoke() => IsRevoked = true;

    private RefreshToken() { }

    public RefreshToken(Guid userId, string token, DateTime expiresAt)
    {
        UserId = userId;
        Token = token;
        ExpiresAt = expiresAt;
    }
}
