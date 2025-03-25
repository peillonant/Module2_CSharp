using System.Numerics;
using Raylib_cs;

public class UI_Board_Sprite
{
    public Dictionary<int, Color> colorCell = new Dictionary<int, Color>();

    public UI_Board_Sprite()
    {
        colorCell.Add(1, Color.DarkBlue);       // Head of the snake
        colorCell.Add(2, Color.Blue);           // Body of the snake
        colorCell.Add(3, Color.SkyBlue);        // Tail of the snake
        colorCell.Add(10, Color.Red);           // Apple
        colorCell.Add(11, Color.Orange);        // Boxe
        colorCell.Add(20, Color.Brown);         // Malus object 
        colorCell.Add(21, Color.Black);         // Collision object
    }
}

public class UI_Board
{
    #region Variable
    private Board board;
    private const int i_Width = 8;
    private const int i_Height = 8;
    private int i_Zoom = 1;
    private int i_PX;
    private int i_PY;
    private UI_Board_Sprite ui_Board_Sprite = new();
    #endregion

    private readonly List<float> ratioCols_8 = [0.01f, 0.16f, 0.70f, 0.85f];
    private readonly List<float> ratioLins_8 = [0.03f, 0.25f, 0.50f, 0.72f];

    public UI_Board(ref Board newBoard)
    {
        board = newBoard;

        if (board.IsPlayer())
            UpdatePositionBoard(-1);
    }

    // Update the Position Board at the initiation for the Player and everytime we display a Enemy Board when it's display
    public void UpdatePositionBoard(int i_Position)
    {
        if (board.IsPlayer())
        {
            i_Zoom = 2;
            i_PX = Raylib.GetScreenWidth() / 2 - (GameInfo.Instance.GetNbCol() / 2 * i_Width * i_Zoom);
            i_PY = Raylib.GetScreenHeight() / 2 - (GameInfo.Instance.GetNbLin() / 2 * i_Height * i_Zoom);
        }
        else
        {
            if (GameInfo.Instance.GetNbPlayerAlive() <= 3)  // Display 2 Enemies (1 each side)
            {
                // TO DO
            }
            else if (GameInfo.Instance.GetNbPlayerAlive() <= 9) // Display 8 Enemies (4 each side, quincunx pattern)
            {
                int tmpIndexCol;
                int tmpIndexLin = i_Position % 4;

                if (i_Position < 4)
                    tmpIndexCol = (i_Position % 2 == 0) ? 1 : 0;
                else
                    tmpIndexCol = (i_Position % 2 == 0) ? 2 : 3;

                //Console.WriteLine($"tmpIndexLin = {tmpIndexLin} and tmpIndexCol = {tmpIndexCol} for the Bot at the Position {i_Position}");

                i_PX = (int) (Raylib.GetScreenWidth() * ratioCols_8[tmpIndexCol]);
                i_PY = (int) (Raylib.GetScreenHeight() * ratioLins_8[tmpIndexLin]);
            }   
            else  // Display 16 Enemies (8 each side, with a 2 column of 4 lines)
            {
                // TO DO
            }
        }
    }

    public void DrawBoard()
    {
        DisplayBoardOutline();

        for (int i=0 ; i < GameInfo.Instance.GetNbCol(); i++)
        {
            for (int j =0 ; j < GameInfo.Instance.GetNbLin(); j++)
            {
                // Display the floor of the board
                int tmpWidth = i_Width * i_Zoom;
                int tmpHeight = i_Height * i_Zoom;
                int tmpX = i_PX + ( i * tmpWidth);
                int tmpY = i_PY + ( j * tmpHeight);
                Vector2 v2_Pos = new (i,j);

                Rectangle recCell = new(tmpX, tmpY, tmpWidth, tmpHeight);

                // Then display the Entity if the cell is above 0
                if (board.GetValueBoard(v2_Pos) > 0)
                    Raylib.DrawRectangleRec(recCell, ui_Board_Sprite.colorCell[board.GetValueBoard(v2_Pos)]);
            }
        }
    }

    private void DisplayBoardOutline()
    {
        // Display the floor of the board
        int tmpWidth = i_Width * GameInfo.Instance.GetNbCol() * i_Zoom;
        int tmpHeight = i_Height * GameInfo.Instance.GetNbLin() * i_Zoom;
        Color colorOutline;
        Rectangle recBoard = new(i_PX, i_PY, tmpWidth, tmpHeight);

        if (board.IsPlayer())
            colorOutline = Color.Black;
        else
            colorOutline = Color.Red;
        
        Raylib.DrawRectangleLinesEx(recBoard, 1f, colorOutline);
    }

    public void DrawInfo(Character origine)
    {        
        DrawTimer(origine.GetTimer().GetTimerLife().ToString());
        DrawPosition(origine.GetRanking().ToString());
    }

    private void DrawTimer(string textTimer)
    {
        int i_Font = 20 * i_Zoom;
        int tmpX , tmpY;
        Color colorFont;
        Vector2 v2_TextSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), textTimer , i_Font, 1);

        if (board.IsPlayer())
        {
            tmpX = (int) Raylib.GetScreenWidth() / 2;
            tmpY = (int)(Raylib.GetScreenHeight() * 0.8f);
            
            colorFont = Color.LightGray;
        }   
        else
        {            
            tmpX =  (int) (i_PX + (GameInfo.Instance.GetNbCol() / 2 * i_Width));
            tmpY =  (int) (i_PY + (GameInfo.Instance.GetNbLin() / 2 * i_Height));

            colorFont = Color.Black;
        }

        tmpX -= (int) v2_TextSize.X / 2;
        tmpY -= (int) v2_TextSize.Y / 2;

        Raylib.DrawText(textTimer, tmpX, tmpY, i_Font, colorFont);
    }

    private void DrawPosition(string textPosition)
    {
        int i_Font = 20 * i_Zoom;
        int tmpX , tmpY;
        Color colorFont;

        if (board.IsPlayer())
        {
            textPosition += " / " + GameInfo.Instance.GetNbPlayerTotal().ToString(); 

            tmpX = (int) Raylib.GetScreenWidth() / 2;
            tmpY = (int) (Raylib.GetScreenHeight() * 0.2f);
            
            colorFont = Color.LightGray;
        }   
        else
        {            
            tmpX =  (int) (i_PX + (GameInfo.Instance.GetNbCol() / 2 * i_Width));
            tmpY =  (int) (i_PY + GameInfo.Instance.GetNbLin() / 2 * i_Height / 2);

            colorFont = Color.Black;
        }

        Vector2 v2_TextSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), textPosition , i_Font, 1);

        tmpX -= (int) v2_TextSize.X / 2;
        tmpY -= (int) v2_TextSize.Y / 2;

        Raylib.DrawText(textPosition, tmpX, tmpY, i_Font, colorFont);
    }
}