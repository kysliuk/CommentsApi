namespace Comments.Contracts;

public sealed record AuthResponse(string Token, Guid UserId, string UserName);
