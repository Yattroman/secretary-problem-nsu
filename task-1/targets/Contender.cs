namespace task_1.targets;

public class Contender : IContender
{
    private string _firstName;
    private string _secondName;

    public Contender(string firstName, string secondName)
    {
        _firstName = firstName;
        _secondName = secondName;
    }

    public string GetDetails()
    {
        return $"{_secondName} {_firstName}";
    }
}