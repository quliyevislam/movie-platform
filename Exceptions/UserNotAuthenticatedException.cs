namespace MoviePlatform.Exceptions;

public class UserNotAuthenticatedException : Exception
{
    public UserNotAuthenticatedException(string message) : base(message) {}
}