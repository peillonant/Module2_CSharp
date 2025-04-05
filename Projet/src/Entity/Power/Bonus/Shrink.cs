public class Shrink : Power
{
    public Shrink(PowerSystem powerSystemOrigin) : base (powerSystemOrigin)
    {
        typePower = TypePower.Bonus;
        name = "Shrink";
    }

    public override void UsePower()
    {
        powerSystemOrigin.characterOrigin.GetSnake().DecreaseSize(2);
    }
}