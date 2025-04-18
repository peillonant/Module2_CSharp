using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

public class ViewBoard
{
    #region Variable
    private readonly Board targetBoards;
    private readonly Snake targetSnake;
    private ViewCell? currentViewSnakeHead, currentViewSnakeTail;
    private readonly ViewCell[,] currentViewBoards = new ViewCell[GameInfo.i_nbCol, GameInfo.i_nbLin];
    private readonly ViewCell[,] tmpViewBoards = new ViewCell[GameInfo.i_nbCol, GameInfo.i_nbLin];
    private const float f_speedAnimation = 0.17f;
    private const float f_limitAnimation = 0.5f;
    #endregion

    public ViewCell GetViewCell(Vector2 v2_Position) => currentViewBoards[(int) v2_Position.X, (int) v2_Position.Y];

    public ViewBoard(Board targetBoards, Snake targetSnake)
    {
        this.targetBoards = targetBoards;
        this.targetSnake = targetSnake;

        InitCurrentBoards();
    }

    private void InitCurrentBoards()
    {
        for (int col = 0; col < GameInfo.i_nbCol; col++)
        {
            for (int lin = 0; lin < GameInfo.i_nbLin; lin++)
            {
                Cell currentCell = targetBoards.GetCellFromBoard(new(col,lin));
                currentViewBoards[col, lin] = new ViewCell(currentCell.GetCellPosition(), currentCell.GetTypeCell());
                tmpViewBoards[col, lin] = new ViewCell( new(col, lin), TypeCell.None);
            }
        }

        currentViewSnakeHead = new (targetSnake.GetSnakeHead(), TypeCell.OwnBodyHead );
        currentViewSnakeTail = new (targetSnake.GetSnakeTail(), TypeCell.OwnBodyTail );
    }

    public void UpdateViewBoard(Action? callBack = null)
    {
        ResetTMPViewBoards();
        
        AnimationSnakeHead();
        
        AnimationSnakeTail();
        
        TransferTMPtoCurrent();

        callBack?.Invoke();
    }

    // Method to reset the TMPViewBoards before the update
    private void  ResetTMPViewBoards()
    {
        for (int col = 0; col < GameInfo.i_nbCol; col++)
        {
            for (int lin = 0; lin < GameInfo.i_nbLin; lin++)
            {
                TypeCell targetTypeCell = targetBoards.GetCellFromBoard(new (col, lin)).GetTypeCell();

                if (targetTypeCell == TypeCell.OwnBodyHead)
                    tmpViewBoards[col, lin] = new ViewCell( new(col, lin), TypeCell.None);
                else
                    tmpViewBoards[col, lin] = new ViewCell( new(col, lin), targetTypeCell);
                
                if (targetTypeCell == TypeCell.Bonus || targetTypeCell == TypeCell.Malus || targetTypeCell == TypeCell.Bomb)
                {
                    targetBoards.GetCellFromBoard(new (col, lin)).UpdateCellTimer(Raylib.GetFrameTime());
                    tmpViewBoards[col, lin].SetCellTimer(targetBoards.GetCellFromBoard(new (col, lin)).GetCellTimer());
                }
            }
        }
    }

    // Method called at the end of the Update ViewBoard to transfer all new element to CurrentViewBoard
    private void TransferTMPtoCurrent()
    {
        for (int col = 0; col < GameInfo.i_nbCol; col++)
        {
            for (int lin = 0; lin < GameInfo.i_nbLin; lin++)
            {
                currentViewBoards[col,lin] = tmpViewBoards[col, lin];
            }
        }
    }

    private void AnimationSnakeHead()
    {
        // First we check if we are able to display every update the head
        if (currentViewSnakeHead == null) return;

        // the currentCell on the viewBoard did not have the same position as the position of the TargetBoard
        if (currentViewSnakeHead.GetCellPosition() != targetSnake.GetSnakeHead())
        {
            Vector2 v2_PositionToWorld = currentViewSnakeHead.GetCellPosition() * GameInfo.i_SizeCell;

            Vector2 v2_Direction = targetSnake.GetSnakeHead() - currentViewSnakeHead.GetCellPosition();

            v2_PositionToWorld -= GameInfo.i_SizeCell / 7.5f * v2_Direction;

            currentViewSnakeHead = new (targetSnake.GetSnakeHead(), TypeCell.OwnBodyHead );
            currentViewSnakeHead.SetPositionToWorld(v2_PositionToWorld);
            
            tmpViewBoards[(int) targetSnake.GetSnakeHead().X, (int) targetSnake.GetSnakeHead().Y] = currentViewSnakeHead;
        }
        else if (currentViewSnakeHead.GetCellPosition() == targetSnake.GetSnakeHead())
        {
            // Update the WorldPosition of the currentViewSnakeHead to have the movement to the target

            Vector2 v2_PositionToWorld = Tweening.LerpVector2(currentViewSnakeHead.GetPositionToWorld(), targetSnake.GetSnakeHead() * GameInfo.i_SizeCell, f_speedAnimation);

            if (Vector2.Distance(v2_PositionToWorld, targetSnake.GetSnakeHead() * GameInfo.i_SizeCell) < f_limitAnimation)
                v2_PositionToWorld = targetSnake.GetSnakeHead() * GameInfo.i_SizeCell;

            currentViewSnakeHead.SetPositionToWorld(v2_PositionToWorld);

            tmpViewBoards[(int) currentViewSnakeHead.GetCellPosition().X, (int) currentViewSnakeHead.GetCellPosition().Y] = currentViewSnakeHead;
        }
    }

    private void AnimationSnakeTail()
    {
        // First we check if we are able to display every update the tail
        if (currentViewSnakeTail == null || targetSnake.GetSizeSnake() == 1) return;

        ViewCell targetViewSnakeTail = new (targetSnake.GetSnakeTail(), TypeCell.OwnBodyTail);

        if (currentViewSnakeTail.GetPositionToWorld() != targetViewSnakeTail.GetPositionToWorld())
        {
            // if the size of the snake is above 2, that means we have to add a body on the target cell to fake the movement of the tail into the body
            if (targetSnake.GetSizeSnake() > 2)
                tmpViewBoards[(int) targetViewSnakeTail.GetCellPosition().X, (int) targetViewSnakeTail.GetCellPosition().Y].UpdateCell(TypeCell.OwnBodyBody);

            Vector2 v2_PositionToWorld = Tweening.LerpVector2(currentViewSnakeTail.GetPositionToWorld(), targetSnake.GetSnakeTail() * GameInfo.i_SizeCell, f_speedAnimation);

            if (Vector2.Distance(v2_PositionToWorld, targetSnake.GetSnakeTail() * GameInfo.i_SizeCell) < f_limitAnimation * 5)
                v2_PositionToWorld = targetSnake.GetSnakeTail() * GameInfo.i_SizeCell;

            currentViewSnakeTail.SetPositionToWorld(v2_PositionToWorld);
        }
        else
            currentViewSnakeTail = targetViewSnakeTail;

        tmpViewBoards[(int) currentViewSnakeTail.GetCellPosition().X, (int) currentViewSnakeTail.GetCellPosition().Y] = currentViewSnakeTail;

    }
}