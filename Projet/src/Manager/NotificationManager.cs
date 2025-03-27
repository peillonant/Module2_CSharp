using Raylib_cs;

public class NotificationManager
{
    UI_Notification ui_Notification = new();
    
    Notification notificationSpeed = new("Increase Speed", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 10, 2f);

    public NotificationManager()
    {
        GameInfo.Instance.SpeedIncreased += notificationSpeed.NotificationHasBeenTriggered;
    }

    public void UpdateNotification()
    {
        notificationSpeed.UpdateNotif();
    }

    public void DrawNotification()
    {
        if (notificationSpeed.IsTriggered())
            ui_Notification.DrawNotifSpeed(notificationSpeed);
    }
}