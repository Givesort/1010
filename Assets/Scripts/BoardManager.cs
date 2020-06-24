using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameState gameState;
    public GameObject[][] board;

    public GameObject gridCellPrefab;

    [SerializeField] private float leftLocation = -2.025f;
    [SerializeField] private float bottomLocation = -2.025f;
    [SerializeField] private float cellSpacing = .45f;

    private void Start()
    {
        var gameStateBoard = gameState.GetBoardState();

        board = new GameObject[10][];

        // Create board
        for (int i = 0; i < 10; i++)
        {
            board[i] = new GameObject[10];
            for (int j = 0; j < 10; j++)
            {
                // Instantiate board cell, set position and parent
                var gridCell = Instantiate(gridCellPrefab);
                gridCell.transform.position = new Vector3(leftLocation + i * cellSpacing,
                    bottomLocation + j * cellSpacing, 0);
                gridCell.transform.parent = this.transform;

                // Populate board with saved board
                if (gameStateBoard[i][j] != CellColor.EMPTY)
                {
                    gridCell.GetComponent<BoardCell>().PlacePieceInCell(gameStateBoard[i][j]);
                }


                board[i][j] = gridCell;
            }
        }
    }

    private void Update()
    {        
        if (gameState.BoardChanged)
        {
            // Remove completed lines and calculate points

            // Detect completed lines
            int[] horizontalCount = new int[board.Length];
            int[] verticalCount = new int[board.Length];

            List<int> completedRows = new List<int>();
            List<int> completedColumns = new List<int>();

            var originalBoard = gameState.GetBoardState();
            int originalCount = 0;
            int newCount = 0;

            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (board[i][j].GetComponent<BoardCell>().GetCellColor() != CellColor.EMPTY)
                    {
                        newCount += 1;
                        horizontalCount[i] += 1;
                        verticalCount[j] += 1;
                        if (horizontalCount[i] == 10)
                        {
                            completedRows.Add(i);
                        }
                        if (verticalCount[j] == 10)
                        {
                            completedColumns.Add(j);
                        }
                    }
                    if (originalBoard[i][j] != CellColor.EMPTY)
                    {
                        originalCount += 1;
                    }
                }
            }

            // Calculate score and add to game state
            int scoreDelta = newCount - originalCount;
            for (int i = completedRows.Count + completedColumns.Count; i > 0; i--)
            {
                scoreDelta += i * 10;
            }
            gameState.AddScore(scoreDelta);

            // Set and save board state
            CellColor[][] newBoard = new CellColor[10][];
            for (int i = 0; i < board.Length; i++)
            {
                newBoard[i] = new CellColor[10];
                for (int j = 0; j < board[i].Length; j++)
                {
                    var boardCell = board[i][j].GetComponent<BoardCell>();

                    if (completedRows.Contains(i) || completedColumns.Contains(j))
                    {
                        boardCell.ClearCell();
                    }

                    newBoard[i][j] = boardCell.GetCellColor();
                }
            }
            // Update board
            gameState.SaveBoardState(newBoard);

            // Update board changed flag
            gameState.BoardChanged = false;
        }
    }

    public void ResetBoard()
    {
        // Clear the board
        CellColor[][] cleanBoard = new CellColor[10][];
        for (int i = 0; i < 10; i++)
        {
            cleanBoard[i] = new CellColor[10];
            for (int j = 0; j < 10; j++)
            {
                if (board != null)
                {
                    board[i][j].GetComponent<BoardCell>().ClearCell();
                }
                cleanBoard[i][j] = CellColor.EMPTY;
            }
        }

        // Clear the game state
        gameState.SaveBoardState(cleanBoard);
    }
}
