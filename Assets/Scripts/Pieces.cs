using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pieces")]
public class Pieces : ScriptableObject
{
    [SerializeField] private List<GameObject> pieces = new List<GameObject>();

    public List<GameObject> GetPieces()
    {
        return pieces;
    }

    public GameObject GetPieceAt(int index)
    {
        if (index < pieces.Count)
        {
            return pieces[index];
        }

        return null;
    }

    public List<GameObject> GetPieces(List<int> indexes)
    {
        List<GameObject> pieces = new List<GameObject>();

        foreach (var index in indexes)
        {
            pieces.Add(GetPieceAt(index));
        }

        return pieces;
    }

    //public GameObject GetRandomPiece()
    //{
    //    var pieceIndex = Random.Range(0, pieces.Count);
    //    return pieces[pieceIndex];
    //}

    //public List<GameObject> GetRandomPieces(int size)
    //{
    //    List<GameObject> pieces = new List<GameObject>();

    //    for (int i = 0; i < size; i++)
    //    {
    //        pieces.Add(GetRandomPiece());
    //    }

    //    return pieces;
    //}
}