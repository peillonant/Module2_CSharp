using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using Raylib_cs;

public class Enemy : Character
{
    private int i_EnemyLevel;
    private int i_EnemyPositionOnBoard;
    private bool b_StillAlive;
    private int i_EnemyPositionLadder;

    private MovementAlgorithm movementAlgorithm;

    public Enemy(int i_newEnemyPositionOnBoard)
    {
        // Position the Snake on the playerBoard at the beginning
        characterBoard = new Board(false);
        characterBoard.AddObject(new(9, 9), 1);

        // 1 = Top, 2 = Right, 3 = Bottom, 4 = Left
        characterSnake = new(Raylib.GetRandomValue(1, 4));

        GenerateLevelEnemy();             
        
        // Initation of the Algorithem linked to the movement
        movementAlgorithm = new(characterBoard, ref characterSnake, i_EnemyLevel);

        // Put the position of the Enemy on the UI
        i_EnemyPositionOnBoard = (i_newEnemyPositionOnBoard >= 0) ? i_newEnemyPositionOnBoard : -1;

        UI_Board = new(ref characterBoard);

        if (i_EnemyPositionOnBoard >= 0 )
           UI_Board.UpdatePositionBoard(i_EnemyPositionOnBoard);

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
        movementAlgorithm.LaunchAlgorithm();
        base.UpdatePlayer();
    }


    protected override void OnTimerDown()
    {
        //Console.Write("EnenmyManger lose regarding the Time");
    }

    protected override void OnCollisionLost()
    {
        //Console.Write("EnenmyManger lose regarding the Collision");
    }


    public override void DrawBoard()
    {
        if (i_EnemyPositionOnBoard >= 0)
        {
            UI_Board.DrawBoard();
            UI_Board.DrawInfo(this);
        }
    }
}