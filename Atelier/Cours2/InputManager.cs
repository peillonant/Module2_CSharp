using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

public class InputManager
{
    GameManager gameManager;

    public InputManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void UpdateInput()
    {
        SpeedManager();

        if (gameManager.b_IsPaused)
            MouseListener();
    }

    private void SpeedManager()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Space) || Raylib.IsKeyPressed(KeyboardKey.P))
        {    
            gameManager.b_IsPaused = !gameManager.b_IsPaused;
            gameManager.i_SpeedIncrement = 1;
        }
        
        if (Raylib.IsKeyPressed(KeyboardKey.One))
        {
            gameManager.b_IsPaused = false;
            gameManager.i_SpeedIncrement = 1;
        }    

        if (Raylib.IsKeyPressed(KeyboardKey.Two))
        {
            gameManager.b_IsPaused = false;
            gameManager.i_SpeedIncrement = 2;
        }  

        if (Raylib.IsKeyPressed(KeyboardKey.Three))
        {
            gameManager.b_IsPaused = false;
            gameManager.i_SpeedIncrement = 3;
        }

        if (Raylib.IsKeyPressed(KeyboardKey.Left))
        {
            if (gameManager.i_SpeedIncrement > 1)
                gameManager.i_SpeedIncrement--;
            
            gameManager.b_IsPaused = false;
        }

        if (Raylib.IsKeyPressed(KeyboardKey.Right))
        {
            if (gameManager.i_SpeedIncrement < 3)
                gameManager.i_SpeedIncrement++;
            
            gameManager.b_IsPaused = false;
        }
    }

    private void MouseListener()
    {
        int i_BorderLeftBoard, i_BorderRightBoard, i_BorderTopBoard, i_BoardBottomBoard;

        i_BorderLeftBoard = Raylib.GetScreenWidth() / 2 - gameManager.board.i_nbCellWidth * gameManager.board.i_cellWidth / 2;
        i_BorderRightBoard = Raylib.GetScreenWidth() / 2 + gameManager.board.i_nbCellWidth * gameManager.board.i_cellWidth / 2;
        
        i_BorderTopBoard = Raylib.GetScreenHeight() / 2 - gameManager.board.i_nbCellHeight * gameManager.board.i_cellHeight / 2;
        i_BoardBottomBoard = Raylib.GetScreenHeight() / 2 + gameManager.board.i_nbCellHeight * gameManager.board.i_cellHeight / 2;

        // Retrieve the position of the Mouse and find the coordinate on the board
        Vector2 v2_MousePosition = Raylib.GetMousePosition();
        
        if (v2_MousePosition.X > i_BorderLeftBoard && v2_MousePosition.X < i_BorderRightBoard &&
            v2_MousePosition.Y > i_BorderTopBoard &&  v2_MousePosition.Y < i_BoardBottomBoard)
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                int tmpPX = (int) v2_MousePosition.X - i_BorderLeftBoard;
                int tmpPY = (int) v2_MousePosition.Y - i_BorderTopBoard;

                tmpPX = tmpPX / gameManager.board.i_cellWidth;
                tmpPY = tmpPY / gameManager.board.i_cellHeight;

                gameManager.board.i_TabBoard[tmpPX, tmpPY] = ( gameManager.board.i_TabBoard[tmpPX, tmpPY] == 0) ? 1 : 0;
            }
        }        
    }
}