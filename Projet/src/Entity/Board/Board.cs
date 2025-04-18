using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using Raylib_cs;

public class Board
{
    //public delegate void CollisionEntity();

    #region Event
    public event Action? CollisionApple;
    public event Action? CollisionBonus;
    public event Action? CollisionMalus;
    public event Action? CollisionCollider;
    public event Action? CollisionBorder;
    public event Action? CollisionSnake;
    //public event Action? CollisionOpponent;
    #endregion

    #region Variable
    private readonly Cell[,] boards = new Cell[GameInfo.i_nbCol, GameInfo.i_nbLin];
    #endregion

    public Board()
    {
        for (int col = 0; col < GameInfo.i_nbCol; col++)
        {
            for (int lin = 0; lin < GameInfo.i_nbLin; lin++)
            {
                boards[col, lin] = new(TypeCell.None, new Vector2(col, lin));
            }
        }
    }

    public void AddObject(Vector2 v2_PositionObjectToAdd, TypeCell newTypeCell) => boards[(int)v2_PositionObjectToAdd.X, (int)v2_PositionObjectToAdd.Y].UpdateCell(newTypeCell);

    public void RemoveObject(Vector2 v2_PositionObjectToRemove) => boards[(int)v2_PositionObjectToRemove.X, (int)v2_PositionObjectToRemove.Y].UpdateCell(TypeCell.None);

    // Retrieve the Cell by a Position
    public Cell GetCellFromBoard(Vector2 v2_PositionCell) => boards[(int) v2_PositionCell.X, (int) v2_PositionCell.Y];

    // Retrieve the TypeCell from the Position
    public TypeCell GetValueBoard(Vector2 v2_PositionToReturn)
    {
        int i_IndexCol = (int)v2_PositionToReturn.X;
        int i_IndexLin = (int)v2_PositionToReturn.Y;

        if (i_IndexCol < 0 || i_IndexCol >= GameInfo.i_nbCol || i_IndexLin < 0 || i_IndexLin >= GameInfo.i_nbLin)
            return TypeCell.Border;
        else
            return boards[i_IndexCol, i_IndexLin].GetTypeCell();
    }

    // Methode to generate the position of the new Apple on the map
    public void GenerateNewApple()
    {
        int i_IndexCol;
        int i_IndexLin;
        Vector2 v2_PositionApple;

        bool b_PositionValidated;
        do
        {
            i_IndexCol = Raylib.GetRandomValue(0, GameInfo.i_nbCol - 1);
            i_IndexLin = Raylib.GetRandomValue(0, GameInfo.i_nbLin - 1);

            v2_PositionApple = new(i_IndexCol, i_IndexLin);

            b_PositionValidated = CheckCollision(v2_PositionApple, false);

        } while (!b_PositionValidated);

        AddObject(v2_PositionApple, TypeCell.Apple);
    }

    // Method that update the Snake position on the board
    public void UpdateSnakePosition(Vector2 v2_SnakePosition, int i_SizeSnake)
    {
        int i_IndexCol = (int)v2_SnakePosition.X;
        int i_IndexLin = (int)v2_SnakePosition.Y;

        if (i_IndexCol < 0 || i_IndexLin < 0 || i_IndexCol > GameInfo.i_nbCol || i_IndexLin > GameInfo.i_nbLin)
            Debug.WriteLine("bug ici");

        Cell cell = boards[i_IndexCol, i_IndexLin];
        // Again an issue here, maybe we need to throw an error regarding the position to understand what happen 

        if (i_SizeSnake == 1)
        {
            if (cell.GetTypeCell() == TypeCell.None)
                cell.UpdateCell(TypeCell.OwnBodyHead);
            else
                cell.UpdateCell(TypeCell.None);
        }

        else if (i_SizeSnake == 2)
        {
            if (cell.GetTypeCell() == TypeCell.None)
                cell.UpdateCell(TypeCell.OwnBodyHead);
            else if (cell.GetTypeCell() == TypeCell.OwnBodyHead)
                cell.UpdateCell(TypeCell.OwnBodyTail);
            else
                cell.UpdateCell(TypeCell.None);
        }
        else if (i_SizeSnake >= 3)
        {
            if (cell.GetTypeCell() == TypeCell.None)
                cell.UpdateCell(TypeCell.OwnBodyHead);
            else if (cell.GetTypeCell() == TypeCell.OwnBodyHead)
                cell.UpdateCell(TypeCell.OwnBodyBody);
            else if (cell.GetTypeCell() == TypeCell.OwnBodyBody)
                cell.UpdateCell(TypeCell.OwnBodyTail);
            else
                cell.UpdateCell(TypeCell.None);
        }
    }

