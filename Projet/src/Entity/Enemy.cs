using Raylib_cs;

public class Enemy : Character
{
    private int i_EnemyLevel;
    public MovementAlgorithm movementAlgorithm;

    public Enemy() : base()
    {
        GenerateLevelEnemy();

        // Initation of the Algorithem linked to the movement
        movementAlgorithm = new(characterBoard, characterSnake, i_EnemyLevel);
    }

    // Method to generate the Level Enemy regarding the level of the game
    private void GenerateLevelEnemy()
    {
        int i_ValueMin = 1;
        int i_ValueMax = 10;
        int i_DifficultyGame = GameInfo.GetDifficultyGame();

        if (i_DifficultyGame == 1)
        {
            i_ValueMin = 1;
            i_ValueMax = 5;
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
            GameInfo.DecreaseNbCharacterAlive();
        }
    }
}