public class Metal : Power
{
    public Metal(PowerSystem powerSystemOrigin) : base (powerSystemOrigin)
    {
        typePower = TypePower.Bonus;
        typeBonus = TypeBonus.Metal;
        name = "Metal";
    }

    public override void UsePower()
    {
        powerSystemOrigin.characterOrigin.GetPowerAffected().SetTimerBonus(10);
        powerSystemOrigin.characterOrigin.GetPowerAffected().SetMetal(true);
        powerSystemOrigin.characterOrigin.GetPowerAffected().SetAffectedByBonus(true);
    }
}