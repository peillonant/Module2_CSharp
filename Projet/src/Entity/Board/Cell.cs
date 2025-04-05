using System.Numerics;

public enum TypeCell : byte
{
    None,
    OwnBodyHead,
    OwnBodyBody,
    OwnBodyTail,
    Apple,
    Bonus,
    Malus,
    Collision,
    Border,
    Bomb,
    Count
}

public class Cell
{
    protected TypeCell typeCell;
    protected bool b_IsOccupied;
    protected Vector2 v2_CellPosition;

    public TypeCell GetTypeCell() => typeCell;
    public bool IsOccupied() => b_IsOccupied;
    public Vector2 GetCellPosition() => v2_CellPosition;


    public Cell(TypeCell newTypeCell, Vector2 cellPosition)
    {
        typeCell = newTypeCell;
        v2_CellPosition = cellPosition;
        UpdateIsOccupied();
    }

    public void UpdateCell(TypeCell newTypeCell)
    {
        typeCell = newTypeCell;
        UpdateIsOccupied();
    }

    private void UpdateIsOccupied()
    {
        switch (typeCell)
        {
            case TypeCell.None:     // Nothing on the cell
                b_IsOccupied = false;             
                break;
            case TypeCell.Border:    // Border
                b_IsOccupied = true;
                break;
            case TypeCell.OwnBodyHead :     // Body of the snake
            case TypeCell.OwnBodyBody :
            case TypeCell.OwnBodyTail :
                b_IsOccupied = true;
                break;
            case TypeCell.Apple:    // Apple
            case TypeCell.Bonus:
                b_IsOccupied = false;
                break;
            case TypeCell.Malus:    // Malus object
                b_IsOccupied = true;
                break;
            case TypeCell.Collision:    // Collision object
                b_IsOccupied = true;
                break;
            case TypeCell.Bomb:
                b_IsOccupied = true;
                break;
        }
    }
}