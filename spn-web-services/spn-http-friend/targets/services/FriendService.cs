using spn_db.targets.services;

namespace spn_http_friend.targets.services;

public class FriendService : IFriendService
{
    private const string NameSeparator = " ";
    private const int RequiredDetailsArraySize = 2;
    private const int FirstNamePosition = 0;
    private const int SecondNamePosition = 1;

    private readonly IDbSearchLoveService _dbSearchLoveService;

    public FriendService(IDbSearchLoveService dbSearchLoveService)
    {
        _dbSearchLoveService = dbSearchLoveService;
    }

    public string? CompareTwoContenders(int searchLoveTryId, string? contenderAName, string? contenderBName)
    {
        var contenderADetailsArray = contenderAName?.Split(NameSeparator);
        var contenderBDetailsArray = contenderBName?.Split(NameSeparator);

        if (contenderADetailsArray == null || contenderBDetailsArray == null || 
            contenderADetailsArray.Length != RequiredDetailsArraySize ||
            contenderBDetailsArray.Length != RequiredDetailsArraySize)
        {
            return null;
        }

        var contenderA = _dbSearchLoveService
            .GetContenderInSearchLoveTryByContenderDetails(
                searchLoveTryId,
                contenderADetailsArray[FirstNamePosition],
                contenderADetailsArray[SecondNamePosition]
            );
        
        var contenderB = _dbSearchLoveService
            .GetContenderInSearchLoveTryByContenderDetails(
                searchLoveTryId,
                contenderBDetailsArray[FirstNamePosition],
                contenderBDetailsArray[SecondNamePosition]
            );

        if (contenderA == null || contenderB == null)
        {
            return null;
        }

        return contenderA.Rate > contenderB.Rate ? contenderA.GetDetailsBasic() : contenderB.GetDetailsBasic();
    }
}