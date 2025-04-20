using System.Drawing;
using System.Numerics;

public enum TypeNotificationBoard
{
    None,
    Attacking,
    Attacked,
    AttackedBy,
    Count
}

public class NotificationBoard : Notification
{
    public readonly TypeNotificationBoard typeNotificationBoard;
    public readonly float f_Zoom;

    // Not optimize due to rezising. Or we have to update when we pass the new size on the Option
    public NotificationBoard(TypeNotificationBoard typeNotificationBoard, Vector2 v2_Position, float f_Delay, float f_Zoom) : base(v2_Position, f_Delay)
    {
        this.typeNotificationBoard = typeNotificationBoard;
        this.f_Zoom = f_Zoom;
        b_IsTriggered = true;
    }
}

