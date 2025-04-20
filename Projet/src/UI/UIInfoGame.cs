using System.Numerics;
using Raylib_cs;

public class UIInfoGame
{
    public static void DrawNbPlayerRemaining()
    {
        string s_Text = "Alive";
        string s_TextNbPlayer = GameInfo.GetNbCharacterAlive().ToString();
        int i_Font = 30;
        int tmpX, tmpY;
        float f_ratioPositionY = 0.2f;

        if (GameInfo.GetNbCharacterAlive() < 4)
            f_ratioPositionY = 0.175f;

        tmpX = (int)(Raylib.GetScreenWidth() / 2) - 150;
        tmpY = (int)(Raylib.GetScreenHeight() * f_ratioPositionY);

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
        int i_PY = (int)(Raylib.GetScreenHeight() * 0.1f / 2);

        int i_RecPX = i_PX - i_Width / 2;
        int i_RecPY = i_PY - i_Height / 2;

        // We draw the rectanlge progress
        float f_WidthProgress = i_Width * f_ProgressApple;

        Rectangle rec_ProgressBar = new(i_RecPX, i_RecPY, f_WidthProgress, i_Height);
        Raylib.DrawRectangleRounded(rec_ProgressBar, 1, 1, Color.Green);

        // We draw the text in the middle of ProgressApple
        string s_TextApple = GameInfo.GetNbAppleEaten().ToString() + "/" + GameInfo.GetNbAppleNeeded().ToString();
        Vector2 v2_SizeText = Raylib.MeasureTextEx(Raylib.GetFontDefault(), s_TextApple, 20, 1);
        int i_TextPX = (int)(i_PX - (v2_SizeText.X / 2));
        int i_TextPY = (int)(i_PY - (v2_SizeText.Y / 2));

        Raylib.DrawText(s_TextApple, i_TextPX, i_TextPY, 20, Color.Black);

        // We draw the rectangle around the ProgressApple
        Rectangle rec_ProgressOutline = new(i_RecPX, i_RecPY, i_Width, i_Height);
        Raylib.DrawRectangleRoundedLinesEx(rec_ProgressOutline, 1, 1, 2f, Color.DarkGreen);

    }
}