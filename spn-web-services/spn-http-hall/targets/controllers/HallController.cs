using Microsoft.AspNetCore.Mvc;
using spn_http_hall.targets.services;
using spn_models.targets.common.dto;

namespace spn_http_hall.targets.controllers;

[ApiController]
[Route("[controller]")]
public class HallController : ControllerBase
{
    private readonly ILogger<HallController> _logger;
    private readonly IHallService _hallService;

    public HallController(ILogger<HallController> logger, IHallService hallService)
    {
        _logger = logger;
        _hallService = hallService;
    }

    [Route("/hall/reset")]
    [HttpPost]
    public IActionResult ResetAllSearchLoveTriesStates([FromQuery] string sessionId)
    {
        var response = _hallService.ResetAllSearchLoveTriesStates();
        _logger.LogInformation(response);
        return Ok(response);
    }

    [Route("/hall/{searchLoveTryId}/next")]
    [HttpPost]
    public IActionResult ReturnNextContenderForCertainTry([FromQuery] string sessionId, int searchLoveTryId)
    {
        var contender = _hallService.ReturnNextContenderForCertainTry(searchLoveTryId);
        _logger.LogInformation($"#{searchLoveTryId} | {contender?.GetDetails()}");
        return Ok(new NextContenderResponse(contender?.GetDetails()));
    }

    [Route("/hall/{searchLoveTryId}/select")]
    [HttpPost]
    public IActionResult ReturnSelectedContenderRate([FromQuery] string sessionId, int searchLoveTryId)
    {
        var selectedContenderRate = _hallService.ReturnSelectedContenderRate(searchLoveTryId);
        _logger.LogInformation($"Selected contender's rate for SLT #{searchLoveTryId}: {selectedContenderRate}");
        return Ok(new ContenderRankResponse(selectedContenderRate));
    }
    
    [Route("/hall/{searchLoveTryId}/showRealContenderRank")]
    [HttpPost]
    public IActionResult ShowRealContenderRank([FromQuery] string sessionId, int searchLoveTryId, 
        [FromBody] RealContenderRankRequest request)
    {
        var contenderRate = _hallService.ReturnContenderRankByName(searchLoveTryId, request.ContenderName);
        return Ok(new ContenderRankResponse(contenderRate));
    }
}