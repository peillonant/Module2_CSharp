using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

public class Snake
{
    private int i_Size;
    private int i_Direction;                         // 1 = Top, 2 = Right, 3 = Bottom, 4 = Left   
    private float f_timerMove;
    private bool b_CanMove;

    private Board localBoard;

    private Vector2 v2_Head = new();
    private List<Vector2> v2_Bodys = new();
    private Vector2 v2_Tail = new(-1, -1);

    public event Action? CollisionExtend;

    public Snake(int i_newDirection, Board board)
    {
        i_Size = 1;

        i_Direction = i_newDirection;

        v2_Head = new(9, 9);

        localBoard = board;
    }

    #region Encapsulation
    public int GetSizeSnake() => i_Size;
    public int GetDirection() => i_Direction;
    public Vector2 GetHead() => v2_Head;
    #endregion

    #region Managing Snake Size
    public void IncreaseSize(int i_NbIncrement)
    {
        if (i_NbIncrement > 1)
            UpdateBoardExtend(i_NbIncrement);

        i_Size += i_NbIncrement;
    }
    public void DecreaseSize(int i_NbRemove)
    {
        for (int i = i_NbRemove; i > 0 ; i--)
        {
            if (i_Size - i >= 1)
            {
                i_Size -= i;
                UpdateBoardShrink(i);
                return;
            }
        }
    }

    // Remove all part of the body that has been removed 
    private void UpdateBoardShrink(int i_NbRemove)
    {
        // Remove Tail first
        localBoard.RemoveObject(v2_Tail);

        if (i_NbRemove == 1)
        {
            // Now check if we add a new tail
            if (v2_Bodys.Count > 0)
            {
                v2_Tail = v2_Bodys.Last();
                v2_Bodys.Remove(v2_Tail);
                localBoard.AddObject(v2_Tail, TypeCell.OwnBodyTail);
            }
            else
                v2_Tail = new(-1,-1);
        }
        else if (i_NbRemove == 2)
        {
            if (v2_Bodys.Count == 1)
            {
                // First we remove the first last element of body from the list and from the board
                localBoard.RemoveObject(v2_Bodys.Last());
                v2_Bodys.Remove(v2_Bodys.Last());
                
                // Then we remove the Tail
                v2_Tail = new(-1,-1);
            }
            else
            {
                // First we remove the first last element of body from the list and from the board
                localBoard.RemoveObject(v2_Bodys.Last());
                v2_Bodys.Remove(v2_Bodys.Last());

                // Second, we pass the last element of the body to the tail
                v2_Tail = v2_Bodys.Last();
                v2_Bodys.Remove(v2_Tail);
                localBoard.AddObject(v2_Tail, TypeCell.OwnBodyTail);
            }
            
        }
    }
    
    // Add part of the body that has been extend
    private void UpdateBoardExtend(int i_NbExtend)
    {
        // first we add the Extend on the body
        for (int i = 0; i < i_NbExtend; i++)
        {
            Vector2 v2_newPosBody;
            int i_DirectionOpposite;

            for (int j = 0; j < 3; j++ )
            {
                // take the opposite of the current Direction to put the extend behind the head
                i_DirectionOpposite = ((i_Direction + 2) % 4 == 0) ? 4 : (i_Direction + 2) % 4;
                
                if (j == 1)
                    i_DirectionOpposite = ((i_Direction + 1) % 4 == 0) ? 4 : (i_Direction + 2) % 4;
                else if (j == 2)
                    i_DirectionOpposite = ((i_Direction - 1) % 4 == 0) ? 4 : (i_Direction + 2) % 4;

                // We compute the position of the new body part
                if (v2_Tail != new Vector2(-1, -1))
                {
                    v2_newPosBody = v2_Tail;
                    v2_Bodys.Add(v2_Tail);
                    localBoard.AddObject(v2_Tail, TypeCell.OwnBodyBody);
                }
                else
                    v2_newPosBody = v2_Head;
                
                GenericFunction.ChangePosition(ref v2_newPosBody, i_DirectionOpposite);

                // Check if this new Position is avalaible on the board
                if (localBoard.CheckCollision(v2_newPosBody, false))
                {
                    v2_Tail = v2_newPosBody;
                    localBoard.AddObject(v2_Tail, TypeCell.OwnBodyTail);
                    break;
                }
                else if (j == 2)
                    CollisionExtend?.Invoke();
            }
        }
    } 
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

    public void UpdateSnakeMovement()
    {
        b_CanMove = GenericFunction.UpdateTimer(ref f_timerMove);

        if (b_CanMove)
        {
            Vector2 v2_newPositionHead = v2_Head;

            GenericFunction.ChangePosition(ref v2_newPositionHead, i_Direction);

            if (localBoard.CheckCollision(v2_newPositionHead, true))
                UpdateSnakePosition(v2_newPositionHead);

            b_CanMove = false;
        }
    }

    private void UpdateSnakePosition(Vector2 v2_newHeadPosition)
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