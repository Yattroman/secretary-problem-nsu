namespace spn_http_friend.targets.services;

public interface IFriendService
{
    string? CompareTwoContenders(int searchLoveTryId, string? contenderAName, string? contenderBName);
}