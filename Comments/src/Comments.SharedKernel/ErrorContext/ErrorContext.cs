namespace Comments.SharedKernel;

public sealed record ErrorContext(string? ClassName, string? MemberName, int LineNumber);
