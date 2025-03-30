using Raylib_cs;

public class NotificationManager
{
    readonly UI_Notification ui_Notification = new();

    readonly Notification notificationSpeed = new("Increase Speed", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 10, 2f);

    public NotificationManager()
    {
        GameInfo.SpeedIncreased += notificationSpeed.NotificationHasBeenTriggered;
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