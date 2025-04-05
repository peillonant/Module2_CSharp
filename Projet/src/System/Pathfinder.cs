using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

public class Pathfinder
{
    #region member fields
    List<PathfindingCell> currentFrontier = [];
    List<PathfindingCell> currentPath = [];
    Queue<PathfindingCell> openSet = [];
    PathfindingCell? CellSnakeHead;
    PathfindingCell? currentCell;
    List<PathfindingCell> adjacentCells = [];
    List<PathfindingCell> pathfindingCells = [];
    #endregion

    public List<PathfindingCell> GetCurrentPath() => currentPath;

    public List<PathfindingCell> GetCurrentFrontier() => currentFrontier;

    // Function to clear the currentFrontier list when we create the new graph for the snake
    private void ResetPathfinder()
    {
        currentFrontier.Clear();
        openSet.Clear();
    }

    // Main pathfinding function, marks cells as being in frontier, while keeping a copy of the frontier
    // in "currentFrontier" for later clearing
    public void CreateGraph(MovementAlgorithm mvtAlgo)
    {
        ResetPathfinder();

        // Retrieve the cell where the snakeHead is
        CellSnakeHead = new(mvtAlgo.GetSnake().GetHead(), TypeCell.OwnBodyHead);

        openSet.Enqueue(CellSnakeHead);
        CellSnakeHead.SetCost(0);
        currentFrontier.Add(CellSnakeHead);

        while (openSet.Count > 0)
        {
            currentCell = openSet.Dequeue();

            // now check all direction adjacent to the currentCell
            int i_TmpDirection = (currentCell.GetCost() == 0) ? mvtAlgo.GetSnake().GetDirection() : 0;

            // If the Coast is equal to 0, we are on the head of the snake so we can just go forward, left or right but not on the back         
            adjacentCells = FindAdjacentCells(CellSnakeHead, currentCell, mvtAlgo.GetSnakeBrain(), mvtAlgo.GetBoard(), i_TmpDirection);

            foreach (PathfindingCell adjacentCell in adjacentCells)
            {
                if (openSet.Contains(adjacentCell))
                    continue;

                adjacentCell.SetCost(currentCell.GetCost() + 1);

                // Check the level to see if we can go deeper or not
                if (!IsValidCell(adjacentCell, mvtAlgo.GetLevel()))
                    continue;

                adjacentCell.SetParentCell(currentCell);

                openSet.Enqueue(adjacentCell);
                adjacentCell.SetInFrontier(true);
                currentFrontier.Add(adjacentCell);
            }
        }
    }

    // Check if the cell is not already on the currentFrontier list and also if the cost to got there is not above the Enemy level
    private bool IsValidCell(PathfindingCell pathfindingCell, int maxcost) => !currentFrontier.Contains(pathfindingCell) && pathfindingCell.GetCost() <= maxcost;

    // Returns a list of all neighboring cells
    private List<PathfindingCell> FindAdjacentCells(PathfindingCell CellSnakeHead, PathfindingCell origin, SnakeBrain SnakeBrain, Board board, int i_direction)
    {
        pathfindingCells.Clear();
        int i_CellToStart = 1;
        int i_NbAdjacentToCheck = 5;

        if (i_direction != 0)
        {
            i_CellToStart = (i_direction - 1 == 0) ? 4 : i_direction - 1;
            i_NbAdjacentToCheck = 3 + i_CellToStart;
        }

        // Rotate around the origine cell to check each cell around and find all correct adjacent cell
        for (int i = i_CellToStart; i < i_NbAdjacentToCheck; i++)
        {
            int tmpDirection = (i % 4 == 0) ? 4 : i % 4;
            Vector2 v2_PositionToCheck = origin.GetCellPosition();

            GenericFunction.ChangePosition(ref v2_PositionToCheck, tmpDirection);

            PathfindingCell adjacentCell = new(v2_PositionToCheck, board.GetValueBoard(v2_PositionToCheck));

            if (CheckVision(adjacentCell, SnakeBrain))
                continue;

            if (CheckCell(adjacentCell.GetCellPosition()))
                continue;

            if (CellSnakeHead.GetCellPosition() == adjacentCell.GetCellPosition())
                continue;

            pathfindingCells.Add(adjacentCell);
        }
        return pathfindingCells;
    }

    // Check if the Position received is already savec on the currentFrontier
    private bool CheckCell(Vector2 v2_PositionCell)
    {
        foreach (PathfindingCell pathfindingCell in currentFrontier)
        {
            if (pathfindingCell.GetCellPosition() == v2_PositionCell)
                return true;
        }
        return false;
    }

    // Check if the Position received is valided regarding the vision of the Snake
    private bool CheckVision(PathfindingCell pathfindingCell, SnakeBrain snakeBrain)
    {
        if (snakeBrain.b_CanSeeBorder && pathfindingCell.GetTypeCell() == TypeCell.Border)
            return true;

        if (snakeBrain.b_CanSeeObstacle && pathfindingCell.GetTypeCell() == TypeCell.Collision)
            return true;

        if (snakeBrain.b_CanSeeOwnBody && 
            (pathfindingCell.GetTypeCell() == TypeCell.OwnBodyHead || pathfindingCell.GetTypeCell() == TypeCell.OwnBodyBody || pathfindingCell.GetTypeCell() == TypeCell.OwnBodyTail))
            return true;

        return false;
    }

    // Method to retrieve the Cell that contain the Apple and return it
    public int GetIndexCell(TypeCell typeCellTarget)
    {
        PathfindingCell pathfindingCell;

        for (int i = 0; i < currentFrontier.Count; i++)
        {
            pathfindingCell = currentFrontier[i];
            if (pathfindingCell.GetTypeCell() == typeCellTarget)
                return i;
        }
        return -1;
    }

    // Creates a path between the Cell origin (Head of Snake) and the Cell targeted
    public void CreatePathToTarget(int i_IndexCell)
    {
        PathfindingCell? targetCell = currentFrontier[i_IndexCell];
        PathfindingCell origin = currentFrontier[0];

        currentPath.Clear();

        while (targetCell != null && targetCell.GetCellPosition() != origin.GetCellPosition())
        {
            currentPath.Add(targetCell);

            if (targetCell.GetParentCell() != null)
                targetCell = targetCell.GetParentCell();
            else
                break;
        }

        currentPath.Add(origin);
        currentPath.Reverse();
    }
}
