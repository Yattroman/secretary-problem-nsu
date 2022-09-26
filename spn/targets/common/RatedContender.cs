namespace task_1.targets;

public class RatedContender
{
    private IContender _contender;
    private int _rate;

    public int Rate
    {
        get => _rate;
        set => _rate = value;
    }

    public string GetDetails()
    {
        return $"{_contender.GetDetails()} | Rate: {_rate}";
    }

    public RatedContender(IContender contender, int rate)
    {
        _contender = contender;
        _rate = rate;
    }
}