using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using Raylib_cs;

public interface IBouncing
{
    protected void Bouncing();
}

public class Ball : IBouncing
{
    private float f_Radius;
    private Vector2 v2_Position;
    private Color color;
    private int i_DirectionX;
    private int i_DirectionY;
    private int i_Speed;

    public Ball(float f_newRadius, Color newColor)
    {
        f_Radius = f_newRadius;
        int i_PX = Raylib.GetRandomValue((int) f_Radius, (int) (Raylib.GetScreenWidth() - f_Radius));
        int i_PY = Raylib.GetRandomValue((int) f_Radius, (int) (Raylib.GetScreenHeight() - f_Radius));
        v2_Position = new (i_PX, i_PY);
        color = newColor;
        i_Speed = Raylib.GetRandomValue(1,5);
        i_DirectionX = (Raylib.GetRandomValue(1,2) == 1) ? -1 : 1;
        i_DirectionY = (Raylib.GetRandomValue(1,2) == 1) ? -1 : 1;
    }

    public void Bouncing()
    {
        if (v2_Position.X <= f_Radius || v2_Position.X >= Raylib.GetScreenWidth() - f_Radius) i_DirectionX *= -1;
        if (v2_Position.Y <= f_Radius || v2_Position.Y >= Raylib.GetScreenHeight() - f_Radius) i_DirectionY *= -1;
    }

    public void UpdateBall()
    {
        v2_Position += new Vector2 (i_DirectionX * i_Speed, i_DirectionY * i_Speed);
        Bouncing();
    }

    public void DrawBall() => Raylib.DrawCircleV(v2_Position, f_Radius, color);
}


public static class Program
{
    public static void Main()
    {
        const int screenWidth = 800;
        const int screenHeight = 450;

        Raylib.InitWindow(screenWidth, screenHeight, "raylib [core] example - basic window");
        Raylib.SetTargetFPS(60);                             // Set our game to run at 60 frames-per-second

        Ball ball1 = new(10f, Color.Blue);

        while(!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.RayWhite);

            ball1.UpdateBall();
            ball1.DrawBall();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}