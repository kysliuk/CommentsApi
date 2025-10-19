using System.Diagnostics;
using Res = Comments.SharedKernel.Properties.Resources;

namespace Comments.SharedKernel;

public class StackTraceErrorContextProvider : IErrorContextProvider
{
    public ErrorContext GetContext()
    {
        var stackTrace = new StackTrace();
        for (int i = 2; i < stackTrace.FrameCount; i++)
        {
            var frame = stackTrace.GetFrame(i);
            var method = frame?.GetMethod();
            var declaringType = method?.DeclaringType;

            if (declaringType != null && !declaringType.FullName!.StartsWith("System.") && !declaringType.FullName.Contains(Res.Result))
                return new ErrorContext(declaringType.FullName, method?.Name, frame!.GetFileLineNumber());
        }

        return new ErrorContext(Res.Unknown, Res.Unknown, 0);
    }
}
