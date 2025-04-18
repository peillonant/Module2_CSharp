using System.Numerics;
using Raylib_cs;

public abstract class Character
{
    #region Variable Entity
    protected Board characterBoard;
    protected Snake characterSnake;
    protected Timer characterTimer = new();
    protected PowerSystem powerSystem;
    protected CharacterPowerAffected characterPowerAffected;
    protected ViewBoard viewBoard;
    #endregion

    #region Variable
    // need to create a struct that contain all this information
    protected Vector2 v2_Position;
    protected bool b_isPlayer;
    protected int i_Ranking;
    protected bool b_StillAlive = true;
    protected float f_Zoom = 1;
    protected int i_IndexTable = -1;
    protected int i_PositionOnTable;
    protected bool b_IsDisplayed = false;
    protected int i_newDirection;
    protected bool b_AnimationPerformed = false;
    protected Color characterColor;
    #endregion 

    public Character()
    {
        characterBoard = new Board();

        // Position the Snake on the playerBoard at the beginning
        characterBoard.AddObject(new(9, 9), TypeCell.OwnBodyHead);

        // 1 = Top, 2 = Right, 3 = Bottom, 4 = Left
        characterSnake = new(Raylib.GetRandomValue(1, 4), characterBoard, () => characterBoard.GenerateNewApple());
        
        powerSystem = new(this);

        characterPowerAffected = new(this);

        viewBoard = new ViewBoard(characterBoard, characterSnake);

        SubscriptionEvent();
    }

    #region Getter
    public Board GetBoard() => characterBoard;
    public Snake GetSnake() => characterSnake;
    public Timer GetTimer() => characterTimer;
    public PowerSystem GetPowerSystem() => powerSystem;
    public CharacterPowerAffected GetPowerAffected() => characterPowerAffected;
    public ViewBoard GetViewBoard() => viewBoard;
    public bool IsPlayer() => b_isPlayer;
    public bool IsAlive() => b_StillAlive;
    public int GetRanking() => i_Ranking;
    public Vector2 GetPosition() => v2_Position;
    public float GetZoom() => f_Zoom;
    public int GetTable() => i_IndexTable;
    public int GetPositionOnTable() => i_PositionOnTable;
    public bool IsDisplayed() => b_IsDisplayed;
    public Color GetCharacterColor() => characterColor;
    #endregion

    #region Setter
    public void SetRanking(int i_newRanking) => i_Ranking = i_newRanking;
    public void SetTable(int i_newIndexTable) => i_IndexTable = i_newIndexTable;
    public void SetPositionOnTable(int i_newPosition) => i_PositionOnTable = i_newPosition;
    public void SetIsDisplayed(bool b_newIsDisplayed) => b_IsDisplayed = b_newIsDisplayed;
    public void SetNewDirection(int newDirection) => i_newDirection = newDirection;
    #endregion

    protected void SubscriptionEvent()
    {
        // Managing the subscription regarding the Apple
        // Optimization create just one function that call all these method
        characterBoard.CollisionApple += CollisitionApple;

        // Managing the subscription regarding the Timer
        characterTimer.TimerDown += OnTimerDown;

        // Managing the subscription regarding the loose on the Collision
        characterBoard.CollisionBorder += OnCollisionBorder;
        characterBoard.CollisionCollider += OnCollisionCollider;
        characterBoard.CollisionSnake += OnCollisionLost;
        
        // Managing the subscription regarding the trigger of Bonus or Malus
        characterBoard.CollisionBonus += powerSystem.TriggerBonusPower;
        characterBoard.CollisionMalus += powerSystem.TriggerMalusPower;

        // Managing the subscription regarding the extend event
        characterSnake.CollisionExtend += OnCollisionLost;
    }

    private void CollisitionApple()
    {
        characterSnake.IncreaseSize(1);
        characterBoard.GenerateNewApple();
        characterTimer.IncreaseTimer(5);
        GameInfo.IncreaseCptApple();
    }

