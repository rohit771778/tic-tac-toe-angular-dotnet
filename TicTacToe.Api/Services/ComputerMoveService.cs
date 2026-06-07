namespace TicTacToe.Api.Services;

public class ComputerMoveService
{
    public (int row, int col)? GetMove(string[][] board)
    {
        if (string.IsNullOrEmpty(board[1][1]))
            return (1, 1);

        int[][] corners =
        {
            new[] {0,0},
            new[] {0,2},
            new[] {2,0},
            new[] {2,2}
        };

        foreach (var corner in corners)
        {
            if (string.IsNullOrEmpty(board[corner[0]][corner[1]]))
            {
                return (corner[0], corner[1]);
            }
        }

        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                if (string.IsNullOrEmpty(board[r][c]))
                {
                    return (r, c);
                }
            }
        }

        return null;
    }
}