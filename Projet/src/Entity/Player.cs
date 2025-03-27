using System.Runtime.CompilerServices;
using Raylib_cs;

public class Player : Character
{
    private float f_Timer;
    private bool b_CanMove;
    private int i_newDirection;

    public Player ()
    {
        // Initiation of the board
        characterBoard = new Board(true);

        // Position the Snake on the playerBoard at the beginning
        characterBoard.AddObject(new(9, 9), 1);

        // 1 = Top, 2 = Right, 3 = Bottom, 4 = Left
        characterSnake = new(Raylib.GetRandomValue(1, 4));         
        i_newDirection = characterSnake.GetDirection();

        // Init the UI_Board linked to the User
        UI_Board = new(this);
        
        SubscriptionEvent();
    }

    public override void UpdatePlayer()
    {
        InputManagerUser();
        base.UpdatePlayer();
        UpdatePositionBoard();
    }

    private void InputManagerUser()
    {
        b_CanMove = GenericFunction.Instance.UpdateTimer(ref f_Timer);

        if (Raylib.IsKeyPressed(KeyboardKey.W) || Raylib.IsKeyPressed(KeyboardKey.Up) && characterSnake.GetDirection() != 3)
            i_newDirection = 1;
        if (Raylib.IsKeyPressed(KeyboardKey.D) || Raylib.IsKeyPressed(KeyboardKey.Right) && characterSnake.GetDirection() != 4)
            i_newDirection = 2;
        if (Raylib.IsKeyPressed(KeyboardKey.S) || Raylib.IsKeyPressed(KeyboardKey.Down) && characterSnake.GetDirection() != 1)
            i_newDirection = 3;
        if (Raylib.IsKeyPressed(KeyboardKey.A) || Raylib.IsKeyPressed(KeyboardKey.Left) && characterSnake.GetDirection() != 2)
            i_newDirection = 4;

        if (b_CanMove)
            characterSnake.ChangeDirection(i_newDirection);


        if (Raylib.IsKeyPressed(KeyboardKey.Escape))
            GameState.Instance.ChangeScene("menu");
    }

    protected override void OnTimerDown()
    {
        GameManager.Instance.GameLostTimer();
    }

    protected override void OnCollisionLost()
    {
        GameManager.Instance.GameLostCollision();
    }

    public override void DrawBoard()
    {
        UI_Board.DrawBoard();
    }
}