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
    protected float f_Timer = 0;
    protected float f_LifeTimer = 0;

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
        f_Timer = 0;
        f_LifeTimer = 0;
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

    public void UpdateCellTimer(float dt) => f_Timer += dt;
    public void SetCellTimer(float f_newTimer) => f_Timer = f_newTimer;
    public void SetCellLifeTimer(float f_newLifeTimer) => f_LifeTimer = f_newLifeTimer;
    public float GetCellTimer() => f_Timer;
    public float GetCellLifeTimer () => f_LifeTimer;
}