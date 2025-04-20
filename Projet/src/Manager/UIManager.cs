using System.Numerics;
using Raylib_cs;

public static class UI_Board_Sprite
{
    static Dictionary<int, Color> colorCell = [];

    public static void InitUI_Board_Sprite()
    {
        colorCell.Add(5, Color.Orange);             // Bonus
        colorCell.Add(6, Color.Brown);              // Malus object 
    }

    public static Color GetColorCell(int indexCell)
    {
        return colorCell[indexCell];
    }
}

public static class UIManager
{
    public static void DrawBoard(Character characterOrigin)
    {

        // Method linked to Board
        UIGrid.DisplayBoardOutline(characterOrigin);
        UIGrid.DisplayGrid(characterOrigin);

        // Method linked to Info
        UIInfoBoard.DrawTimer(characterOrigin);
        UIInfoBoard.DrawPosition(characterOrigin);
        if (characterOrigin.IsPlayer())
        {
            UIInfoGame.DrawNbPlayerRemaining();
            UIPower.DrawPowerDisplayed(characterOrigin);
        }

    }
}