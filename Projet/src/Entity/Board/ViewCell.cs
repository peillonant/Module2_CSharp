using System.Numerics;
using Raylib_cs;

public class ViewCell : Cell
{
    private Vector2 v2_PositionToWorld;
    public ViewCell(Vector2 v2_CellPosition, TypeCell newTypeCell) : base(newTypeCell, v2_CellPosition)
    {
        v2_PositionToWorld = CreatePositionToWorld(v2_CellPosition);
    }

    #region Encapsulation
    public void SetPositionToWorld(Vector2 v2_newPositionToWorld) => v2_PositionToWorld = v2_newPositionToWorld;
    public void ResetPositionToWorld(Vector2 v2_newPositionToWorld) => CreatePositionToWorld(v2_newPositionToWorld);
    public Vector2 GetPositionToWorld() => v2_PositionToWorld;
    #endregion

    private static Vector2 CreatePositionToWorld(Vector2 v2_CellPosition) => v2_CellPosition * GameInfo.i_SizeCell;
}