using spn.targets.common.models.interfaces;

namespace spn.targets.common.models;

public class RatedContender
{
    private IContender _contender;
    public int Rate { get; }
    public int OrderNumber { get; set; }
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

    public RatedContender(string firstName, string secondName, int rate)
    {
        _contender = new Contender(firstName, secondName);
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