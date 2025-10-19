namespace Comments.SharedKernel;

public static class ErrorFactory
{
    public static string CreateDetailedError(string error, ErrorCode code, ErrorContext context)
    {
        var location = $"{context.ClassName}::{context.MemberName} (Line {context.LineNumber})";
        return $"[{code}] [{location}] {error}";
    }
}
