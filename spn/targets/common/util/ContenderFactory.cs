namespace task_1.targets;

public class ContenderFactory : IContendersFactory
{
    private NameGenerator _nameGenerator = new NameGenerator();

    public IContender CreateNewContender()
    {
        return new Contender(_nameGenerator.GenerateFirstName(), _nameGenerator.GenerateSecondName());
    }
}