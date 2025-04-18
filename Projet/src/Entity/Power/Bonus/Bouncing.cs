public class Bouncing : Power
{
    public Bouncing(PowerSystem powerSystemOrigin) : base (powerSystemOrigin)
    {
        typePower = TypePower.Bonus;
        typeBonus = TypeBonus.Bouncing;
        name = "Bouncing";
    }

    public override void UsePower()
    {
        powerSystemOrigin.characterOrigin.GetPowerAffected().SetTimerBonus(20);
        powerSystemOrigin.characterOrigin.GetPowerAffected().SetBounce(true);
        powerSystemOrigin.characterOrigin.GetPowerAffected().SetAffectedByBonus(true);
    }
}