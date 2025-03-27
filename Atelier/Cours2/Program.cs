using System.Diagnostics;
using Raylib_cs;

public static class Program
{
    private static void Main()
    {
        int i_ScreenWidth = 1280;
        int i_ScreenHight = 1000;

        Raylib.InitWindow(i_ScreenWidth, i_ScreenHight, "Jeu de la vie");
        Raylib.SetWindowState(ConfigFlags.ResizableWindow);
        Raylib.SetTargetFPS(60);

        GameManager gameManager = new GameManager();

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RayWhite);

            gameManager.UpdateGame();
            gameManager.DrawGame();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}