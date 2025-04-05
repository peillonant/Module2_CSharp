using System.Diagnostics;
using Raylib_cs;

public static class NotificationManager
{
    static readonly NotificationText notificationSpeed = new("Increase Speed", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 10, 2f);
    
    static List<NotificationBoard> listNotificationBoard = [];
    static List<NotificationText> listNotificationText = [];

    public static void UpdateNotification()
    {
        notificationSpeed.UpdateNotif();
        
        for (int i = 0; i < listNotificationBoard.Count; i++)
        {
            listNotificationBoard[i].UpdateNotif();

            if (!listNotificationBoard[i].IsTriggered())
            {
                listNotificationBoard.Remove(listNotificationBoard[i]);
                i--;
            }
        }

        for (int i = 0; i < listNotificationText.Count; i++)
        {
            listNotificationText[i].UpdateNotif();

            if (!listNotificationText[i].IsTriggered())
            {
                listNotificationText.Remove(listNotificationText[i]);
                i--;
            }
        }
    }

    public static void DrawNotification()
    {
        if (notificationSpeed.IsTriggered())
            UI_Notification.DrawNotifText(notificationSpeed);

        for (int i = 0; i < listNotificationBoard.Count; i++)
        {
            UI_Notification.DrawNotifBoard(listNotificationBoard[i]);
        }

        for (int i = 0; i < listNotificationText.Count; i++)
        {
            UI_Notification.DrawNotifText(listNotificationText[i]);
        }

        UI_Board.DrawAppleEaten();
    }

    public static void AddNotificationBoard(Character characterTargeted, TypeNotificationBoard typeNotificationBoard)
    {
        NotificationBoard newNotificationBoard = new(typeNotificationBoard, (int) characterTargeted.GetPosition().X, (int) characterTargeted.GetPosition().Y, 5f, characterTargeted.GetZoom());
        listNotificationBoard.Add(newNotificationBoard);
    }

    public static void AddNotificationText(string name)
    {
        NotificationText newNotificationText = new(name, Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 10, 2f);
        listNotificationText.Add(newNotificationText);
    }

    public static void SubscriptionEvent()
    {
        GameInfo.SpeedIncreased += notificationSpeed.NotificationHasBeenTriggered;
    }
}