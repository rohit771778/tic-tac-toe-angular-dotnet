using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Services;

namespace TicTacToe.Api.Controllers;

[ApiController]
[Route("api/scoreboard")]
public class ScoreboardController : ControllerBase
{
    private readonly IScoreboardService _scoreboardService;

    public ScoreboardController(IScoreboardService scoreboardService)
    {
        _scoreboardService = scoreboardService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_scoreboardService.GetScoreboard());
    }

    [HttpPost("reset")]
    public IActionResult Reset()
    {
        _scoreboardService.Reset();

        return Ok();
    }
}