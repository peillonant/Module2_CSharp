public class UpTimer : Power
{
    public UpTimer(PowerSystem powerSystemOrigin) : base (powerSystemOrigin)
    {
        typePower = TypePower.Bonus;
        name = "UpTimer";
    }

    public override void UsePower()
    {
        powerSystemOrigin.characterOrigin.GetTimer().IncreaseTimer(10);
    }
}