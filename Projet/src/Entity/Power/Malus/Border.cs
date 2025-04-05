using Raylib_cs;

public class Border : Power
{
    public Border(PowerSystem powerSystemOrigin) : base (powerSystemOrigin)
    {
        typePower = TypePower.Malus;
        name = "Border";
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
            List<Cell> cellAvailables = characterTargeted.GetBoard().GetCellAvailable();
        
            int indexCell = Raylib.GetRandomValue(0, cellAvailables.Count - 1);
            
            cellAvailables[indexCell].UpdateCell(TypeCell.Border);
        }

        NotificationBoardPower(characterTargeted, name);
    }
}