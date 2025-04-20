using System.Diagnostics;
using Raylib_cs;

public static class NotificationManager
{
    static readonly NotificationText notificationSpeed = new("Increase Speed", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 10, 2f, TypeNotificationText.InfoBoard);

    static List<NotificationBoard> listNotificationBoard = [];
    static List<NotificationText> listNotificationText = [];
    static List<NotificationArrow> listNotificationArrow = [];

    static float f_CurrentProgressApple = 0;
    static float f_TargetProgessApple = 0;

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

        for (int i = 0; i < listNotificationArrow.Count; i++)
        {
            listNotificationArrow[i].UpdateNotif();

            if (!listNotificationArrow[i].IsTriggered())
            {
                listNotificationArrow.Remove(listNotificationArrow[i]);
                i--;
            }
        }

        ComputeAppleProgress();
    }

    public static void DrawNotification()
    {
        if (notificationSpeed.IsTriggered())
            UINotification.DrawNotifText(notificationSpeed);

        for (int i = 0; i < listNotificationBoard.Count; i++)
            UINotification.DrawNotifBoard(listNotificationBoard[i]);

        for (int i = 0; i < listNotificationText.Count; i++)
            UINotification.DrawNotifText(listNotificationText[i]);

        for (int i = 0; i < listNotificationArrow.Count; i++)
            UINotification.DrawNotifArrow(listNotificationArrow[i]);

        UIInfoGame.DrawProgressApple(f_CurrentProgressApple);
    }

    public static void AddNotificationBoard(Character characterTargeted, TypeNotificationBoard typeNotificationBoard)
    {
        NotificationBoard newNotificationBoard = new(typeNotificationBoard, characterTargeted.GetPosition(), 5f, characterTargeted.GetZoom());
        listNotificationBoard.Add(newNotificationBoard);
    }

    public static void AddNotificationText(string name, TypeNotificationText typeNotificationText)
    {
        NotificationText newNotificationText = new(name, Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 10, 2f, typeNotificationText);
        listNotificationText.Add(newNotificationText);
        Debug.WriteLine("plop");
    }

    public static void AddNotificationArrow(Character characterOrigine, Character characterTargeted, TypeNotificationArrow typeNotificationArrow)
    {
        NotificationArrow newNotificationArrow = new(characterOrigine, characterTargeted, 3f, typeNotificationArrow);
        listNotificationArrow.Add(newNotificationArrow);
    }

    public static void SubscriptionEvent()
    {
        GameInfo.SpeedIncreased += notificationSpeed.NotificationHasBeenTriggered;
    }

    public static void UnsubscriptionEvent()
    {
        GameInfo.SpeedIncreased -= notificationSpeed.NotificationHasBeenTriggered;
    }

    // Method to compute the progression of the bar display on the Screen (using tweening method)
    private static void ComputeAppleProgress()
    {
        // While the CurrentProgressApple is behind the target, we apply the tweening to update the CurrentProgressApple to the target
        if (f_CurrentProgressApple < f_TargetProgessApple)
        {
            f_CurrentProgressApple = Tweening.Lerp(f_CurrentProgressApple, f_TargetProgessApple, 0.1f);

            if (f_TargetProgessApple - f_CurrentProgressApple < 0.05f)
                f_CurrentProgressApple = f_TargetProgessApple;
        }
        else if (f_CurrentProgressApple > f_TargetProgessApple)   // when the CurrentProgressApple is at 1, we have to reset the bar to 0 before progressing again
        {
            f_CurrentProgressApple = Tweening.Lerp(f_CurrentProgressApple, 0, 5f);
        }
        else
        {
            int i_Diff = GameInfo.GetNbAppleNeeded() / 2;

            int i_appleEaten = (GameInfo.GetNbAppleNeeded() > 10) ? GameInfo.GetNbAppleEaten() - i_Diff : GameInfo.GetNbAppleEaten();
            int i_appleTarget = (GameInfo.GetNbAppleNeeded() > 10) ? GameInfo.GetNbAppleNeeded() - i_Diff : GameInfo.GetNbAppleNeeded();

            f_TargetProgessApple = (i_appleEaten > 0) ? (float)i_appleEaten / i_appleTarget : 0;
        }
    }
}