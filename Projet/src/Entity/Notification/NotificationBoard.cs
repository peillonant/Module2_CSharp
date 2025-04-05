using System.Drawing;

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
    public NotificationBoard(TypeNotificationBoard typeNotificationBoard, int i_PX, int i_PY, float f_Delay, float f_Zoom) : base (i_PX, i_PY, f_Delay)
    {
        this.typeNotificationBoard = typeNotificationBoard;
        this.f_Zoom = f_Zoom;
        b_IsTriggered = true;
    }
}