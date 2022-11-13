namespace spn.targets.common.exceptions;

public class SecretaryProblemException : Exception
{
    public SecretaryProblemException()
    {
    }

    public SecretaryProblemException(ErrorType errorType) :
        base($"{errorType.ErrorCode}: {errorType.Message}")
    {
    }

    public SecretaryProblemException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}