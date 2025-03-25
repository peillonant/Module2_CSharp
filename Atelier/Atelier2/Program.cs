using System.Diagnostics;
using Raylib_cs;

public static class Program
{
    static SceneMenu sceneMenu = new();
    static SceneGameplay sceneGameplay = new();
    static SceneOptions sceneOptions = new();

    static SceneSplash sceneSplash = new();

    public static void Main()
    {
        Raylib.InitWindow(800, 600, "Raylib-cs Framework Gamecodeur");
        Raylib.SetWindowState(ConfigFlags.ResizableWindow);
        Raylib.SetTargetFPS(60);

        Raylib.InitAudioDevice();
        Raylib.SetExitKey(KeyboardKey.Q);
        Raylib.SetExitKey(KeyboardKey.A);

        GameState gameState = GameState.Instance;
        gameState.RegisterScene("splash", sceneSplash);
        gameState.RegisterScene("menu", sceneMenu);
        gameState.RegisterScene("gameplay", sceneGameplay);
        gameState.RegisterScene("options", sceneOptions);

        gameState.ChangeScene("splash");

        gameState.i_ScreenWidth = Raylib.GetScreenWidth();
        gameState.i_ScreenHeight = Raylib.GetScreenHeight();

        Debug.WriteLine("Lancement du programme");

        while (!Raylib.WindowShouldClose())
        {
            gameState.debugMagic.Update();
            gameState.UpdateScene();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RayWhite);

            gameState.DrawScene();

#if DEBUG
            gameState.debugMagic.Draw();
#endif

            Raylib.EndDrawing();
        }

        //gameState.RemoveScene("menu");
        //gameState.RemoveScene("gameplay");
        //gameState.RemoveScene("options");
        gameState.Close();

        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }
}