using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject board;
    public GameObject pieceSpawner;

    public GameState gameState;

    private void Start()
    {
        gameState.LoadBoardState();
        //gameState.LoadActivePieces();
        bool existsPlaceablePiece = ExistsPlaceablePiece();

        // Reset game if none of the pieces can be placed
        if (!existsPlaceablePiece && gameState.GetActivePieces().Count > 0)
        {
            ResetGame();
        }
    }

    private void Update()
    {
        if (gameState.BoardChanged || gameState.SpawnedNewPieces)
        {

            gameState.SpawnedNewPieces = false;

            bool existsPlaceablePiece = ExistsPlaceablePiece();

            // Reset game if none of the pieces can be placed
            if (!existsPlaceablePiece && gameState.GetActivePieces().Count > 0)
            {
                ResetGame();
            }
        }
    }

    private bool ExistsPlaceablePiece()
    {
        // Detect if one of the spawned pieces can be placed
        bool existsPlaceablePiece = false;

        foreach (var piece in gameState.GetActivePieces())
        {
            existsPlaceablePiece = existsPlaceablePiece || piece.GetComponent<Piece>().CanBePlaced();
        }

        return existsPlaceablePiece;
    }

    private void ResetGame()
    {
        gameState.SetScore(0);
        board.GetComponent<BoardManager>().ResetBoard();
        pieceSpawner.GetComponent<SpawnerManager>().ResetSpawner();
    }
}
