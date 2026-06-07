using TicTacToe.Api.Models;

namespace TicTacToe.Api.Services;

public interface IScoreboardService
{
    Scoreboard GetScoreboard();

    void IncrementXWin();

    void IncrementOWin();

    void IncrementDraw();

    void Reset();
}