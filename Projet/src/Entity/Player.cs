using System.Runtime.CompilerServices;
using Raylib_cs;

public class Player : Character
{
    private float f_Timer;
    private bool b_CanMove;
    private int i_newDirection;

    public Player() : base()
    {
        i_newDirection = characterSnake.GetDirection();
        b_IsDisplayed = true;
        b_isPlayer = true;
    }

    public override void UpdatePlayer()
    {
        InputManagerUser();
        base.UpdatePlayer();
    }

    private void InputManagerUser()
    {
        b_CanMove = GenericFunction.UpdateTimer(ref f_Timer);

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
        GameManager.GameLostTimer();
    }

    protected override void OnCollisionLost()
    {
        GameManager.GameLostCollision();
    }
}