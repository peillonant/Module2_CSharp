public enum TypeCell : byte
{
    None,
    OwnBodyHead,
    OwnBodyBody,
    OwnBodyTail,
    Bonus,
    Apple,
    Malus,
    Collision,
    Border,
    Count
}

public class Cell
{
    private TypeCell typeCell;
    protected bool b_IsOccupied;

    public TypeCell GetTypeCell() => typeCell;
    public bool IsOccupied() => b_IsOccupied;

    public Cell(TypeCell newTypeCell)
    {
        typeCell = newTypeCell;
        UpdateIsOccupied();
    }

    public void UpdateCell(TypeCell newTypeCell)
    {
        typeCell = newTypeCell;
        UpdateIsOccupied();
    }

    public void UpdateIsOccupied()
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
        }
    }
}