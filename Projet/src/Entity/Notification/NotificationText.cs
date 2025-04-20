using System.Dynamic;
using System.Numerics;
using Raylib_cs;

public enum TypeNotificationText
{
    None,
    InfoBoard,
    Power,
    Count
}

public class NotificationText : Notification
{
    private readonly string s_TextToDisplay;
    public readonly TypeNotificationText typeNotificationText;

    public string GetTextToDisplay() => s_TextToDisplay;

    public NotificationText(string s_TextToDisplay, int i_PX, int i_PY, float f_Delay, TypeNotificationText typeNotificationText) : base(new(i_PX, i_PY), f_Delay)
    {
        this.typeNotificationText = typeNotificationText;
        this.s_TextToDisplay = s_TextToDisplay;
        b_IsTriggered = true;
    }
}