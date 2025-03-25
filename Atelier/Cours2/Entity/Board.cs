using System.Diagnostics;
using Raylib_cs;

public class Board
{
    GameManager gameManager;

    public readonly int i_cellWidth = 16;
    public readonly int i_cellHeight = 16;
    public readonly int i_nbCellWidth;
    public readonly int i_nbCellHeight;
    public int[,] i_TabBoard;
    public int[,] i_TabBoardNewGen;

    public float f_Timer = 0;
    public float f_Delay = 1;
    
    public Board(GameManager gameManager)
    {
        i_nbCellWidth = ((int) Raylib.GetScreenWidth() / i_cellWidth) - 5;
        i_nbCellHeight = ((int) Raylib.GetScreenHeight() / i_cellHeight) - 5;

        i_TabBoard = new int[i_nbCellWidth, i_nbCellHeight];
        i_TabBoardNewGen = new int[i_nbCellWidth, i_nbCellHeight];

        this.gameManager = gameManager;

        InitBoard();
        InitBoardNewGen();
    }

    private void InitBoard()
    {
        for (int i = 0; i < i_nbCellHeight; i++ )
        {
            for (int j = 0; j < i_nbCellWidth; j++)
            {
                i_TabBoard[j,i] = 0;
            }
        }
    }

    private void InitBoardNewGen()
    {
        for (int i = 0; i < i_nbCellHeight; i++ )
        {
            for (int j = 0; j < i_nbCellWidth; j++)
            {
                i_TabBoardNewGen[j,i] = 0;
            }
        }
    }

    
    public void UpdateBoard()
    {
        if (CanMove())
        {
            CheckBoard();
            UpdateGeneration();
        }
    }

    private bool CanMove()
    {   
        f_Timer += Raylib.GetFrameTime();

        if (f_Timer >= (f_Delay / gameManager.i_SpeedIncrement))
        {
            f_Timer = 0;
            return true;
        }

        return false;
    }

    private void CheckBoard()
    {
        for (int i = 0; i < i_nbCellHeight; i++ )
        {
            for (int j = 0; j < i_nbCellWidth; j++)
            {
                int i_nbNeighboor = CheckNeighboor(i,j);

                if (i_nbNeighboor == 3)
                    i_TabBoardNewGen[j,i] = 1;
                else if (i_nbNeighboor == 2)
                    i_TabBoardNewGen[j,i] = i_TabBoard[j,i];
                else
                    i_TabBoardNewGen[j,i] = (i_TabBoardNewGen[j,i] == 1) ? 1 : 0;
            }
        }
    }

    private int CheckNeighboor(int i_indexCellHeight, int i_indexCellWidth)
    {
        int i_indexWidthMin = (i_indexCellWidth != 0) ? i_indexCellWidth - 1 : i_indexCellWidth;
        int i_indexWidthMax = (i_indexCellWidth != i_nbCellWidth - 1) ? i_indexCellWidth + 1 : i_indexCellWidth - 1;

        int i_indexHeightMin = (i_indexCellHeight != 0) ? i_indexCellHeight - 1 : i_indexCellHeight;
        int i_indexHeightMax = (i_indexCellHeight != i_nbCellHeight - 1) ? i_indexCellHeight + 1 : i_indexCellHeight - 1;

        int i_tmpNeighboor = 0;

        for (int i = i_indexHeightMin; i <= i_indexHeightMax; i++)
        {
            for (int j = i_indexWidthMin; j <=  i_indexWidthMax; j++)
            {
                if (i == i_indexCellHeight && j == i_indexCellWidth)
                    continue;

                if (i_TabBoard[j,i] == 1)
                    i_tmpNeighboor++;
            }
        }

        return i_tmpNeighboor;
    }

    private void UpdateGeneration()
    {
        for (int i = 0; i < i_nbCellHeight; i++ )
        {
            for (int j = 0; j < i_nbCellWidth; j++)
            {
                i_TabBoard[j,i] = i_TabBoardNewGen[j,i];
            }
        }    
    
        InitBoardNewGen();
    }
}