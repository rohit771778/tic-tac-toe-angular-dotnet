using TicTacToe.Api.Enums;
using TicTacToe.Api.Models;
using TicTacToe.Api.Requests;

namespace TicTacToe.Api.Services;

public interface IGameService
{
    GameState CreateGame(GameMode mode);

    GameState GetGame(Guid gameId);

    GameState MakeMove(Guid gameId, MoveRequest request);

    GameState Undo(Guid gameId);

    GameState Reset(Guid gameId);
}