using System.Dynamic;
using System.Numerics;
using Raylib_cs;

public class Notification
{
    private readonly Vector2 v2_Position;
    private readonly string s_TextToDisplay;
    private float f_Delay;
    private float f_Timer;
    private bool b_IsTriggered;

    // Not optimize due to rezising. Or we have to update when we pass the new size on the Option
    public Notification(string s_TextToDisplay, int i_PX, int i_PY, float f_Delay)
    {
        this.s_TextToDisplay = s_TextToDisplay;
        this.v2_Position.X = i_PX;
        this.v2_Position.Y = i_PY;
        this.f_Delay = f_Delay;
    }

    // GET METHODE
    public string GetTextToDisplay() => s_TextToDisplay;
    public Vector2 GetPosition() => v2_Position;
    public float GetTimer() => f_Timer;
    public bool IsTriggered() => b_IsTriggered;
    public float GetDelay() => f_Delay;

    // SET METHODE
    public void SetTriggered(bool b_newTriggered) => b_IsTriggered = b_newTriggered; 

    public void NotificationHasBeenTriggered()
    {
        b_IsTriggered = true;
        f_Timer = 0;
    }

    public void UpdateNotif()
    {
        f_Timer += Raylib.GetFrameTime();
        
        if (f_Timer >= f_Delay && b_IsTriggered)
        {
            f_Timer = 0;
            b_IsTriggered = false;
        }
    }
}