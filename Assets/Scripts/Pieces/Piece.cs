using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public GameState gameState;

    private Vector3 reducedScale = new Vector3(.7f, .7f, 1);
    private Vector3 normalScale = new Vector3(1, 1, 1);

    [SerializeField] private Vector3 offset = new Vector3(0, 0, 0);
    private Vector3 initialPosition;
    private int pieceKey;

    private bool isBeingHeld = false;
    private bool wasBeingHeld = false;

    public int pieceLength = 1;
    public int pieceHeight = 1;

    public void SetInitialPositionAndKey(Vector3 position, int key)
    {
        initialPosition = position;
        pieceKey = key;
        this.gameObject.transform.position = initialPosition;
        this.gameObject.transform.localScale = reducedScale;
    }

    private void Update()
    {
        if (isBeingHeld)
        {
            // Get mouse position
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, 0) + offset;
            this.gameObject.transform.localScale = normalScale;
        }
        else
        {
            // If piece was moved
            if (this.gameObject.transform.position != initialPosition && wasBeingHeld)
            {

                var childrenCells = GetComponentsInChildren<PieceCell>();

                // Get list of intersecting board cells
                List<GameObject> boardCells = new List<GameObject>();
                foreach (var childCell in childrenCells)
                {
                    boardCells.AddRange(childCell.GetCurrentCollisions());
                }

                // Check to see if the drop location is a valid location on the board
                bool isValidPosition = boardCells.Count == childrenCells.Count();
                foreach (var boardCell in boardCells)
                {
                    isValidPosition = isValidPosition && boardCell.GetComponent<BoardCell>().GetIsEmpty();
                }

                // Later add check to see if this is a valid spot on the board to put the piece
                if (isValidPosition)
                {
                    // Get the cell color and place in the board cell
                    var cellColor = childrenCells.ElementAt(0).GetCellColor();

                    foreach (var boardCell in boardCells)
                    {
                        boardCell.GetComponent<BoardCell>().PlacePieceInCell(cellColor);
                    }

                    gameState.RemoveAndSaveActivePieces(pieceKey);
                    gameState.BoardChanged = true;
                    Destroy(this.gameObject);
                }
                else
                {
                    this.gameObject.transform.position = initialPosition;
                    this.gameObject.transform.localScale = reducedScale;
                    wasBeingHeld = false;
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isBeingHeld = true;
        }
    }

    private void OnMouseUp()
    {
        isBeingHeld = false;
        wasBeingHeld = true;
    }

    public abstract bool CanBePlaced();

    public int GetPieceKey()
    {
        return pieceKey;
    }

}
