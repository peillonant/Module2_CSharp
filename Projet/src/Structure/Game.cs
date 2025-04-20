using System.Diagnostics;
using Raylib_cs;

public static class Game
{
    private static readonly ScenesManager _scenesManager = new();
    private static readonly AssetsManager _assetsManager = new();

    public static bool b_CloseWindow = false;

    public static void Main()
    {
        Raylib.InitWindow(1280, 720, "Slither Clash");
        Raylib.SetWindowState(ConfigFlags.ResizableWindow);
        Raylib.SetTargetFPS(60);
        Raylib.SetExitKey(KeyboardKey.Null);

        LoadAsset.Load(_assetsManager);
        _scenesManager.Show<SceneMenu>();

        // Init Sprite and color linked to the UI
        UI_Board_Sprite.InitUI_Board_Sprite();
        UINotification_Color.InitUI_Notification_Color();

        while (!Raylib.WindowShouldClose() && !b_CloseWindow)
        {
            _scenesManager.UpdateScene();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RayWhite);

            _scenesManager.DrawScene();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}