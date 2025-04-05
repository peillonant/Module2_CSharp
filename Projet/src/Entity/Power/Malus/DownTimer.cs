using Raylib_cs;

public class DownTimer : Power
{
    public DownTimer(PowerSystem powerSystemOrigin) : base (powerSystemOrigin)
    {
        typePower = TypePower.Malus;
        name = "DownTimer";
    }
    
    public override void UsePower()
    {
        Character characterTargeted = powerSystemOrigin.PickCharacterOpponent();
        
        if (characterTargeted.GetPowerAffected().IsShield())
        {
            characterTargeted.GetPowerAffected().SetShield(false);
        }
        else
        {
            characterTargeted.GetTimer().DownTimerPower();
        }

        NotificationBoardPower(characterTargeted, name);
    }
}