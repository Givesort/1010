using System.Collections.Generic;
using UnityEngine;

public class PieceCell : MonoBehaviour
{
    [SerializeField] private CellColor cellColor;

    private List<GameObject> currentCollisions = new List<GameObject>();

    public List<GameObject> GetCurrentCollisions()
    {
        return currentCollisions;
    }

    public void SetCellColor(CellColor color)
    {
        cellColor = color;
    }

    public CellColor GetCellColor()
    {
        return cellColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if gameobject is a BoardCell add it to the list.
        if (collision.gameObject.GetComponentInChildren<BoardCell>() != null)
        {
            currentCollisions.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // if gameobject is a BoardCell remove it to the list.
        if (collision.gameObject.GetComponentInChildren<BoardCell>() != null)
        {
            currentCollisions.Remove(collision.gameObject);
        }
    }

}
