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
    static ScenePause scenePause = new();

    public static void Main()
    {
        Raylib.InitWindow(1280, 720, "Slither Clash");
        Raylib.SetWindowState(ConfigFlags.ResizableWindow);
        Raylib.SetTargetFPS(60);
        Raylib.SetExitKey(KeyboardKey.Null); 

        GameState.RegisterScene("menu", sceneMenu);
        GameState.RegisterScene("gameplay", sceneGameplay);
        GameState.RegisterScene("difficulty", sceneDifficulty);
        GameState.RegisterScene("options", sceneOptions);
        GameState.RegisterScene("win", sceneWin);
        GameState.RegisterScene("gameover", sceneGameOver);
        GameState.RegisterScene("pause", scenePause);

        GameState.ChangeScene("menu");

        Debug.WriteLine("Lancement du programme");

        while (!Raylib.WindowShouldClose())
        {
            GameState.UpdateScene();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RayWhite);
            
            GameState.DrawScene();

            Raylib.EndDrawing();
        }

        GameState.RemoveScene("menu");
        GameState.RemoveScene("gameplay");
        GameState.RemoveScene("options");
        GameState.RemoveScene("win");
        GameState.RemoveScene("gameover");
        GameState.RemoveScene("difficulty");
        GameState.Close();

        Raylib.CloseWindow();
    }
}