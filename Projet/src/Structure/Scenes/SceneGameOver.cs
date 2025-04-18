using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

public class SceneGameOver : Scene
{
    string s_TextScene = "GAMEOVER";
    string s_TextButton = "Back";
    int i_PXText, i_PXButton;
    int i_PYText, i_PYButton;
    Button? backButton;

    private ButtonsList buttonsList = new ButtonsList();

    // Methode qui s'execute lorsque la scene vient à l'écran
    public override void Show()
    {
        Vector2 v2_TextSceneSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), s_TextScene, 50, 1);

        i_PXText = (int) ((Raylib.GetScreenWidth() - v2_TextSceneSize.X) / 2);
        i_PYText = (int) ((Raylib.GetScreenHeight() - v2_TextSceneSize.Y) / 2) - 40;

        i_PXButton = (Raylib.GetScreenWidth() - 200) / 2;
        i_PYButton = (Raylib.GetScreenHeight() - 40) / 2 + 100;

        backButton = new()
        {
            Rect = new Rectangle(i_PXButton, i_PYButton, 200, 40),
            Text = s_TextButton,
            Color = Color.White
        };

        buttonsList.AddButton(backButton);
    }

    // Méthode lorsque la scene est caché par une autre scene
    public override void Hide()
    {
        backButton = null;
        buttonsList.ClearList();
    }

    // Game loop, Update variable
    public override void Update()
    {
        if (backButton == null) return;

        buttonsList.Update();

        if (backButton.IsClicked || Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            GameInfo.SetLaunchNewGame(true);
            Services.Get<IScenesManager>().Show<SceneMenu>();    
        }
    }

    // Game loop, Draw on the Screen
    public override void Draw()
    {
        Raylib.DrawText(s_TextScene, i_PXText, i_PYText, 50, Color.Black);

        buttonsList.Draw();
    }
}