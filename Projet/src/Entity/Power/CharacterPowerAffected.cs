using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

public class CharacterPowerAffected (Character characterOrigin)
{
    public readonly Character characterOrigin = characterOrigin;

    private bool b_AffectedByBonus;
    private bool b_IsShieled;
    private bool b_IsMetal;
    private bool b_CanBounce;
    private bool b_IsFreezed;
    private float f_TimerBonus = 0;
    private float f_TimerBonusSpend = 0;

    private bool b_AffectedByMalus;
    private bool b_IsStuned;
    private bool b_HasBomb;
    private List<Cell> listCellBomb = [];
    private float f_TimerMalus = 0;
    private float f_TimerMalusSpend = 0;

    public void UpdatePowerTimer()
    {
        if (b_AffectedByBonus)
        {
            f_TimerBonusSpend += Raylib.GetFrameTime();

            if (f_TimerBonusSpend >= f_TimerBonus)
            {
                b_IsShieled = false;
                b_IsMetal = false;
                b_CanBounce = false;
                b_IsFreezed = false;
                f_TimerBonusSpend = 0;
                f_TimerBonus = 0;
            }
        }

        if (b_AffectedByMalus)
        {
            if (b_IsStuned)
            {
                f_TimerMalusSpend += Raylib.GetFrameTime();

                if (f_TimerMalusSpend >= f_TimerMalus)
                {
                    b_IsStuned = false;
                    f_TimerMalusSpend = 0;
                    f_TimerMalus = 0;
                }
            }

            if (listCellBomb.Count > 0)
                TriggerBomb();
            else if (listCellBomb.Count == 0 && b_HasBomb)
                b_HasBomb = false;   
        }

        b_AffectedByBonus = b_IsShieled || b_IsMetal || b_CanBounce || b_IsFreezed;
        b_AffectedByMalus = b_IsStuned || b_HasBomb;
    }

    #region Encapsulation
    public bool IsAffectedByBonus() => b_AffectedByBonus;
    public bool IsAffectedByMalus() => b_AffectedByMalus;
    public bool IsShield() => b_IsShieled;
    public bool IsMetal() => b_IsMetal;
    public bool CanBounce() => b_CanBounce;
    public bool IsFreezed() => b_IsFreezed;
    public bool IsStuned() => b_IsStuned;
    public bool GetHasBomb() => b_HasBomb;
    public float GetTimerBonus() => f_TimerBonus;
    public float GetTimerMalus() => f_TimerMalus;

    public void SetShield(bool b_Status) => b_IsShieled = b_Status;
    public void SetMetal(bool b_Status) => b_IsMetal = b_Status;
    public void SetBounce(bool b_Status) => b_CanBounce = b_Status;
    public void SetFreezed(bool b_Status) => b_IsFreezed = b_Status;
    public void SetStuned(bool b_Status) => b_IsStuned = b_Status;
    public void SetTimerBonus(float newTimer) =>  f_TimerBonus = newTimer;
    public void SetTimerMalus(float newTimer) =>  f_TimerMalus = newTimer;
    public void SetAffectedByBonus(bool b_Status) => b_AffectedByBonus = b_Status;
    public void SetAffectedByMalus(bool b_Status) => b_AffectedByMalus = b_Status;
    #endregion

    // Je me casse peut être trop la tête et juste un effet qui permet de changer de direction malgrès le contact le bord devrait suffir
    // Puis jouer avec un effet tweening de recule peut rendre la chose sympa
    public void Bouncing(Character character)
    {
        int randBounce = Raylib.GetRandomValue(1,2);
        randBounce = (randBounce == 1) ? -1 : 1;

        int changeDirection = character.GetSnake().GetDirection() + randBounce;

        if (changeDirection < 1)
            changeDirection = 4;
        else if (changeDirection > 4)
            changeDirection = 1;

        character.SetNewDirection(changeDirection);
    }

    // Method to manage the power Metal on the character
    // This will destruct all collision cell during the time laps
    public void Metal(Character character)
    {
        // First retrieve the direction of the snake
        int i_Direction = character.GetSnake().GetDirection();

        // Compute the cell for the next frame
        Vector2 v2_posCollision = character.GetSnake().GetSnakeHead();

        v2_posCollision = GenericFunction.ChangePosition(v2_posCollision, i_Direction);

        // Remove the TypeCell Collision on it
        character.GetBoard().RemoveObject(v2_posCollision);
    }

    // Method to manage the power Bomb on the characterBoard
    public void AddBomb(Cell cellBomb)
    {
        listCellBomb.Add(cellBomb);
        b_HasBomb = true;
        b_AffectedByMalus = true;
    }

    // Method to manage all Bomb added on the characterBoard
    private void TriggerBomb()
    {
        for (int i = 0; i < listCellBomb.Count; i++)
        {
            listCellBomb[i].UpdateCellTimer(Raylib.GetFrameTime());

            if (listCellBomb[i].GetCellTimer() > 5)
            {               
                characterOrigin.GetBoard().TriggerBombCell(listCellBomb[i]);
                characterOrigin.GetBoard().RemoveObject(listCellBomb[i].GetCellPosition());

                listCellBomb.Remove(listCellBomb[i]);
                i--;
            }
        }
    }
}