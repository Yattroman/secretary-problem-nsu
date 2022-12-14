using Microsoft.AspNetCore.Mvc;
using spn_http_friend.targets.services;
using spn_models.targets.common.dto;

namespace spn_http_friend.targets.controllers;

[ApiController]
[Route("[controller]")]
public class FriendController : ControllerBase
{
    private readonly ILogger<FriendController> _logger;
    private readonly IFriendService _friendService;

    public FriendController(ILogger<FriendController> logger, IFriendService friendService)
    {
        _logger = logger;
        _friendService = friendService;
    }

    [Route("/friend/{searchLoveTryId}/compare")]
    [HttpPost]
    public IActionResult CompareTwoContenders([FromQuery] string sessionId, int searchLoveTryId, 
        [FromBody] CompareContendersRequest request)
    {
        var bestContenderName = _friendService
            .CompareTwoContenders(searchLoveTryId, request.ContenderAName, request.ContenderBName);
        _logger.LogInformation($"Best contender between {request.ContenderAName} and {request.ContenderBName}" +
                               $" is {bestContenderName}");
        return Ok(new CompareContendersResponse(bestContenderName));
    }
    
}