using System.Diagnostics;
using System.Numerics;

public class Snake
{
    private int i_Size;
    private int i_Direction;                         // 1 = Top, 2 = Right, 3 = Bottom, 4 = Left   
    private float f_timerMove;
    private bool b_CanMove;

    private Board localBoard;
    private Queue<Vector2> snakeBody = new();

    public event Action? CollisionExtend;

    public Snake(int i_newDirection, Board board, Action Callback)
    {
        i_Size = 3;
        i_Direction = i_newDirection;
        localBoard = board;
        CreateSnakeBody(Callback);
    }

    #region Encapsulation
    public int GetSizeSnake() => i_Size;
    public int GetDirection() => i_Direction;
    public Vector2 GetSnakeHead() => snakeBody.Last();
    public Vector2 GetSnakeTail() => snakeBody.Peek();
    public Queue<Vector2> GetSnakeBody() => snakeBody;
    #endregion

    #region Managing Snake Size
    public void DecreaseSize(int i_NbRemove)
    {
        for (int i = i_NbRemove; i > 0 ; i--)
        {
            if (i_Size - i >= 1)
            {
                i_Size -= i;
                UpdateBodyShrink(i);
                break;
            }
        }
    }
    public void IncreaseSize(int i_NbIncrement)
    {
        if (i_NbIncrement > 1)
            UpdateBodyExtend(i_NbIncrement);
        i_Size += i_NbIncrement;
    }

    // Remove all part of the body that has been removed 
    private void UpdateBodyShrink(int i_NbRemove)
    {
        for (int i = 0; i < i_NbRemove; i++)
        {
            localBoard.RemoveObject(snakeBody.Peek());
            snakeBody.Dequeue();
        }
    }
    
    // Add part of the body that has been extend
    private void UpdateBodyExtend(int i_NbExtend)
    {
        // first we add the Extend on the body
        for (int i = 0; i < i_NbExtend; i++)
        {
            Vector2 v2_newPosBody;
            int i_DirectionOpposite = 1;

            for (int j = 0; j < 3; j++ )
            {
                // take the opposite of the current Direction to put the extend behind the tail
                if (j == 0)
                    i_DirectionOpposite = ((i_Direction + 2) % 4 == 0) ? 4 : (i_Direction + 2) % 4;            
                else if (j == 1)         // Take the side on the Right
                    i_DirectionOpposite = ((i_Direction + 1) % 4 == 0) ? 4 : (i_Direction + 1) % 4;
                else if (j == 2)    // Take the size on the Right
                    i_DirectionOpposite = ((i_Direction - 1) % 4 == 0) ? 4 : (i_Direction - 1) % 4;

                v2_newPosBody = snakeBody.Peek();
                v2_newPosBody = GenericFunction.ChangePosition(v2_newPosBody, i_DirectionOpposite);
                
                if (localBoard.CheckCollision(v2_newPosBody, false))
                {
                    if (snakeBody.Count > 1)
                        localBoard.AddObject(snakeBody.Peek(), TypeCell.OwnBodyBody);
                    
                    // Last position now
                    localBoard.AddObject(v2_newPosBody, TypeCell.OwnBodyTail);

                    AddSegmentEndBody(v2_newPosBody);
                    
                    break;
                }
                else if (j == 2)
                    CollisionExtend?.Invoke();
            }
        }
    }

    // Method to add a segment to the snake at the end o
    public void AddSegmentEndBody(Vector2 v2_newPosition)
    {
        List<Vector2> tmpSnakeBody = new List<Vector2>(snakeBody.Reverse());
        tmpSnakeBody.Add(v2_newPosition);
        tmpSnakeBody.Reverse();
        snakeBody = new Queue<Vector2>(tmpSnakeBody);
    }
    #endregion

    // Create the body of the snake at the beginning of the game (3 segments at the start for each character)
    private void CreateSnakeBody(Action Callback)
    {
        Vector2 startPosition = new(9,9);
    
        for (int i = i_Size; i > 0; i--)
        {
            if (i < i_Size)
                startPosition = GenericFunction.ChangePosition(startPosition, i_Direction);

            snakeBody.Enqueue(startPosition);
            localBoard.AddObject(startPosition, (TypeCell) i);
        }

        Callback?.Invoke();
    }

    // Method to Change Direction of the Snake
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

    // Method call by the Character Update to move the snake regarding the direction and the speed of the game
    public void UpdateSnakeMovement()
    {
        b_CanMove = GenericFunction.UpdateTimer(ref f_timerMove);

        if (b_CanMove)
        {
            Vector2 v2_newPositionHead = snakeBody.Last();

            v2_newPositionHead = GenericFunction.ChangePosition(v2_newPositionHead, i_Direction);

            if (localBoard.CheckCollision(v2_newPositionHead, true))
                UpdateSnakePosition(v2_newPositionHead);

            b_CanMove = false;
        }
    }

    // Method that update the position of the Snake
    private void UpdateSnakePosition(Vector2 v2_newHeadPosition)
    {
        // First we update the new position
        localBoard.UpdateSnakePosition(v2_newHeadPosition, i_Size);

        // Now we update the previous head position
        localBoard.UpdateSnakePosition(snakeBody.Last(), i_Size);

        // Now we add the new position to the Snake Body
        snakeBody.Enqueue(v2_newHeadPosition);

        if (i_Size == 1)
            snakeBody.Dequeue();
        
        // Check if after adding the new position on the body we have more segment than the size requiered. Then remove it
        else if (i_Size < snakeBody.Count)
        {
            // Remove from the board and from the body the tail 
            localBoard.UpdateSnakePosition(snakeBody.Peek(), i_Size);
            snakeBody.Dequeue();

            // Update the last body part on the board into a Tail
            if (i_Size > 2)     // Condition added to not update the body when the size of the snake is equal to 2
                localBoard.UpdateSnakePosition(snakeBody.Peek(), i_Size);
        }   
    }
}