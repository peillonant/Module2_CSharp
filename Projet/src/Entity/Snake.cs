using System.Numerics;
using Raylib_cs;

public class Snake
{
    private int i_Size;
    private int i_Direction;                         // 1 = Top, 2 = Right, 3 = Bottom, 4 = Left   
    private float f_timerMove;
    private bool b_CanMove;

    private Vector2 v2_Head = new();
    private List<Vector2> v2_Bodys = new();
    private Vector2 v2_Tail = new(-1, -1);

    public Snake(int i_newDirection)
    {
        i_Size = 1;

        i_Direction = i_newDirection;

        v2_Head = new(9, 9);
    }

    #region Encapsulation
    public int GetSizeSnake() => i_Size;
    public int GetDirection() => i_Direction;
    public Vector2 GetHead() => v2_Head;
    #endregion

    #region Managing Snake Size
    public void IncreaseSize(int i_NbIncrement) => i_Size += i_NbIncrement;
    public void DecreaseSize(int i_NbRemove) => i_Size -= i_NbRemove;
    #endregion

    public void ChangeDirection(int i_newDirection)
    {
        // First we check if the new direction is the same as the current direction of the snake
        if (i_Direction != i_newDirection)
        {
            // Now, we will check if the new direction is different than the opposition direction of the snake's current direction
            int i_DirectionOpposite = (i_Direction + 2) % 4;

            if (i_DirectionOpposite == 0)
                i_DirectionOpposite = 4;

            if (i_newDirection != i_DirectionOpposite)
                i_Direction = i_newDirection;
        }
    }

    public void UpdateSnakeMovement(Board localBoard)
    {
        b_CanMove = GenericFunction.UpdateTimer(ref f_timerMove);

        if (b_CanMove)
        {
            Vector2 v2_newPositionHead = v2_Head;

            GenericFunction.ChangePosition(ref v2_newPositionHead, i_Direction);

            if (localBoard.CheckCollision(v2_newPositionHead, true))
                UpdateSnakePosition(v2_newPositionHead, localBoard);

            b_CanMove = false;
        }
    }

    private void UpdateSnakePosition(Vector2 v2_newHeadPosition, Board localBoard)
    {
        // First we update the previous position
        localBoard.UpdateSnakePosition(v2_newHeadPosition, i_Size);
        localBoard.UpdateSnakePosition(v2_Head, i_Size);

        if (i_Size == 2)
        {
            // Condition to avoid having a bad behavior the first time we pass from a Snake size of 1 to 2
            if (v2_Tail != new Vector2(-1, -1))
                localBoard.UpdateSnakePosition(v2_Tail, i_Size);

            v2_Tail = v2_Head;
        }
        else if (i_Size > 2)
        {
            if (v2_Bodys.Count < (i_Size - 2))
            {
                v2_Bodys.Add(v2_Head);

                // We have to create this condition to update the tail when we switch from 2 to 3 on the size of the snake
                if (i_Size == 3)
                    localBoard.UpdateSnakePosition(v2_Tail, i_Size);
            }
            else
            {
                v2_Bodys.Add(v2_Head);

                // Now we update the Tail position on the board
                localBoard.UpdateSnakePosition(v2_Bodys[0], i_Size);
                localBoard.UpdateSnakePosition(v2_Tail, i_Size);

                // Now we update the Tail Vector we the new position of the last Body part
                v2_Tail = v2_Bodys[0];

                // Finally, we remove the part of the body that has been added on the tail
                v2_Bodys.RemoveAt(0);
            }
        }

        // New position become the Head
        v2_Head = v2_newHeadPosition;
    }
}