using System.Diagnostics;

public enum TypePower
{
    None,
    Bonus,
    Malus,
    Count
}

public enum TypeBonus
{
    Shrink,
    UpTimer,
    Metal,
    Bouncing,
    Shield,
    Freeze,
    Count
}

public enum TypeMalus
{
    Stun,
    Obstacle,
    DownTimer,
    Extend,
    Bomb,
    Border,
    Count
}

public abstract class Power (PowerSystem powerSystemOrigin)
{
    protected TypePower typePower;
    protected PowerSystem powerSystemOrigin = powerSystemOrigin;
    protected string name = "";
    protected float f_Timer = 0;

    public abstract void UsePower();

    public TypePower GetTypePower() => typePower;
    public string GetNamePower() => name;

    protected void NotificationBoardPower(Character characterTargeted, string namePower)
    {
        if (powerSystemOrigin.characterOrigin.IsPlayer())
        {
            NotificationManager.AddNotificationBoard(characterTargeted, TypeNotificationBoard.Attacking);
        }
        else if (characterTargeted.IsPlayer())
        {
            NotificationManager.AddNotificationBoard(characterTargeted, TypeNotificationBoard.Attacked);
            NotificationManager.AddNotificationBoard(powerSystemOrigin.characterOrigin, TypeNotificationBoard.AttackedBy);
            NotificationManager.AddNotificationText(namePower);
        }
    }
    
}