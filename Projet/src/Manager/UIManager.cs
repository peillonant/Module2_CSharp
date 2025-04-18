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

public static class UIManager
{
    public static void DrawBoard(Character characterOrigin)
    {
        
        // Method linked to Board
        UIGrid.DisplayBoardOutline(characterOrigin);
        UIGrid.DisplayGrid(characterOrigin);

        // Method linked to Info
        DrawTimer(characterOrigin);
        DrawPosition(characterOrigin);
        if (characterOrigin.IsPlayer())
        {
            DrawNbPlayerRemaining();
            UIPower.DrawPowerDisplayed(characterOrigin);
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
                tmpX = Raylib.GetScreenWidth() / 2;
                tmpY = (int)(Raylib.GetScreenHeight() * 0.9f);

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
            tmpY = characterOrigin.IsAlive() ? (int)(characterOrigin.GetPosition().Y + GameInfo.i_nbLin / 2 * GameInfo.i_SizeCell / 2) : (int)(characterOrigin.GetPosition().Y + GameInfo.i_nbLin * GameInfo.i_SizeCell / 2);
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
        string s_Text = "Alive";
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

    public static void DrawProgressApple(float f_ProgressApple)
    {
        int i_Width = Raylib.GetScreenWidth() / 3;
        int i_Height = 30;
        int i_PX = Raylib.GetScreenWidth() / 2;
        int i_PY = (int) (Raylib.GetScreenHeight() * 0.1f / 2);

        int i_RecPX = i_PX - i_Width / 2 ;
        int i_RecPY = i_PY - i_Height / 2;

        // We draw the rectanlge progress
        float f_WidthProgress = i_Width * f_ProgressApple;
        
        Rectangle rec_ProgressBar = new(i_RecPX, i_RecPY, f_WidthProgress, i_Height);
        Raylib.DrawRectangleRounded(rec_ProgressBar, 1, 1, Color.Green);

        // We draw the text in the middle of ProgressApple
        string s_TextApple = GameInfo.GetNbAppleEaten().ToString() + "/" + GameInfo.GetNbAppleNeeded().ToString();
        Vector2 v2_SizeText = Raylib.MeasureTextEx(Raylib.GetFontDefault(), s_TextApple, 20, 1);
        int i_TextPX = (int) (i_PX - (v2_SizeText.X / 2));
        int i_TextPY = (int) (i_PY - (v2_SizeText.Y / 2));

        Raylib.DrawText(s_TextApple, i_TextPX, i_TextPY, 20, Color.Black);

        // We draw the rectangle around the ProgressApple
        Rectangle rec_ProgressOutline = new(i_RecPX, i_RecPY, i_Width, i_Height);
        Raylib.DrawRectangleRoundedLinesEx(rec_ProgressOutline, 1, 1, 2f, Color.DarkGreen);
        
    }
}