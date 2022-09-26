namespace task_1.targets;

public class NameGenerator
{
    private Random _random = new Random(); 
    private ISet<string> _firstNameSet = new HashSet<string>() {"Roma", "Michail", "Ruslan", "Tigran", "Nidzhat"};
    private ISet<string> _secondNameSet = new HashSet<string>() {"Ivanov", "Petrov", "Sidorov", "Babayan", "Eginyan"};

    public string GenerateFirstName()
    {
        return _firstNameSet.ElementAt(_random.Next(_firstNameSet.Count));
    }
    
    public string GenerateSecondName()
    {
        return _secondNameSet.ElementAt(_random.Next(_secondNameSet.Count));
    }

}