using System.Numerics;
using Raylib_cs;

public class Notification
{
    protected Vector2 v2_Position;
    protected float f_Delay;
    protected float f_Timer;
    protected bool b_IsTriggered;

    // Not optimize due to rezising. Or we have to update when we pass the new size on the Option
    public Notification(Vector2 v2_Position, float f_Delay)
    {
        this.v2_Position = v2_Position;
        this.f_Delay = f_Delay;
    }

    // GET METHOD
    public Vector2 GetPosition() => v2_Position;
    public float GetTimer() => f_Timer;
    public bool IsTriggered() => b_IsTriggered;
    public float GetDelay() => f_Delay;

    // SET METHOD
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