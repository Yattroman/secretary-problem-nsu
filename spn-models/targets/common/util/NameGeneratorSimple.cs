namespace spn_models.targets.common.util;

public class NameGeneratorSimple
{
    private readonly ISet<string> _firstNameSet = new HashSet<string>
        { "Roma", "Michail", "Ruslan", "Tigran", "Nidzhat" };

    private readonly Random _random = new();

    private readonly ISet<string> _secondNameSet = new HashSet<string>
        { "Ivanov", "Petrov", "Sidorov", "Babayan", "Eginyan" };

    public List<(string First, string Second)> GenerateFullNames(int quantity)
    {
        var fullnames = new List<(string First, string Second)>();
        for (var i = 0; i < quantity; i++) fullnames.Add((GenerateFirstName(), GenerateSecondName()));

        return fullnames;
    }

    private string GenerateFirstName()
    {
        return _firstNameSet.ElementAt(_random.Next(_firstNameSet.Count));
    }

    private string GenerateSecondName()
    {
        return _secondNameSet.ElementAt(_random.Next(_secondNameSet.Count));
    }
}