    // Called by the Snake to check the collision but also by function that add a new object on the board (Apple, Bonus, Collider)
    public bool CheckCollision(Vector2 v2_SnakePosition, bool b_isSnake)
    {
        int i_IndexCol = (int)v2_SnakePosition.X;
        int i_IndexLin = (int)v2_SnakePosition.Y;

        // First check if the head of the snake is hitting outside of the board
        if (i_IndexCol < 0 || i_IndexCol >= GameInfo.i_nbCol || i_IndexLin < 0 || i_IndexLin >= GameInfo.i_nbLin)
        {
            if (b_isSnake) CollisionBorder?.Invoke();

            return false;
        }
        else
        {
            Cell cell = boards[i_IndexCol, i_IndexLin];

            // check if the head of the snake is hitting his body or opponent body
            if ((int) cell.GetTypeCell() > 0 && (int) cell.GetTypeCell() < 4)
            {
                if (b_isSnake) CollisionSnake?.Invoke();

                return false;
            }
            
            if (cell.GetTypeCell() == TypeCell.Collision || cell.GetTypeCell() == TypeCell.Bomb)
            {
                if (b_isSnake) CollisionCollider?.Invoke();

                return false;
            }

            // check if the head of the snake is hitting an Apple
            if (cell.GetTypeCell() == TypeCell.Apple)
            {
                if (b_isSnake)
                {
                    CollisionApple?.Invoke();
                    cell.UpdateCell(TypeCell.None);
                    return true;
                }
                else
                    return false;
            }

            // check if the head of the snake is hitting a Bonus
            if (cell.GetTypeCell() == TypeCell.Bonus)
            {
                if (b_isSnake)
                {
                    CollisionBonus?.Invoke();
                    cell.UpdateCell(TypeCell.None);
                    return true;
                }
                else 
                    return false;
            }

            if (cell.GetTypeCell() == TypeCell.Malus)
            {
                if (b_isSnake)
                {
                    CollisionMalus?.Invoke();
                    cell.UpdateCell(TypeCell.None);
                    return true;
                }
                else
                    return false;
            }

            if (cell.GetTypeCell() == TypeCell.Border)
            {
                if (b_isSnake) CollisionBorder?.Invoke();

                return false;
            }

            return true;
        }
    }

    // Method used to know how many cell is still available (No Snake body, Apple, Collider)
    public List<Cell> GetCellAvailable()
    {
        List<Cell> cellAvailables = [];

        for (int col = 0; col < GameInfo.i_nbCol; col++)
        {
            for (int lin = 0; lin < GameInfo.i_nbLin; lin++)
            {
                if (boards[col, lin].GetTypeCell() == TypeCell.None)
                    cellAvailables.Add(boards[col, lin]);
            }
        }

        return cellAvailables;
    }

    // Function that has been trigger by the Power Bomb and check all cell around to see if a Snake part is here    (Moor Neighboor)
    public void TriggerBombCell(Cell cellBomb)
    {
        for (int col = -1; col <= 1; col++)
        {
            for (int lin = -1; lin <= 1; lin++)
            {
                if (lin == 0 && col == 0) continue; // Ignore the cellBomb

                int newCol = (int) cellBomb.GetCellPosition().X + col;
                int newLin = (int) cellBomb.GetCellPosition().Y + lin;

                // Vérifie que la cellule est bien dans la grille
                if (newLin >= 0 && newLin < GameInfo.i_nbLin && newCol >= 0 && newCol < GameInfo.i_nbCol)
                {
                    Cell cellToTest = boards[newCol, newLin];

                    if ((int) cellToTest.GetTypeCell() > 0 && (int) cellToTest.GetTypeCell() < 4)
                        CollisionSnake?.Invoke();
                }
            }
        }
    }
}