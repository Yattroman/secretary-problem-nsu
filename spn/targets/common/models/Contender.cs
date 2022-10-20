namespace task_1.targets;

public class Contender : IContender
{
    private readonly string _firstName;
    private readonly string _secondName;

    public Contender(string firstName, string secondName)
    {
        _firstName = firstName;
        _secondName = secondName;
    }

    public string GetDetails()
    {
        return $"{_secondName} {_firstName}";
    }

    public string GetFirstName()
    {
        return _firstName;
    }

    public string GetSecondName()
    {
        return _secondName;
    }
}