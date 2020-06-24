using System.Collections.Generic;
using UnityEngine;

public class BoardCell : MonoBehaviour
{
    public List<Sprite> cellSprites;

    private CellColor cellColor = CellColor.EMPTY;
    [SerializeField] private bool isEmpty =  true;

    public CellColor GetCellColor()
    {
        return cellColor;
    }

    public bool GetIsEmpty()
    {
        return isEmpty;
    }

    public void SetIsEmpty(bool empty)
    {
        isEmpty = empty;
    }

    public void PlacePieceInCell(CellColor color)
    {
        isEmpty = false;
        cellColor = color;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = cellSprites[(int)cellColor];
    }

    public void ClearCell()
    {
        isEmpty = true;
        cellColor = CellColor.EMPTY;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = cellSprites[0];
    }
}
