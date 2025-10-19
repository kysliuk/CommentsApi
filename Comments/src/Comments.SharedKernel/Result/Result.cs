namespace Comments.SharedKernel;

public class Result<T> : IResult
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public ErrorCode? ErrorCode { get; }
    public string? Error { get; }
    public string? FullError { get; }

    private Result(bool isSuccess, T? value, ErrorCode? errorCode, string? error, string? fullError)
    {
        IsSuccess = isSuccess;
        Value = value;
        ErrorCode = errorCode;
        Error = error;
        FullError = fullError;
    }

    public static Result<T> Success(T value) =>
        new(true, value, null, null, null);

    public static Result<T> Failure(
        string error,
        ErrorCode errorCode,
        IErrorContextProvider contextProvider)
    {
        var context = contextProvider.GetContext();
        var fullError = ErrorFactory.CreateDetailedError(error, errorCode, context);
        return new(false, default, errorCode, error, fullError);
    }
}
