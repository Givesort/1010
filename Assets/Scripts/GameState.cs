using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState")]
public class GameState : ScriptableObject
{
    public Pieces pieces;

    [SerializeField] private int _score;
    [SerializeField] private int _highScore;
    [SerializeField] private CellColor[][] _boardState;
    [SerializeField] private List<int> _activePieces = new List<int>();

    public bool BoardChanged { set; get; }
    public bool SpawnedNewPieces { set; get; }

    private void OnEnable()
    {
        _score = PlayerPrefs.GetInt("Score");
        _highScore = PlayerPrefs.GetInt("HighScore");
    }

    public int GetScore()
    {
        return _score;
    }

    public void AddScore(int scoreDelta)
    {
        SetScore(_score + scoreDelta);
    }

    public void SetScore(int score)
    {
        _score = score;
        PlayerPrefs.SetInt("Score", score);
        if (_score > _highScore)
        {
            SetHighScore(score);
        }
    }

    public int GetHighScore()
    {
        return _highScore;
    }

    public void SetHighScore(int highScore)
    {
        _highScore = highScore;
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    public List<GameObject> GetActivePieces()
    {
        return pieces.GetPieces(_activePieces);
    }

    public List<int> GetActivePieceKeys()
    {
        return _activePieces;
    }

    public void SetActivePieces(List<int> activePieces)
    {
        _activePieces = activePieces;
    }

    public void SaveActivePieces()
    {
        var serialized = "";
        foreach (var piece in _activePieces)
        {
            serialized += piece.ToString() + ",";
        }
        serialized = serialized.Trim(',');
        PlayerPrefs.SetString("ActivePieces", serialized);
    }

    public void SetAndSaveActivePieces(List<int> activePieces)
    {
        SetActivePieces(activePieces);
        SaveActivePieces();
    }

    public void RemoveActivePiece(int activePiece)
    {
        _activePieces.Remove(activePiece);
    }

    public void RemoveAndSaveActivePieces(int activePiece)
    {
        RemoveActivePiece(activePiece);
        SaveActivePieces();
    }

    public void LoadActivePieces()
    {
        var serialized = PlayerPrefs.GetString("ActivePieces");
        List<int> activePieces = new List<int>();
        var columns = serialized.Split(',');
        for (int i = 0; i < Mathf.Min(columns.Length, 3); i++)
        {
            int activePiece = int.TryParse(columns[i], out activePiece) ? activePiece : -1;
            // If valid piece then add it to the list of active pieces
            if (activePiece >= 0 && activePiece < pieces.GetPieces().Count)
            {
                activePieces.Add(activePiece);
            }
        }
    }

    public void SaveBoardState(CellColor[][] boardState)
    {
        _boardState = boardState;

        // Write boardstate to string and save in playerprefs
        string serialized = "";
        for (int i = 0; i < boardState.Length; i++)
        {
            for (int j = 0; j < boardState[i].Length; j++)
            {
                var cellColor = (int) boardState[i][j];
                serialized += cellColor.ToString() + ",";
            }
            serialized = serialized.Trim(',');
            serialized += "n";
        }
        serialized = serialized.Trim('n');
        PlayerPrefs.SetString("BoardState", serialized);
    }

    public void LoadBoardState()
    {

        var serialized = PlayerPrefs.GetString("BoardState");
        var rows = serialized.Split('n');

        CellColor[][] boardState = GetCleanBoardState();

        for (int i = 0; i < Mathf.Min(rows.Length, boardState.Length); i++)
        {
            var columns = rows[i].Split(',');
            boardState[i] = new CellColor[10];

            for (int j = 0; j < Mathf.Min(columns.Length, boardState[0].Length); j++)
            {
                if (j < boardState.Length &&  i < boardState.Length)
                {
                    int entry = int.TryParse(columns[j], out entry) ? entry : 0;
                    boardState[i][j] = (CellColor)entry;
                }
            }
        }

        SaveBoardState(boardState);
    }

    private CellColor[][] GetCleanBoardState()
    {
        CellColor[][] cleanBoard = new CellColor[10][];
        for (int i = 0; i < 10; i++)
        {
            cleanBoard[i] = new CellColor[10];
            for (int j = 0; j < 10; j++)
            {
                cleanBoard[i][j] = CellColor.EMPTY;
            }
        }
        return cleanBoard;
    }

    public CellColor[][] GetBoardState()
    {
        return _boardState;
    }
}
