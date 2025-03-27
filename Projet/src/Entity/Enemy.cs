using Raylib_cs;

public class Enemy : Character
{
    private int i_EnemyLevel;
    private MovementAlgorithm movementAlgorithm;

    public Enemy(int i_newEnemyPositionOnBoard)
    {
        // Position the Snake on the playerBoard at the beginning
        characterBoard = new Board();
        characterBoard.AddObject(new(9, 9), 1);

        // 1 = Top, 2 = Right, 3 = Bottom, 4 = Left
        characterSnake = new(Raylib.GetRandomValue(1, 4));

        GenerateLevelEnemy();             
        
        // Initation of the Algorithem linked to the movement
        movementAlgorithm = new(characterBoard, ref characterSnake, i_EnemyLevel);

        // Put the position of the Enemy on the UI
        i_PositionOnTable = (i_newEnemyPositionOnBoard >= 0) ? i_newEnemyPositionOnBoard : -1;

        UI_Board = new(this);

        if (i_PositionOnTable >= 0 )
        {
           i_IndexTable = 1;
           b_IsDisplayed = true;
        }

        SubscriptionEvent(); 
    }

    // Method to generate the Level Enemy regarding the level of the game
    private void GenerateLevelEnemy()
    {   
        int i_ValueMin = 1;
        int i_ValueMax = 10;
        int i_DifficultyGame = GameInfo.Instance.GetDifficultyGame();

        if (i_DifficultyGame == 1)    
        {
            i_ValueMin = 10;
            i_ValueMax = 10;
        }
        else if (i_DifficultyGame == 2)
        {
            i_ValueMin = 2;
            i_ValueMax = 7;
        }
        else if (i_DifficultyGame == 3)
        {
            i_ValueMin = 3;
            i_ValueMax = 10;
        }

        i_EnemyLevel = Raylib.GetRandomValue(i_ValueMin, i_ValueMax);
    }

    public override void UpdatePlayer()
    {
        if (b_StillAlive)
        {
            movementAlgorithm.LaunchAlgorithm();
            base.UpdatePlayer();
            UpdatePositionBoard();
        }
    }

    protected override void OnTimerDown() => GameLost();

    protected override void OnCollisionLost() => GameLost();

    private void GameLost()
    {
        if (b_StillAlive)
        {
            b_StillAlive = false;
            characterTimer.TimerLoose();
            GameInfo.Instance.DecreaseNbPlayerAlive();
            b_CanBeDisplay = false;
        }
    }


    public override void DrawBoard()
    {
        if (i_PositionOnTable >= 0 && i_IndexTable > 0)
        {
            UI_Board.DrawBoard();
        }
    }
}