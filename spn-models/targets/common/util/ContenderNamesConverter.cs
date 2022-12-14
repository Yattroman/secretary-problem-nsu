namespace spn_models.targets.common.util;

public abstract class ContenderNamesConverter
{
    private const int FirstNamePosition = 0;
    private const int SecondNamePosition = 1;
    private const int RequiredNamesLength = 2;
    private const string NamesSeparator = " ";

    public static (string? first, string? second) parseFullNameFirstSecond(string? fullName)
    {
        var names = fullName?.Split(NamesSeparator);
        return names is not { Length: RequiredNamesLength } ? 
            (null, null) : (names[FirstNamePosition], names[SecondNamePosition]);
    }
}