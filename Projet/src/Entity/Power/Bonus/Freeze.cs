public class Freeze : Power
{
    public Freeze(PowerSystem powerSystemOrigin) : base (powerSystemOrigin)
    {
        typePower = TypePower.Bonus;
        name = "Freeze";
    }

    public override void UsePower()
    {
        powerSystemOrigin.characterOrigin.GetPowerAffected().SetTimerBonus(5f);
        powerSystemOrigin.characterOrigin.GetPowerAffected().SetFreezed(true);
        powerSystemOrigin.characterOrigin.GetPowerAffected().SetAffectedByBonus(true);
    }
}