using System.Numerics;
using Raylib_cs;

public static class UI_Board_Sprite
{
    static Dictionary<int, Color> colorCell = [];

    public static void InitUI_Board_Sprite()
    {
        colorCell.Add(1, Color.DarkBlue);           // Head of the snake
        colorCell.Add(2, Color.Blue);               // Body of the snake
        colorCell.Add(3, Color.SkyBlue);            // Tail of the snake
        colorCell.Add(4, Color.Red);                // Apple
        colorCell.Add(5, Color.Orange);             // Bonus
        colorCell.Add(6, Color.Brown);              // Malus object 
        colorCell.Add(7, Color.DarkPurple);         // Collision object
        colorCell.Add(8, Color.Black);              // New Board object
        colorCell.Add(9, Color.Gray);               // Bomb
    }

    public static Color GetColorCell (int indexCell)
    {
        return colorCell[indexCell];
    }
}

public static class UI_Board
{
    public static void DrawBoard(Character characterOrigin)
    {
        // Method linked to Board
        DisplayBoardOutline(characterOrigin);
        DisplayGrid(characterOrigin);

        // Method linked to Info
        DrawTimer(characterOrigin);
        DrawPosition(characterOrigin);
        if (characterOrigin.IsPlayer())
        {
            DrawNbPlayerRemaining();
        }
    }

    private static void DisplayBoardOutline(Character characterOrigin)
    {
        // Display the floor of the board
        int tmpWidth = (int)(GameInfo.i_SizeCell * GameInfo.i_nbCol * characterOrigin.GetZoom());
        int tmpHeight = (int)(GameInfo.i_SizeCell * GameInfo.i_nbLin * characterOrigin.GetZoom());
        Color colorOutline;
        Rectangle recBoard = new(characterOrigin.GetPosition().X, characterOrigin.GetPosition().Y, tmpWidth, tmpHeight);

        colorOutline = characterOrigin.IsPlayer() ? Color.Black : Color.Red;

        Raylib.DrawRectangleLinesEx(recBoard, 1f, colorOutline);
    }

    private static void DisplayGrid(Character characterOrigin)
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
                    if ((int)characterOrigin.GetBoard().GetValueBoard(v2_Pos) > 0)
                    {
                        int i_IndexColor = (int) characterOrigin.GetBoard().GetValueBoard(v2_Pos);
                        Raylib.DrawRectangleRec(recCell, UI_Board_Sprite.GetColorCell(i_IndexColor));
                    }
                }
            }
        }
    }

    private static void DrawTimer(Character characterOrigin)
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

    private static void DrawPosition(Character characterOrigin)
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

    private static void DrawNbPlayerRemaining()
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

    public static void DrawPowerDisplayed(Power? bonusDisplayed, Power? malusDisplayed)
    {
        // First display the square around the Power
        int i_Width = 100;
        int i_Height = 100;
        int i_PY = (int)(Raylib.GetScreenHeight() * 0.8f);;
        int i_PXBonus, i_PXMalus;
        Rectangle recPower;

        // Draw Rectangle for Bonus
        i_PXBonus = Raylib.GetScreenWidth() / 2 - 150 - (i_Width / 2);
        recPower = new(i_PXBonus, i_PY, i_Width, i_Height);
        Raylib.DrawRectangleLinesEx(recPower, 2f, UI_Board_Sprite.GetColorCell(5));

        // Draw Rectangle for Malus
        i_PXMalus = Raylib.GetScreenWidth() / 2 + 150 - (i_Width / 2);
        recPower = new(i_PXMalus, i_PY, i_Width, i_Height);
        Raylib.DrawRectangleLinesEx(recPower, 2f, UI_Board_Sprite.GetColorCell(6));
    
        // Then display the text in the square of the Bonus if it's available
        if (bonusDisplayed != null)
            DrawTextPower(bonusDisplayed, UI_Board_Sprite.GetColorCell(5), i_PXBonus, i_PY, i_Width, i_Height);

        // Then display the text in the square of the Malus if it's available
        if (malusDisplayed != null)
            DrawTextPower(malusDisplayed, UI_Board_Sprite.GetColorCell(6), i_PXMalus, i_PY, i_Width, i_Height);
    }   

    private static void DrawTextPower(Power powerDisplayed, Color colorFont, int i_PX, int i_PY, int i_Width, int i_Height)
    {
        int i_FontSize = 20;

        string s_Text = powerDisplayed.GetNamePower();

        Vector2 v2_TextSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), s_Text, i_FontSize, 1);
        int i_tmpPY;
        i_PX += (i_Width / 2) - (int)v2_TextSize.X / 2;
        i_tmpPY = i_PY + (i_Height / 2) - (int)v2_TextSize.Y / 2;

        Raylib.DrawText(s_Text, i_PX, i_tmpPY, i_FontSize, colorFont);
    }

    public static void DrawAppleEaten()
    {
        
    }
}