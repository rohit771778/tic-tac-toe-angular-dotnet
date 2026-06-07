using TicTacToe.Api.Enums;

namespace TicTacToe.Api.Requests;

public class MoveRequest
{
    public Player Player { get; set; }

    public int Row { get; set; }

    public int Column { get; set; }
}