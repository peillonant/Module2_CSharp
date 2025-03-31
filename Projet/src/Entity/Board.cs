using System;
using System.Linq.Expressions;
using System.Numerics;
using Raylib_cs;

public class Board
{
    //public delegate void CollisionEntity();

    #region Event
    public event Action? CollisionApple;
    public event Action? CollisionBonus;
    public event Action? CollisionCollider;
    public event Action? CollisionBorder;
    public event Action? CollisionSnake;
    //public event Action? CollisionOpponent;
    #endregion

    #region Variable
    private readonly Cell[,] i_Board = new Cell[GameInfo.i_nbCol, GameInfo.i_nbLin]; // Update this to be an Array of cell
    #endregion

    public Board()
    {
        for (int i = 0; i < GameInfo.i_nbLin; i++)
        {
            for (int j = 0; j < GameInfo.i_nbCol; j++)
            {
                i_Board[i, j] = new(TypeCell.None);
            }
        }

        GenerateNewApple();
    }

    public void AddObject(Vector2 v2_PositionObjectToAdd, TypeCell newTypeCell) => i_Board[(int)v2_PositionObjectToAdd.Y, (int)v2_PositionObjectToAdd.X].UpdateCell(newTypeCell);

    public void RemoveObject(Vector2 v2_PositionObjectToRemove) => i_Board[(int)v2_PositionObjectToRemove.Y, (int)v2_PositionObjectToRemove.X].UpdateCell(TypeCell.None);

    public TypeCell GetValueBoard(Vector2 v2_PositionToReturn)
    {
        int i_IndexLin = (int)v2_PositionToReturn.X;
        int i_IndexCol = (int)v2_PositionToReturn.Y;

        if (i_IndexCol < 0 || i_IndexCol >= GameInfo.i_nbCol || i_IndexLin < 0 || i_IndexLin >= GameInfo.i_nbLin)
            return TypeCell.Border;
        else
            return i_Board[i_IndexCol, i_IndexLin].GetTypeCell();
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

            v2_PositionApple = new(i_IndexLin, i_IndexCol);

            b_PositionValidated = CheckCollision(v2_PositionApple, false);

        } while (!b_PositionValidated);

        AddObject(v2_PositionApple, TypeCell.Apple);
    }

    public void UpdateSnakePosition(Vector2 v2_SnakePosition, int i_SizeSnake)
    {
        int i_IndexLin = (int)v2_SnakePosition.X;
        int i_IndexCol = (int)v2_SnakePosition.Y;
        Cell cell = i_Board[i_IndexCol, i_IndexLin];

        if (i_SizeSnake == 1)
        {
            if (cell.GetTypeCell() == TypeCell.None)
                cell.UpdateCell(TypeCell.OwnBodyHead);
            else
                cell.UpdateCell(TypeCell.None);
        }

        else if (i_SizeSnake == 2)
        {
            if ((int) cell.GetTypeCell() < 2)
            {
                cell.UpdateCell(cell.GetTypeCell() + 1);
            }
            else
                cell.UpdateCell(TypeCell.None);
        }
        else if (i_SizeSnake >= 3)
        {
            if ((int)cell.GetTypeCell() < 3)
            {
                cell.UpdateCell(cell.GetTypeCell() + 1);
            }
            else
                cell.UpdateCell(TypeCell.None);
        }
    }

    // Called by the Snake to check the collision but also by function that add a new object on the board (Apple, Bonus, Collider)
    public bool CheckCollision(Vector2 v2_SnakePosition, bool b_isSnake)
    {
        int i_IndexLin = (int)v2_SnakePosition.X;
        int i_IndexCol = (int)v2_SnakePosition.Y;

        // First check if the head of the snake is hitting outside of the board
        if (i_IndexCol < 0 || i_IndexCol >= GameInfo.i_nbCol || i_IndexLin < 0 || i_IndexLin >= GameInfo.i_nbLin)
        {
            if (b_isSnake) CollisionBorder?.Invoke();

            return false;
        }
        else
        {
            Cell cell = i_Board[i_IndexCol, i_IndexLin];

            // check if the head of the snake is hitting his body or opponent body
            if ((int) cell.GetTypeCell() > 0 && (int) cell.GetTypeCell() < 4)
            {
                if (b_isSnake) CollisionSnake?.Invoke();

                return false;
            }
            
            if (cell.GetTypeCell() == TypeCell.Collision)
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

            return true;
        }
    }
}