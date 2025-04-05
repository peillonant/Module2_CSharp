public class Shield : Power
{
    public Shield(PowerSystem powerSystemOrigin) : base (powerSystemOrigin)
    {
        typePower = TypePower.Bonus;
        name = "Shield";
    }

    public override void UsePower()
    {
        powerSystemOrigin.characterOrigin.GetPowerAffected().SetTimerBonus(15);
        powerSystemOrigin.characterOrigin.GetPowerAffected().SetShield(true);
        powerSystemOrigin.characterOrigin.GetPowerAffected().SetAffectedByBonus(true);
    }
}