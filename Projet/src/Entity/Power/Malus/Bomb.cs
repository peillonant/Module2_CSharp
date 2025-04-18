using Raylib_cs;

// Need to find a way to explode after X secondes on all cell around

public class Bomb : Power
{
    public Bomb(PowerSystem powerSystemOrigin) : base (powerSystemOrigin)
    {
        typePower = TypePower.Malus;
        typeMalus = TypeMalus.Bomb;
        name = "Bomb";
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

            characterTargeted.GetPowerAffected().AddBomb(cellAvailables[indexCell]);
            characterTargeted.GetPowerAffected().SetAffectedByMalus(true);

            cellAvailables[indexCell].UpdateCell(TypeCell.Bomb);
            cellAvailables[indexCell].SetCellLifeTimer(5);
            
        }

        NotificationBoardPower(characterTargeted, name);
    }
}