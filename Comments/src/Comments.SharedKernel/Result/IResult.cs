namespace Comments.SharedKernel;

public interface IResult
{
    bool IsSuccess { get; }
    ErrorCode? ErrorCode { get; }
    string? Error { get; }
    string? FullError { get; }
}
