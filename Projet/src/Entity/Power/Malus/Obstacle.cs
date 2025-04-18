using Raylib_cs;

public class Obstacle : Power
{
    public Obstacle(PowerSystem powerSystemOrigin) : base (powerSystemOrigin)
    {
        typePower = TypePower.Malus;
        typeMalus = TypeMalus.Obstacle;
        name = "Obstacle";
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
            
            for (int i = 0; i < 5; i++)
            {
                int indexCell = Raylib.GetRandomValue(0, cellAvailables.Count - 1);    
                cellAvailables[indexCell].UpdateCell(TypeCell.Collision);
            }
        }

        NotificationBoardPower(characterTargeted, name);
    }
}