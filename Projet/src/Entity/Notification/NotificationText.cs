using System.Dynamic;
using System.Numerics;
using Raylib_cs;

public class NotificationText : Notification
{
    private readonly string s_TextToDisplay;

    public string GetTextToDisplay() => s_TextToDisplay;

    public NotificationText(string s_TextToDisplay, int i_PX, int i_PY, float f_Delay) : base (i_PX, i_PY, f_Delay)
    {
        this.s_TextToDisplay = s_TextToDisplay;
    }
}