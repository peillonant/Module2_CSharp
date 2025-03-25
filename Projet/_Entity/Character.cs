using System.Runtime.CompilerServices;
using Raylib_cs;

public class Character
{
    protected Board characterBoard;
    protected Snake characterSnake;
    protected Timer characterTimer = new();
    protected UI_Board UI_Board;
    protected int i_Ranking;

    public Character()
    {
        characterBoard = new Board(false);

        // Position the Snake on the playerBoard at the beginning
        characterBoard.AddObject(new(9, 9), 1);

        // 1 = Top, 2 = Right, 3 = Bottom, 4 = Left
        characterSnake = new(Raylib.GetRandomValue(1, 4));

        UI_Board = new(ref characterBoard);             
        
        SubscriptionEvent(); 
    }

    public Board GetBoard() => characterBoard;
    public Snake GetSnake() => characterSnake;
    public Timer GetTimer() => characterTimer;
    public int GetRanking() => i_Ranking;
    public int SetRanking(int i_newRanking) => i_Ranking = i_newRanking; 

    protected void SubscriptionEvent()
    {
        // Managing the subscription regarding the Apple
        // Optimization create just one function that call all these method
        characterBoard.CollisionApple += CollisitionApple;

        // Managing the subscription regarding the Timer
        characterTimer.TimerDown += OnTimerDown;

        // Managing the subscription regarding the loose on the Collision
        characterBoard.CollisionBorder += OnCollisionLost;
        characterBoard.CollisionCollider += OnCollisionLost;
        characterBoard.CollisionSnake += OnCollisionLost;
        characterBoard.CollisionOpponent += OnCollisionLost;
    }

    private void CollisitionApple()
    {
        characterSnake.IncreaseSize(1);
        characterBoard.GenerateNewApple();
        characterTimer.IncreaseTimer();
        GameInfo.Instance.IncreaseCptApple();
    }

    protected virtual void OnTimerDown()
    {
        GameManager.Instance.GameLostTimer();
    }

    protected virtual void OnCollisionLost()
    {
        GameManager.Instance.GameLostCollision();
    }

    public virtual void UpdatePlayer()
    {
        characterSnake.UpdateSnakeMovement(characterBoard);
        characterTimer.UpdateTimer();
    }
    
    public virtual void DrawBoard() {}

    public virtual void DrawPlayerInfo() {}
}