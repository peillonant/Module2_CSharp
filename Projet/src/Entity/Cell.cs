using System.Numerics;
using Raylib_cs;

public enum TypeCell : byte
{
    None,
    Bonus,
    Apple,
    Malus,
    Collision,
    OwnBody,
    Border
}

public class Cell
{
    private TypeCell typeCell; 

    private Cell? parent;
    private Cell? connectedCell;
    private Vector2 v2_CellPosition;
    private int i_cost;

    bool b_IsOccupied; 
    private bool b_InFrontier = false;
    public bool b_CanBeReached { get { return !b_IsOccupied && b_InFrontier; } }

    #region Encapsulation
    // Setter
    public void SetParentCell (Cell parent) => this.parent = parent;
    public void SetConnectedCell (Cell connectedCell) => this.connectedCell = connectedCell;
    public void SetCost (int cost) => this.i_cost = cost;
    public void SetInFrontier (bool frontier) => this.b_InFrontier = frontier;

    // Getter
    public Cell? GetParentCell () => parent;
    public Cell? GetConnectedCell() => connectedCell;
    public Vector2 GetCellPosition() => v2_CellPosition;
    public int GetCost () => i_cost;
    public bool GetInFrontier () => b_InFrontier;
    public TypeCell GetTypeCell() => typeCell;
    public bool GetIsOccupied () => b_IsOccupied;
    #endregion
    

    public Cell(Vector2 v2_CellPosition, Board board)
    {
        this.v2_CellPosition = v2_CellPosition;
        parent = null;
        connectedCell = null;

        CheckIsOccupied(board);
    }

    private void CheckIsOccupied(Board board)
    {
        int i_valueCell = board.GetValueBoard(v2_CellPosition);

        switch (i_valueCell)
        {
            case -1:    // Border
                typeCell = TypeCell.Border;
                b_IsOccupied = true;
                break;
            case 0:     // Nothing on the cell
                typeCell = TypeCell.None;
                break;
            case < 4:     // Body of the snake
                typeCell = TypeCell.OwnBody;
                b_IsOccupied = true;
                break;
            case 10:    // Apple
                typeCell = TypeCell.Apple;
                break;
            case 11:    // Boxe
                typeCell = TypeCell.Bonus;
                break;
            case 20:    // Malus object
                typeCell = TypeCell.Malus;
                b_IsOccupied = true;
                break;
            case < 30:    // Collision object
                typeCell = TypeCell.Collision;
                b_IsOccupied = true;
                break;
        }
    }
}