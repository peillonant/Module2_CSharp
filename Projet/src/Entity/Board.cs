using System;
using System.Linq.Expressions;
using System.Numerics;
using Raylib_cs;

public class Board
{
    //public delegate void CollisionEntity();

    #region Event
    public event Action? CollisionApple;
    //public event Action? CollisionBox;
    public event Action? CollisionCollider;
    public event Action? CollisionBorder;
    public event Action? CollisionSnake;
    public event Action? CollisionOpponent;
    #endregion

    #region Variable
    private readonly int[,] i_Board = new int[GameInfo.i_nbCol, GameInfo.i_nbLin]; // Update this to be an Array of cell
    #endregion

    public Board()
    {
        for (int i = 0; i < GameInfo.i_nbLin; i++)
        {
            for (int j = 0; j < GameInfo.i_nbCol; j++)
            {
                i_Board[i, j] = 0;
            }
        }

        GenerateNewApple();
    }

    public void AddObject(Vector2 v2_PositionObjectToAdd, int i_IndexObject) => i_Board[(int)v2_PositionObjectToAdd.Y, (int)v2_PositionObjectToAdd.X] = i_IndexObject;

    public void RemoveObject(Vector2 v2_PositionObjectToRemove) => i_Board[(int)v2_PositionObjectToRemove.Y, (int)v2_PositionObjectToRemove.X] = 0;

    public int GetValueBoard(Vector2 v2_PositionToReturn)
    {
        int i_IndexLin = (int)v2_PositionToReturn.X;
        int i_IndexCol = (int)v2_PositionToReturn.Y;

        if (i_IndexCol < 0 || i_IndexCol >= GameInfo.i_nbCol || i_IndexLin < 0 || i_IndexLin >= GameInfo.i_nbLin)
            return -1;
        else
            return i_Board[i_IndexCol, i_IndexLin];
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

        AddObject(v2_PositionApple, 10);
    }

    public void UpdateSnakePosition(Vector2 v2_SnakePosition, int i_SizeSnake)
    {
        int i_IndexLin = (int)v2_SnakePosition.X;
        int i_IndexCol = (int)v2_SnakePosition.Y;
        int i_IndexBoard = i_Board[i_IndexCol, i_IndexLin];

        if (i_SizeSnake == 1)
            i_Board[i_IndexCol, i_IndexLin] = (i_IndexBoard == 0) ? 1 : 0;

        else if (i_SizeSnake == 2)
            i_Board[i_IndexCol, i_IndexLin] = (i_IndexBoard < 2) ? i_IndexBoard + 1 : 0;

        else if (i_SizeSnake >= 3)
            i_Board[i_IndexCol, i_IndexLin] = (i_IndexBoard < 3) ? i_IndexBoard + 1 : 0;
    }

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
            int i_indexCell = i_Board[i_IndexCol, i_IndexLin];

            // Then check if the head of the snake is hitting his body or opponent body
            if (i_indexCell > 0 && i_indexCell < 4)
            {
                if (b_isSnake) CollisionSnake?.Invoke();

                return false;
            }
            else if (i_indexCell > 20 && i_indexCell < 29)
            {
                if (b_isSnake) CollisionCollider?.Invoke();

                return false;
            }
            else if (i_indexCell > 30 && i_indexCell < 34)
            {
                if (b_isSnake) CollisionOpponent?.Invoke();

                return false;
            }

            // Then check if the head of the snake is hitting an object
            if (i_indexCell == 10)
            {
                if (b_isSnake)
                {
                    CollisionApple?.Invoke();
                    i_Board[i_IndexCol, i_IndexLin] = 0;
                    return true;
                }
                else
                    return false;
            }
            return true;
        }
    }
}