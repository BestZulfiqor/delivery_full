namespace Core.Exceptions;

public class NotCreateException : Exception
{
    public NotCreateException()
    {
    }

    public NotCreateException(string message)
        : base(message)
    {
    }

    public NotCreateException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

public class NotUpdatedException : Exception
{
    public NotUpdatedException()
    {
    }

    public NotUpdatedException(string message)
        : base(message)
    {
    }

    public NotUpdatedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}