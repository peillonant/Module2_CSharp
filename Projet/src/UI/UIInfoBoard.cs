using System.Numerics;
using Raylib_cs;

public class UIInfoBoard
{
    public static void DrawTimer(Character characterOrigin)
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
                tmpX = (int)(characterOrigin.GetPosition().X + (GameInfo.i_nbCol / 2 * GameInfo.i_SizeCell * characterOrigin.GetZoom()));
                tmpY = (int)(characterOrigin.GetPosition().Y + (GameInfo.i_nbLin / 2 * GameInfo.i_SizeCell * characterOrigin.GetZoom()));

                colorFont = Color.Black;
            }

            tmpX -= (int)v2_TextSize.X / 2;
            tmpY -= (int)v2_TextSize.Y / 2;

            Raylib.DrawText(s_TextTimer, tmpX, tmpY, i_Font, colorFont);
        }
    }

    public static void DrawPosition(Character characterOrigin)
    {

        string s_TextPosition = characterOrigin.GetRanking().ToString();
        int i_Font = (int)(20 * characterOrigin.GetZoom());
        int tmpX, tmpY;
        Color colorFont;

        if (characterOrigin.IsPlayer())
        {
            float f_ratioPositionY = 0.2f;

            if (GameInfo.GetNbCharacterAlive() < 4)
                f_ratioPositionY = 0.175f;

            s_TextPosition += " / " + GameInfo.GetNbCharacterTotal().ToString();

            tmpX = (int)Raylib.GetScreenWidth() / 2;
            tmpY = (int)(Raylib.GetScreenHeight() * f_ratioPositionY);

            colorFont = Color.LightGray;
        }
        else
        {
            tmpX = (int)(characterOrigin.GetPosition().X + (GameInfo.i_nbCol / 2 * GameInfo.i_SizeCell * characterOrigin.GetZoom()));
            tmpY = characterOrigin.IsAlive() ? (int)(characterOrigin.GetPosition().Y + GameInfo.i_nbLin / 2 * GameInfo.i_SizeCell / 2 * characterOrigin.GetZoom()) : (int)(characterOrigin.GetPosition().Y + GameInfo.i_nbLin * GameInfo.i_SizeCell / 2 * characterOrigin.GetZoom());
            i_Font = characterOrigin.IsAlive() ? i_Font : (int)(i_Font * 1.5f);

            colorFont = characterOrigin.IsAlive() ? Color.Black : Color.Red;
        }

        Vector2 v2_TextSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), s_TextPosition, i_Font, 1);

        tmpX -= (int)v2_TextSize.X / 2;
        tmpY -= (int)v2_TextSize.Y / 2;

        Raylib.DrawText(s_TextPosition, tmpX, tmpY, i_Font, colorFont);
    }
}