namespace task_1.targets;

public class RatedContender
{
    private IContender _contender;
    public int Rate { get; }
    public bool IsFamiliarWithPrincess { get; set; }

    public string GetDetails()
    {
        return $"{_contender.GetDetails()} | Rate: {Rate}";
    }
    
    public void MeetWithPrincess()
    {
        IsFamiliarWithPrincess = true;
    }

    public RatedContender(IContender contender, int rate)
    {
        _contender = contender;
        Rate = rate;
    }

    public string GetFirstName()
    {
        return _contender.GetFirstName();
    }
    
    public string GetSecondName()
    {
        return _contender.GetSecondName();
    }
}