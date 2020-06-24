public class RectanglePiece : Piece
{
    public override bool CanBePlaced()
    {
        var board = gameState.GetBoardState();

        for (int i = 0; i < board.Length - pieceLength + 1; i++)
        {
            for (int j = 0; j < board[i].Length - pieceHeight + 1; j++)
            {
                bool isValidPosition = true;
                for (int k = 0; k < pieceLength; k++)
                {
                    for (int l = 0; l < pieceHeight; l++)
                    {
                        isValidPosition = isValidPosition && board[i + k][j + l] == CellColor.EMPTY;
                    }
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
