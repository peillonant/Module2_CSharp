public class Extend : Power
{
    public Extend(PowerSystem powerSystemOrigin) : base (powerSystemOrigin)
    {
        typePower = TypePower.Malus;
        typeMalus = TypeMalus.Extend;
        name = "Extend";
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
            characterTargeted.GetSnake().IncreaseSize(2);
        }

        NotificationBoardPower(characterTargeted, name);
    }
}