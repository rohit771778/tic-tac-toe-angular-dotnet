using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Requests;
using TicTacToe.Api.Services;

namespace TicTacToe.Api.Controllers;

[ApiController]
[Route("api/games")]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;

    public GamesController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost]
    public IActionResult Create(CreateGameRequest request)
    {
        return Ok(_gameService.CreateGame(request.Mode));
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        return Ok(_gameService.GetGame(id));
    }

[HttpPost("{id}/moves")]
public IActionResult Move(Guid id, MoveRequest request)
{
    try
    {
        return Ok(_gameService.MakeMove(id, request));
    }
    catch(Exception ex)
    {
        return BadRequest(ex.Message);
    }
}

    [HttpPost("{id}/undo")]
    public IActionResult Undo(Guid id)
    {
        return Ok(_gameService.Undo(id));
    }

    [HttpPost("{id}/reset")]
    public IActionResult Reset(Guid id)
    {
        return Ok(_gameService.Reset(id));
    }
}