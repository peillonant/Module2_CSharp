using System.Numerics;
using Raylib_cs;

public class UI_Board
{
    private Board board;
    private GameManager gameManager;

    public UI_Board(Board originBoard, GameManager gameManager)
    {
        board = originBoard;
        this.gameManager = gameManager;
    }

    public void DrawUI()
    {
        DrawBoard();

        DrawIndicateSpeed();
    }

    private void DrawBoard()
    {
        // Display the floor of the board
        int i_PX = Raylib.GetScreenWidth() / 2 - board.i_nbCellWidth * board.i_cellWidth / 2;
        int i_PY = Raylib.GetScreenHeight() / 2 - board.i_nbCellHeight * board.i_cellHeight / 2;
        int tmpX = board.i_nbCellWidth * board.i_cellWidth;
        int tmpY = board.i_nbCellHeight * board.i_cellHeight;

        Rectangle recBoard = new(i_PX, i_PY, tmpX, tmpY);
        Raylib.DrawRectangleLinesEx(recBoard, 1f, Color.Black);

        for (int i = 0 ; i < board.i_nbCellHeight; i++)
        {
            for (int j = 0 ; j < board.i_nbCellWidth; j++)
            {
                tmpX = i_PX + ( j * board.i_cellWidth);
                tmpY = i_PY + ( i * board.i_cellHeight);

                Rectangle recCell = new(tmpX, tmpY, board.i_cellWidth, board.i_cellHeight);

                Raylib.DrawRectangleLinesEx(recCell, 0.5f, Color.Black);

                // Then display the Entity if the cell is above 0
                if (board.i_TabBoard[j,i] > 0)
                    Raylib.DrawRectangleRec(recCell, Color.Black);
            }
        }
    }

    
    private void DrawIndicateSpeed()
    {
        string textDisplay;
        int i_Font = 20;
        int i_PX , i_PY;

        if (gameManager.b_IsPaused)
            textDisplay = "Pause";
        else
            textDisplay = "x " + gameManager.i_SpeedIncrement;

        Vector2 v2_TextSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), textDisplay , i_Font, 1);
        
        i_PX = Raylib.GetScreenWidth() / 2;
        i_PY = 15;

        i_PX -= (int) v2_TextSize.X / 2;
        i_PY -= (int) v2_TextSize.Y / 2;

        Raylib.DrawText(textDisplay, i_PX, i_PY, i_Font, Color.Black);
    }
}