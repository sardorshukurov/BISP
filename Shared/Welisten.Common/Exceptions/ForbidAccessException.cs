namespace Welisten.Common.Exceptions;

/// <summary>
/// Exception for catch forbidden access
/// </summary>
public class ForbidAccessException : Exception
{
    public string Code { get; } = string.Empty;


    public ForbidAccessException()
    {
    }

    public ForbidAccessException(string message) : base(message)
    {
    }

    public ForbidAccessException(Exception inner) : base(inner.Message, inner)
    {
    }

    public ForbidAccessException(string message, Exception inner) : base(message, inner)
    {
    }

    public ForbidAccessException(string code, string message) : base(message)
    {
        Code = code;
    }

    public ForbidAccessException(string code, string message, Exception inner) : base(message, inner)
    {
        Code = code;
    }
}