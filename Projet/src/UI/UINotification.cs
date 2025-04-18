using System.Numerics;
using Raylib_cs;

public static class UINotification_Color
{
    public static Dictionary<int, Color> colorBoardNotification = [];

    public static void InitUI_Notification_Color()
    {
        colorBoardNotification.Add(1, Color.DarkBlue);              // Attacking
        colorBoardNotification.Add(2, Color.Red);                   // Attacked
        colorBoardNotification.Add(3, Color.DarkBrown);             // AttackedBy
    }
}

public static class UINotification
{
    public static void DrawNotifText(NotificationText notificationToDisplay)
    {
        int i_Font = 20;
        int i_tmpPX = (int)notificationToDisplay.GetPosition().X;
        int i_tmpPY = (int)notificationToDisplay.GetPosition().Y;

        i_tmpPY = (int)Tweening.OutSin(notificationToDisplay.GetTimer(), i_tmpPY, i_tmpPY - 100, notificationToDisplay.GetDelay());
        //i_Font = (int) GenericFunction.Instance.Tweening_OutSin(notificationToDisplay.GetTimer(), i_Font, 10, notificationToDisplay.GetDelay());

        Vector2 v2_TextSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), notificationToDisplay.GetTextToDisplay(), i_Font, 1);

        i_tmpPX -= (int)v2_TextSize.X / 2;
        i_tmpPY -= (int)v2_TextSize.Y / 2;

        Raylib.DrawText(notificationToDisplay.GetTextToDisplay(), i_tmpPX, i_tmpPY, i_Font, Color.SkyBlue);
    }

    public static void DrawNotifBoard(NotificationBoard notificationBoard)
    {
        // Display the floor of the board
        int tmpWidth = (int)(GameInfo.i_SizeCell * GameInfo.i_nbCol * notificationBoard.f_Zoom);
        int tmpHeight = (int)(GameInfo.i_SizeCell * GameInfo.i_nbLin * notificationBoard.f_Zoom);

        Color colorOutline;
        
        Rectangle recBoard = new(notificationBoard.GetPosition().X, notificationBoard.GetPosition().Y, tmpWidth, tmpHeight);

        colorOutline = UINotification_Color.colorBoardNotification[(int) notificationBoard.typeNotificationBoard];

        Raylib.DrawRectangleLinesEx(recBoard, 2f, colorOutline);
    }
}