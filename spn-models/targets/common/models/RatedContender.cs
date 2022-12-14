using spn_models.targets.common.models.interfaces;

namespace spn_models.targets.common.models;

public class RatedContender
{
    private readonly IContender _contender;

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

    public int Rate { get; }
    public int OrderNumber { get; set; }
    public bool IsFamiliarWithPrincess { get; set; }

    public string? GetDetails()
    {
        return $"{_contender.GetDetails()} | Rate: {Rate}";
    }
    
    public string? GetDetailsBasic()
    {
        return _contender.GetDetails();
    }

    public void MeetWithPrincess()
    {
        IsFamiliarWithPrincess = true;
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