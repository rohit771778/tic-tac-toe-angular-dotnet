using TicTacToe.Api.Enums;

namespace TicTacToe.Api.Requests;

public class CreateGameRequest
{
    public GameMode Mode { get; set; }
}