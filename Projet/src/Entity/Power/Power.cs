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
    None,
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
    None,
    Stun,
    Obstacle,
    DownTimer,
    Extend,
    Bomb,
    Border,
    Count
}

public abstract class Power(PowerSystem powerSystemOrigin)
{
    protected TypePower typePower;
    protected PowerSystem powerSystemOrigin = powerSystemOrigin;
    protected TypeBonus typeBonus = TypeBonus.None;
    protected TypeMalus typeMalus = TypeMalus.None;
    protected string name = "";
    protected float f_Timer = 0;

    public abstract void UsePower();

    public TypePower GetTypePower() => typePower;
    public string GetNamePower() => name;

    public TypeBonus GetPowerTypeBonus() => typeBonus;
    public TypeMalus GetPowerTypeMalus() => typeMalus;

    protected void NotificationBoardPower(Character characterTargeted, string namePower)
    {
        if (powerSystemOrigin.characterOrigin.IsPlayer())
        {
            NotificationManager.AddNotificationBoard(characterTargeted, TypeNotificationBoard.Attacking);

            NotificationManager.AddNotificationArrow(powerSystemOrigin.characterOrigin, characterTargeted, TypeNotificationArrow.PlayerAttacking);
        }
        else if (characterTargeted.IsPlayer())
        {
            NotificationManager.AddNotificationBoard(characterTargeted, TypeNotificationBoard.Attacked);
            NotificationManager.AddNotificationBoard(powerSystemOrigin.characterOrigin, TypeNotificationBoard.AttackedBy);

            NotificationManager.AddNotificationText(namePower, TypeNotificationText.Power);

            NotificationManager.AddNotificationArrow(powerSystemOrigin.characterOrigin, characterTargeted, TypeNotificationArrow.PlayerAttacked);
        }
        else if (powerSystemOrigin.characterOrigin.IsDisplayed() && characterTargeted.IsDisplayed())
        {
            NotificationManager.AddNotificationArrow(powerSystemOrigin.characterOrigin, characterTargeted, TypeNotificationArrow.EnemiesAttackingThemself);
        }
    }

}