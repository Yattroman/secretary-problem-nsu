namespace spn_models.targets.common.models;

public class StrategyResponse
{
    private StrategyResponse(string message, RatedContender? contender)
    {
        Message = message;
        Contender = contender;
    }

    public string Message { get; }
    public RatedContender? Contender { get; }

    public static StrategyResponse InvalidContender()
    {
        return new StrategyResponse("Invalid contender", null);
    }

    public static StrategyResponse NotEnoughInfo()
    {
        return new StrategyResponse("Not enough info to decide who is better...", null);
    }

    public static StrategyResponse ItCanBeBetter()
    {
        return new StrategyResponse("There is information that this one isn't best...", null);
    }

    public static StrategyResponse ThatIsBestChoice(RatedContender contender)
    {
        return new StrategyResponse("This maaan is good choice", contender);
    }

    public static StrategyResponse NoVariantsSuitable()
    {
        return new StrategyResponse("It can be better to stay old maid", null);
    }
}