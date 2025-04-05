using Raylib_cs;

public class MovementAlgorithm
{
    private Board board;
    private Snake snake;
    private Pathfinder pathfinder = new();
    private SnakeBrain snakeBrain;
    private int i_EnemyLevel;
    private int i_PathIndex;
    private float f_Timer = 0;
    private bool b_CheckAlgo = true;
    private bool b_IsTargettingBonus;


    #region Encapsulation
    public Board GetBoard() => board;
    public Snake GetSnake() => snake;
    public int GetLevel() => i_EnemyLevel;
    public SnakeBrain GetSnakeBrain() => snakeBrain;
    #endregion

    public MovementAlgorithm(Board localBoard, Snake localSnake, int localLevel)
    {
        board = localBoard;
        snake = localSnake;
        i_EnemyLevel = localLevel;

        snakeBrain = new(i_EnemyLevel);
    }

    // Function called by Enemy script to launch the Algorithm on the Update methode
    public void LaunchAlgorithm()
    {
        b_CheckAlgo = GenericFunction.UpdateTimer(ref f_Timer);

        if (b_CheckAlgo)
        {
            if (CheckToLaunchPathfinder())
            {
                b_CheckAlgo = false;

                SnakeView();

                // Create the graph that contains all element around the head of the Snake with distance available
                pathfinder.CreateGraph(this);

                i_PathIndex = 0;

                pathfinder.CreatePathToTarget(SnakeTarget());

                TargetIsBonusOrApple();
            }

            UpdateDirectionSnake();

            i_PathIndex++;

            b_CheckAlgo = false;
        }
    }

    // Method to check if we have to relaunch the Pathfinder algorithm or not
    private bool CheckToLaunchPathfinder()
    {
        // First time we launch the Algorithm
        if (pathfinder.GetCurrentPath().Count < 1)
            return true;

        // The Snake arrived at the destination, we need to compute a new direction
        if (i_PathIndex == pathfinder.GetCurrentPath().Count - 1)
            return true;

        // We launch a random number to see if the snake decides to change the direction
        if (CheckToChangeDirection())
            return true;

        return false;
    }

    // Method to see if the snake Enemy has to change the direction
    private bool CheckToChangeDirection()
    {
        if (!b_IsTargettingBonus)
            return Raylib.GetRandomValue(1, snakeBrain.i_ValueMax) > snakeBrain.i_ThresholdChangeDirectionBonusTargeted;

        else
            return Raylib.GetRandomValue(1, snakeBrain.i_ValueMax) > snakeBrain.i_ThresholdChangeDirection;
    }

    // Method that will generate a Random number to check if the snake has a Priorisation regarding Bonus and See obstacle
    private void SnakeView()
    {
        int tmp = Raylib.GetRandomValue(1, snakeBrain.i_ValueMax);

        // First part, we manage the Priorisation [CAN BE OPTIMIZE LATER TO HAVE A NEW DECISION REGARDING TIMER]
        if (tmp <= snakeBrain.i_ThresholdApple) snakeBrain.b_FocusApple = true;
        else if (tmp <= snakeBrain.i_ThresholdBonus) snakeBrain.b_FocusBonus = true;

        // Second part, we manage the view of obstacles
        snakeBrain.b_CanSeeBorder = tmp <= snakeBrain.i_ThresholdBorder;
        snakeBrain.b_CanSeeObstacle = tmp <= snakeBrain.i_ThresholdObstacle;
        snakeBrain.b_CanSeeOwnBody = tmp <= snakeBrain.i_ThresholdOwnBody;
    }

    // Method that will generate the target for the snake
    private int SnakeTarget()
    {
        int i_IndexTarget = -1;

        if (snakeBrain.b_FocusApple)
            i_IndexTarget = pathfinder.GetIndexCell(TypeCell.Apple);
        else if (snakeBrain.b_FocusBonus)
        {
            int indexRandom = Raylib.GetRandomValue(1, 2);
            i_IndexTarget = (indexRandom == 1) ? pathfinder.GetIndexCell(TypeCell.Bonus) : pathfinder.GetIndexCell(TypeCell.Malus);
            
            if (i_IndexTarget == -1)
                i_IndexTarget = (indexRandom == 1) ? pathfinder.GetIndexCell(TypeCell.Malus) : pathfinder.GetIndexCell(TypeCell.Bonus);
        }
            

        if (i_IndexTarget < 0)
            i_IndexTarget = Raylib.GetRandomValue(1, pathfinder.GetCurrentFrontier().Count - 1);

        return i_IndexTarget;
    }

    // Method that will change the Direction regarding the Path created by the Pathfinding algorithm 
    private void UpdateDirectionSnake()
    {
        int i_newDirection;
        // Compare position between the origin and the path
        if (i_PathIndex >= pathfinder.GetCurrentPath().Count - 1)
        {
            i_newDirection = Raylib.GetRandomValue(1, 4);
            Console.WriteLine($"[MvtAlgorith] Weird issue here, with a PathIndex equal to {i_PathIndex} and the Path count is {pathfinder.GetCurrentPath().Count}");
        }
        else
        {
            i_newDirection = ChangeDirection(pathfinder.GetCurrentPath()[i_PathIndex], pathfinder.GetCurrentPath()[i_PathIndex + 1]);
        } 

        snake.ChangeDirection(i_newDirection);
    }

    // Method to compare both position to return the correct Direction
    private static int ChangeDirection(PathfindingCell origin, PathfindingCell destination)
    {
        if (origin.GetCellPosition().Y > destination.GetCellPosition().Y)
            return 1;
        else if (origin.GetCellPosition().X < destination.GetCellPosition().X)
            return 2;
        else if (origin.GetCellPosition().Y < destination.GetCellPosition().Y)
            return 3;
        else if (origin.GetCellPosition().X > destination.GetCellPosition().X)
            return 4;
        else
            return 1;
    }

    // Method that update the bool to know if the snake is targetting a Bonus or an Apple
    private void TargetIsBonusOrApple()
    {
        if (pathfinder.GetCurrentPath().Count > 0)
        {
            Cell cellTargeted = pathfinder.GetCurrentPath().Last();

            b_IsTargettingBonus = (cellTargeted.GetTypeCell() == TypeCell.Apple) || (cellTargeted.GetTypeCell() == TypeCell.Bonus);
        }
    }
}

