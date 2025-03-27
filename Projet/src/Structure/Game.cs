using System.Diagnostics;
using Raylib_cs;

public static class Game
{
    static SceneMenu sceneMenu = new();
    static SceneGameplay sceneGameplay = new();
    static SceneOptions sceneOptions = new();
    static SceneDifficulty sceneDifficulty = new();
    static SceneWin sceneWin = new();
    static SceneGameOver sceneGameOver = new();

    public static void Main()
    {
        Raylib.InitWindow(1280, 720, "Slither Clash");
        Raylib.SetWindowState(ConfigFlags.ResizableWindow);
        Raylib.SetTargetFPS(60);
        Raylib.SetExitKey(KeyboardKey.Null); 


        GameState gameState = GameState.Instance;
        gameState.RegisterScene("menu", sceneMenu);
        gameState.RegisterScene("gameplay", sceneGameplay);
        gameState.RegisterScene("difficulty", sceneDifficulty);
        gameState.RegisterScene("options", sceneOptions);
        gameState.RegisterScene("win", sceneWin);
        gameState.RegisterScene("gameover", sceneGameOver);

        gameState.ChangeScene("menu");

        Debug.WriteLine("Lancement du programme");

        while (!Raylib.WindowShouldClose())
        {
            gameState.debugMagic.Update();
            gameState.UpdateScene();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RayWhite);
            
            gameState.DrawScene();

            Raylib.EndDrawing();
        }

        gameState.RemoveScene("menu");
        gameState.RemoveScene("gameplay");
        gameState.RemoveScene("options");
        gameState.Close();

        Raylib.CloseWindow();
    }
}