using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

public static class UINotification_Color
{
    public static Dictionary<int, Color> colorNotification = [];

    public static void InitUI_Notification_Color()
    {
        colorNotification.Add(1, Color.DarkBlue);              // Attacking
        colorNotification.Add(2, Color.Red);                   // Attacked
        colorNotification.Add(3, Color.DarkBrown);             // AttackedBy
    }
}

public static class UINotification
{

    public static void DrawNotifText(NotificationText notificationToDisplay)
    {
        int i_Font = 0, i_SpaceAnimation = 0;
        Color colorFont = Color.SkyBlue;
        int i_tmpPX = (int)notificationToDisplay.GetPosition().X;
        int i_tmpPY = (int)notificationToDisplay.GetPosition().Y;

        if (notificationToDisplay.typeNotificationText == TypeNotificationText.InfoBoard)
        {
            i_Font = 20;
            i_SpaceAnimation = 100;
            colorFont = Color.SkyBlue;
        }
        else if (notificationToDisplay.typeNotificationText == TypeNotificationText.Power)
        {
            i_Font = 30;
            i_SpaceAnimation = -100;
            colorFont = Color.Red;
        }

        i_tmpPY = (int)Tweening.OutSin(notificationToDisplay.GetTimer(), i_tmpPY, i_tmpPY - i_SpaceAnimation, notificationToDisplay.GetDelay());

        Vector2 v2_TextSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), notificationToDisplay.GetTextToDisplay(), i_Font, 1);

        i_tmpPX -= (int)v2_TextSize.X / 2;
        i_tmpPY -= (int)v2_TextSize.Y / 2;

        Raylib.DrawText(notificationToDisplay.GetTextToDisplay(), i_tmpPX, i_tmpPY, i_Font, colorFont);
    }

    public static void DrawNotifBoard(NotificationBoard notificationBoard)
    {
        int tmpWidth = (int)(GameInfo.i_SizeCell * GameInfo.i_nbCol * notificationBoard.f_Zoom);
        int tmpHeight = (int)(GameInfo.i_SizeCell * GameInfo.i_nbLin * notificationBoard.f_Zoom);

        Color colorOutline;

        Rectangle recBoard = new(notificationBoard.GetPosition().X, notificationBoard.GetPosition().Y, tmpWidth, tmpHeight);

        colorOutline = UINotification_Color.colorNotification[(int)notificationBoard.typeNotificationBoard];

        Raylib.DrawRectangleLinesEx(recBoard, 2f, colorOutline);
    }

    public static void DrawNotifArrow(NotificationArrow notificationArrow)
    {
        float f_FreezeDuration = 2f;
        float f_AnimationTime = notificationArrow.GetDelay() - f_FreezeDuration;

        Vector2 v2_PositionEnd;

        if (notificationArrow.GetTimer() < f_AnimationTime)
            v2_PositionEnd = Vector2.Lerp(notificationArrow.v2_OriginPosition, notificationArrow.GetPosition(), notificationArrow.GetTimer());
        else
            v2_PositionEnd = notificationArrow.GetPosition();

        Color colorArrow = UINotification_Color.colorNotification[(int)notificationArrow.typeNotificationArrow];

        DrawArrow(notificationArrow.v2_OriginPosition, v2_PositionEnd, colorArrow);
    }

    static void DrawArrow(Vector2 v2_Start, Vector2 v2_End, Color color)
    {
        // Drawing the line for the Arrow
        Raylib.DrawLineEx(v2_Start, v2_End, 2.5f, color);

        // Compute the edge of the Triangle
        float angle = MathF.Atan2(v2_End.Y - v2_Start.Y, v2_End.X - v2_Start.X);
        Vector2 arrowTip1 = new Vector2(v2_End.X - 15 * MathF.Cos(angle - MathF.PI / 6), v2_End.Y - 15 * MathF.Sin(angle - MathF.PI / 6));
        Vector2 arrowTip2 = new Vector2(v2_End.X - 15 * MathF.Cos(angle + MathF.PI / 6), v2_End.Y - 15 * MathF.Sin(angle + MathF.PI / 6));

        Raylib.DrawTriangle(v2_End, arrowTip2, arrowTip1, color); // Pointes de la flèche
    }

}