namespace MoviePlatform.Exceptions;

public class InvalidUserIdentifierException : Exception
{
    public InvalidUserIdentifierException(string message) : base(message) {}
}