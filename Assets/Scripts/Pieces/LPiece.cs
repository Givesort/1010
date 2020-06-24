using System;

public class LPiece : Piece
{
    public bool isBottom = false;
    public bool isLeft = false;
    public override bool CanBePlaced()
    {
        var board = gameState.GetBoardState();

        for (int i = 0; i < board.Length - pieceLength + 1; i++)
        {
            for (int j = 0; j < board[i].Length - pieceHeight + 1; j++)
            {
                bool isValidPosition = true;

                int verticalOffset = isBottom ? 0 : pieceHeight - 1;
                int horizontalOffset = isLeft ? 0 : pieceLength - 1;

                // Check horizontal cells
                for (int k = 0; k < pieceLength; k++)
                {
                    isValidPosition = isValidPosition && board[i + k][j + verticalOffset] == CellColor.EMPTY;
                }

                // Check vertical cells
                for (int k = 0; k < pieceHeight; k++)
                {
                    isValidPosition = isValidPosition && board[i + horizontalOffset][j + k] == CellColor.EMPTY;
                }
                if (isValidPosition)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
