using System.Runtime.CompilerServices;
using Raylib_cs;

public class Player : Character
{

    public Player ()
    {
        // Initiation of the board
        characterBoard = new Board(true);

        // Position the Snake on the playerBoard at the beginning
        characterBoard.AddObject(new(9, 9), 1);

        // 1 = Top, 2 = Right, 3 = Bottom, 4 = Left
        characterSnake = new(Raylib.GetRandomValue(1, 4));         

        // Init the UI_Board linked to the User
        UI_Board = new(ref characterBoard);
        
        SubscriptionEvent();
    }

    public override void UpdatePlayer()
    {
        InputManagerUser();
        base.UpdatePlayer();
    }

    private void InputManagerUser()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.W) || Raylib.IsKeyPressed(KeyboardKey.Up))
            characterSnake.ChangeDirection(1);
        if (Raylib.IsKeyPressed(KeyboardKey.D) || Raylib.IsKeyPressed(KeyboardKey.Right))
            characterSnake.ChangeDirection(2);
        if (Raylib.IsKeyPressed(KeyboardKey.S) || Raylib.IsKeyPressed(KeyboardKey.Down))
            characterSnake.ChangeDirection(3);
        if (Raylib.IsKeyPressed(KeyboardKey.A) || Raylib.IsKeyPressed(KeyboardKey.Left))
            characterSnake.ChangeDirection(4);

        if (Raylib.IsKeyPressed(KeyboardKey.Escape))
            GameState.Instance.ChangeScene("menu");
    }

    public override void DrawBoard()
    {
        UI_Board.DrawBoard();
        UI_Board.DrawInfo(this);
    }    
}