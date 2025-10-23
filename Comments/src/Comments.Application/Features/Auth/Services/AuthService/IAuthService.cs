using Comments.Contracts;
using Comments.SharedKernel;

namespace Comments.Application;

public interface IAuthService
{
    Task<Result<AuthResponse>> RegisterAsync(RegisterUserDto dto, CancellationToken cancellationToken = default);
    Task<Result<AuthResponse>> LoginAsync(LoginUserDto dto, CancellationToken cancellationToken = default);
    Task<Result<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenDto dto, CancellationToken cancellationToken = default);
}

