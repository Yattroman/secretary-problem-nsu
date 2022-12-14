using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using spn_http_princess.targets.services;

namespace spn_http_princess.targets.controllers;

[ApiController]
[Route("[controller]")]
public class PrincessController : ControllerBase
{
    private readonly IPrincessService _princessService;

    public PrincessController(IPrincessService princessService)
    {
        _princessService = princessService;
    }
    
    [Route("/princess/{searchLoveTryId}/searchLove")]
    [HttpPost]
    public IActionResult ReturnSelectedContenderRate(int searchLoveTryId, [FromQuery] string sessionId)
    {
        return Ok(_princessService.SearchLoveTryByPreparedData(searchLoveTryId, sessionId));
    }
}