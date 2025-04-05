using System.Diagnostics;

public class Stun : Power
{
    public Stun(PowerSystem powerSystemOrigin) : base (powerSystemOrigin)
    {
        typePower = TypePower.Malus;
        name = "Stun";
    }


    public override void UsePower()
    {
        Character characterTargeted;
        bool b_CharacterAlreadyAffected;
        int cpt = 0;
        do
        {
            characterTargeted = powerSystemOrigin.PickCharacterOpponent();

            if (characterTargeted.GetPowerAffected().IsAffectedByMalus() && characterTargeted.GetPowerAffected().IsStuned())
                b_CharacterAlreadyAffected = true;
            else
                b_CharacterAlreadyAffected = false;
            
            cpt++;
        }while(b_CharacterAlreadyAffected || cpt < 17);
        
        if (characterTargeted.GetPowerAffected().IsShield())
        {
            characterTargeted.GetPowerAffected().SetShield(false);
        }
        else
        {
            characterTargeted.GetPowerAffected().SetStuned(true);
            characterTargeted.GetPowerAffected().SetAffectedByMalus(true);
            characterTargeted.GetPowerAffected().SetTimerMalus(7.5f);
        }
        
        NotificationBoardPower(characterTargeted, name);
    }
}