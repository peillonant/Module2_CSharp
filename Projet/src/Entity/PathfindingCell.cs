using System.Numerics;
using Raylib_cs;

// Create a class Cell that contain
// - typeCell, b_IsOccupied

// Then transform this class into Pathfinding Cell that heritate from cell with the information added

public class PathfindingCell : Cell
{
    private PathfindingCell? parent;
    private PathfindingCell? connectedCell;
    private Vector2 v2_CellPosition;
    private int i_cost;

    private bool b_InFrontier = false;
    public bool b_CanBeReached { get { return !b_IsOccupied && b_InFrontier; } }

    public PathfindingCell(Vector2 v2_CellPosition, TypeCell newTypeCell) : base(newTypeCell)
    {
        this.v2_CellPosition = v2_CellPosition;
        parent = null;
        connectedCell = null;
    }

    #region Encapsulation
    // Setter
    public void SetParentCell(PathfindingCell parent) => this.parent = parent;
    public void SetConnectedCell(PathfindingCell connectedCell) => this.connectedCell = connectedCell;
    public void SetCost(int cost) => this.i_cost = cost;
    public void SetInFrontier(bool frontier) => this.b_InFrontier = frontier;

    // Getter
    public PathfindingCell? GetParentCell() => parent;
    public PathfindingCell? GetConnectedCell() => connectedCell;
    public Vector2 GetCellPosition() => v2_CellPosition;
    public int GetCost() => i_cost;
    public bool GetInFrontier() => b_InFrontier;
    public bool GetIsOccupied() => b_IsOccupied;
    #endregion
}