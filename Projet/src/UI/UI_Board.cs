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
        colorCell.Add(4, Color.Orange);        // Boxe
        colorCell.Add(5, Color.Red);           // Apple
        colorCell.Add(6, Color.Brown);         // Malus object 
        colorCell.Add(7, Color.Black);         // Collision object
    }
}

public class UI_Board
{
    #region Variable
    private readonly Character characterOrigin;
    private readonly UI_Board_Sprite ui_Board_Sprite = new();
    #endregion

    public UI_Board(Character characterOrigin)
    {
        this.characterOrigin = characterOrigin;
    }

    public void DrawBoard()
    {
        // Method linked to Board
        DisplayBoardOutline();
        DisplayGrid();

        // Method linked to Info
        DrawTimer();
        DrawPosition();
        if (characterOrigin.IsPlayer())
        {
            DrawNbPlayerRemaining();
        }

    }

    private void DisplayBoardOutline()
    {
        // Display the floor of the board
        int tmpWidth = (int)(GameInfo.i_SizeCell * GameInfo.i_nbCol * characterOrigin.GetZoom());
        int tmpHeight = (int)(GameInfo.i_SizeCell * GameInfo.i_nbLin * characterOrigin.GetZoom());
        Color colorOutline;
        Rectangle recBoard = new(characterOrigin.GetPosition().X, characterOrigin.GetPosition().Y, tmpWidth, tmpHeight);

        colorOutline = characterOrigin.IsPlayer() ? Color.Black : Color.Red;

        Raylib.DrawRectangleLinesEx(recBoard, 1f, colorOutline);
    }

    private void DisplayGrid()
    {
        if (characterOrigin.IsAlive())
        {
            for (int i = 0; i < GameInfo.i_nbCol; i++)
            {
                for (int j = 0; j < GameInfo.i_nbLin; j++)
                {
                    // Display the floor of the board
                    float f_Zoom = characterOrigin.GetZoom();
                    int tmpWidth = (int)(GameInfo.i_SizeCell * f_Zoom);
                    int tmpHeight = (int)(GameInfo.i_SizeCell * f_Zoom);
                    int tmpX = (int)(characterOrigin.GetPosition().X + (i * tmpWidth));
                    int tmpY = (int)(characterOrigin.GetPosition().Y + (j * tmpHeight));

                    Rectangle recCell = new(tmpX, tmpY, tmpWidth, tmpHeight);

                    Vector2 v2_Pos = new(i, j);

                    // Then display the Entity if the cell is above 0
                    if ((int)characterOrigin.GetBoard().GetValueBoard(v2_Pos) > 0 && characterOrigin.GetBoard().GetValueBoard(v2_Pos) != TypeCell.Border)
                    {
                        int i_IndexColor = (int) characterOrigin.GetBoard().GetValueBoard(v2_Pos);
                        Raylib.DrawRectangleRec(recCell, ui_Board_Sprite.colorCell[i_IndexColor]);
                    }
                }
            }
        }
    }

    private void DrawTimer()
    {
        if (characterOrigin.IsAlive())
        {
            string s_TextTimer = characterOrigin.GetTimer().GetTimerLife().ToString();
            int i_Font = (int)(20 * characterOrigin.GetZoom());
            int tmpX, tmpY;
            Color colorFont;
            Vector2 v2_TextSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), s_TextTimer, i_Font, 1);

            if (characterOrigin.IsPlayer())
            {
                tmpX = (int)Raylib.GetScreenWidth() / 2;
                tmpY = (int)(Raylib.GetScreenHeight() * 0.8f);

                colorFont = Color.LightGray;
            }
            else
            {
                tmpX = (int)(characterOrigin.GetPosition().X + (GameInfo.i_nbCol / 2 * GameInfo.i_SizeCell));
                tmpY = (int)(characterOrigin.GetPosition().Y + (GameInfo.i_nbLin / 2 * GameInfo.i_SizeCell));

                colorFont = Color.Black;
            }

            tmpX -= (int)v2_TextSize.X / 2;
            tmpY -= (int)v2_TextSize.Y / 2;

            Raylib.DrawText(s_TextTimer, tmpX, tmpY, i_Font, colorFont);
        }
    }

    private void DrawPosition()
    {

        string s_TextPosition = characterOrigin.GetRanking().ToString();
        int i_Font = (int)(20 * characterOrigin.GetZoom());
        int tmpX, tmpY;
        Color colorFont;

        if (characterOrigin.IsPlayer())
        {
            s_TextPosition += " / " + GameInfo.GetNbCharacterTotal().ToString();

            tmpX = (int)Raylib.GetScreenWidth() / 2;
            tmpY = (int)(Raylib.GetScreenHeight() * 0.2f);

            colorFont = Color.LightGray;
        }
        else
        {
            tmpX = (int)(characterOrigin.GetPosition().X + (GameInfo.i_nbCol / 2 * GameInfo.i_SizeCell));
            tmpY = characterOrigin.IsAlive() ? (int)(characterOrigin.GetPosition().Y + GameInfo.i_nbCol / 2 * GameInfo.i_SizeCell / 2) : (int)(characterOrigin.GetPosition().Y + GameInfo.i_nbLin * GameInfo.i_SizeCell / 2);
            i_Font = characterOrigin.IsAlive() ? i_Font : (int)(i_Font * 1.5f);

            colorFont = characterOrigin.IsAlive() ? Color.Black : Color.Red;
        }

        Vector2 v2_TextSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), s_TextPosition, i_Font, 1);

        tmpX -= (int)v2_TextSize.X / 2;
        tmpY -= (int)v2_TextSize.Y / 2;

        Raylib.DrawText(s_TextPosition, tmpX, tmpY, i_Font, colorFont);
    }

    private void DrawNbPlayerRemaining()
    {
        string s_Text = "Player Alive";
        string s_TextNbPlayer = GameInfo.GetNbCharacterAlive().ToString();
        int i_Font = 30;
        int tmpX, tmpY;

        tmpX = (int)(Raylib.GetScreenWidth() / 2) - 150;
        tmpY = (int)(Raylib.GetScreenHeight() * 0.2f);

        Vector2 v2_TextSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), s_TextNbPlayer, i_Font, 1);

        tmpX -= (int)v2_TextSize.X / 2;
        tmpY -= (int)v2_TextSize.Y / 2;

        Raylib.DrawText(s_TextNbPlayer, tmpX, tmpY, i_Font, Color.LightGray);

        v2_TextSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), s_Text, i_Font, 1);

        tmpX -= (int)v2_TextSize.X / 2;
        tmpY -= (int)v2_TextSize.Y / 2;

        Raylib.DrawText(s_Text, tmpX, (int)(tmpY - v2_TextSize.Y), i_Font, Color.LightGray);
    }
}