    protected void OnCollisionBorder()
    {
        if (characterPowerAffected.IsAffectedByBonus() && characterPowerAffected.CanBounce())
        {
            characterPowerAffected.Bouncing(this);
        }
        else
            OnCollisionLost();
    }

    protected void OnCollisionCollider()
    {
        if (characterPowerAffected.IsAffectedByBonus())
        {
            if (characterPowerAffected.CanBounce())
            {
                characterPowerAffected.Bouncing(this);
            }
            else if (characterPowerAffected.IsMetal())
            {
                characterPowerAffected.Metal(this);
            }
            else
                OnCollisionLost();
        }
        else
            OnCollisionLost();
    }

    protected abstract void OnTimerDown();

    protected abstract void OnCollisionLost();

    public virtual void UpdatePlayer()
    {
        if (b_AnimationPerformed)
        {
            if (characterPowerAffected.IsAffectedByBonus() || characterPowerAffected.IsAffectedByMalus())
                characterPowerAffected.UpdatePowerTimer();
        
            if (!characterPowerAffected.IsFreezed() && !characterPowerAffected.IsStuned())
                characterSnake.UpdateSnakeMovement();
            
            if (!characterPowerAffected.IsFreezed())
                characterTimer.UpdateTimer();

            if (!characterPowerAffected.IsStuned())
                powerSystem.UpdatePowerManager();

            b_AnimationPerformed = false;
        }

        if (b_IsDisplayed)
        {
            UpdatePositionBoard();
            viewBoard.UpdateViewBoard(() => b_AnimationPerformed = true);
        }
    }

    private void UpdatePositionBoard()
    {
        if (b_isPlayer)
        {
            f_Zoom = (GameInfo.GetNbCharacterAlive() <= 3) ? 2.5f : 2;
            v2_Position.X = (int)(Raylib.GetScreenWidth() / 2 - (GameInfo.i_nbCol / 2 * GameInfo.i_SizeCell * f_Zoom));
            v2_Position.Y = (int)(Raylib.GetScreenHeight() / 2 - (GameInfo.i_nbLin / 2 * GameInfo.i_SizeCell * f_Zoom));
        }
        else
        {
            int tmpIndexCol, tmpIndexLin;
            float f_ratioCol, f_ratioLin;

            if (GameInfo.GetNbCharacterAlive() <= 3)  // Display 2 Enemies (1 each side)
            {
                tmpIndexCol = (i_PositionOnTable % 2 == 1) ? 0 : 1;
                tmpIndexLin = 0;
                f_Zoom = 2;

                f_ratioCol = GameInfo.ratioCols_3[tmpIndexCol];
                f_ratioLin = GameInfo.ratioLins_3[tmpIndexLin];
            }
            else if (GameInfo.GetNbCharacterAlive() <= 5) // display 4 Enemies (2 each side)
            {
                tmpIndexCol = i_PositionOnTable % 2;
                tmpIndexLin = i_PositionOnTable / 2;
                f_Zoom = 1.5f;

                f_ratioCol = GameInfo.ratioCols_5[tmpIndexCol];
                f_ratioLin = GameInfo.ratioLins_5[tmpIndexLin];
            }
            else if (GameInfo.GetNbCharacterAlive() <= 9) // Display 8 Enemies (4 each side)
            {

                tmpIndexCol = i_PositionOnTable % 2;
                tmpIndexLin = i_PositionOnTable / 2;

                f_ratioCol = GameInfo.ratioCols_8[tmpIndexCol];
                f_ratioLin = GameInfo.ratioLins_8[tmpIndexLin];
            }
            else  // Display 16 Enemies (8 each side, with a 2 column of 4 lines)
            {
                tmpIndexCol = i_PositionOnTable % 4;
                tmpIndexLin = i_PositionOnTable / 4;

                f_ratioCol = GameInfo.ratioCols_16[tmpIndexCol];
                f_ratioLin = GameInfo.ratioLins_16[tmpIndexLin];
            }

            v2_Position.X = (int)(Raylib.GetScreenWidth() * f_ratioCol);
            v2_Position.Y = (int)(Raylib.GetScreenHeight() * f_ratioLin);
        }
    }
}