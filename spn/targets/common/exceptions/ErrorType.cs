namespace task_1.targets.common.exceptions;

public class ErrorType
{
    public ErrorCode ErrorCode { get; }
    public string Message { get; }

    private ErrorType(ErrorCode errorCode, string message)
    {
        ErrorCode = errorCode;
        Message = message;
    }
    
    public static ErrorType HallIsEmpty()
    {
        return new ErrorType(ErrorCode.HallIsEmpty, 
            "There is no contenders in the hall");
    }

    public static ErrorType UnfamiliarContender()
    {
        return new ErrorType(ErrorCode.NotFamiliarWithPrincess,
            "Contender(s) isn(aren)'t familiar with princess yet");
    }
    
    public static ErrorType InvalidContender()
    {
        return new ErrorType(ErrorCode.InvalidContender,
            "Contender(s) is(are) invalid");
    }
    
    public static ErrorType RandomFullNameNetError()
    {
        return new ErrorType(ErrorCode.RandomFullNamesNetError,
            "There are problems with getting random fullNames from Net");
    }
}