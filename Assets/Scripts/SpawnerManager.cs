using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    public GameState gameState;
    public Pieces pieces;

    public Transform leftSpawnPoint;
    public Transform centerSpawnPoint;
    public Transform rightSpawnPoint;

    private void Start()
    {
        if (gameState.GetActivePieces().Count > 0)
        {
            SpawnPieces();
        }
        else
        {
            SpawnRandomPieces();
        }
    }

    private void Update()
    {
        // If there are no active pieces then create 3
        if (gameState.GetActivePieces().Count == 0)
        {
            SpawnRandomPieces();
            gameState.SpawnedNewPieces = true;
        }
    }

    private void SpawnPieces()
    {
        // Get the list of pieces as ints
        List<int> activePieces = new List<int>();
        foreach ( var piece in gameState.GetActivePieceKeys())
        {
            activePieces.Add(piece);
        }
        // Set the gamestate and save it
        gameState.SetAndSaveActivePieces(activePieces);

        // Set the initial position of the pieces
        InstantiatePieces(activePieces);
    }

    private void SpawnRandomPieces()
    {
        // Generate a list of pieces as ints
        List<int> activePieces = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            var piece = Random.Range(0, pieces.GetPieces().Count);
            activePieces.Add(piece);
        }
        // Set the gamestate and save it
        gameState.SetAndSaveActivePieces(activePieces);

        //Instantiate pieces
        InstantiatePieces(activePieces);

    }

    private void InstantiatePieces(List<int> activePieces)
    {
        // Instantiate pieces, set parent
        if (activePieces.Count > 0)
        { 
            var leftPiece = Instantiate(pieces.GetPieceAt(activePieces[0]));
            leftPiece.transform.parent = leftSpawnPoint;
            leftPiece.GetComponent<Piece>().SetInitialPositionAndKey(leftSpawnPoint.transform.position, activePieces[0]);
        }
        if (activePieces.Count > 1)
        {
            var centerPiece = Instantiate(pieces.GetPieceAt(activePieces[1]));
            centerPiece.transform.parent = centerSpawnPoint;
            centerPiece.GetComponent<Piece>().SetInitialPositionAndKey(centerSpawnPoint.transform.position, activePieces[1]);
        }
        if (activePieces.Count > 2)
        {
            var rightPiece = Instantiate(pieces.GetPieceAt(activePieces[2]));
            rightPiece.transform.parent = rightSpawnPoint;
            rightPiece.GetComponent<Piece>().SetInitialPositionAndKey(rightSpawnPoint.transform.position, activePieces[2]);
        }
    }

    public void ResetSpawner()
    {
        // Destroy spawned pieces
        foreach (Transform child in leftSpawnPoint.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Transform child in centerSpawnPoint.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Transform child in rightSpawnPoint.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        // clear active pieces
        gameState.SetActivePieces(new List<int>());

    }

}
