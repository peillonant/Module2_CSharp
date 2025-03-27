using System.Numerics;
using Raylib_cs;

public class UI_Notification
{
    public void DrawNotifSpeed(Notification notificationToDisplay)
    {
        int i_Font = 20;
        int i_tmpPX = (int) notificationToDisplay.GetPosition().X;
        int i_tmpPY = (int) notificationToDisplay.GetPosition().Y;

        i_tmpPY = (int) GenericFunction.Instance.Tweening_OutSin(notificationToDisplay.GetTimer(), i_tmpPY, i_tmpPY-100, notificationToDisplay.GetDelay());
        //i_Font = (int) GenericFunction.Instance.Tweening_OutSin(notificationToDisplay.GetTimer(), i_Font, 10, notificationToDisplay.GetDelay());
        
        Vector2 v2_TextSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), notificationToDisplay.GetTextToDisplay() , i_Font, 1);

        i_tmpPX -= (int) v2_TextSize.X / 2;
        i_tmpPY -= (int) v2_TextSize.Y / 2;

        Raylib.DrawText(notificationToDisplay.GetTextToDisplay(), i_tmpPX, i_tmpPY, i_Font, Color.SkyBlue);
    }
}