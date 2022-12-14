using spn_models.targets.common.models.interfaces;

namespace spn_models.targets.common.models;

public class Contender : IContender
{
    private readonly string _firstName;
    private readonly string _secondName;

    public Contender(string firstName, string secondName)
    {
        _firstName = firstName;
        _secondName = secondName;
    }

    public string? GetDetails()
    {
        return $"{_firstName} {_secondName}";
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