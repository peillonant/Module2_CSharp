using System.Diagnostics;
using Raylib_cs;

public static class Game
{
    private static readonly ScenesManager _scenesManager = new();  

    public static void Main()
    {
        Raylib.InitWindow(1920, 1080, "Snake");
        Raylib.SetTargetFPS(60);
                
        _scenesManager.Load<MenuScene>();

        Debug.WriteLine("Lancement du programme");

        while (!Raylib.WindowShouldClose())
        {

            _scenesManager.Update();
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RayWhite);
            _scenesManager.Draw();
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}