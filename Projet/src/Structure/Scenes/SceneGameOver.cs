using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

public class SceneGameOver : Scene
{
    string s_TextScene = "GAMEOVER";
    string s_TextButton = "Back";
    int i_PXText, i_PXButton;
    int i_PYText, i_PYButton;
    Button backButton = new();

    private ButtonsList buttonsList = new ButtonsList();

    public SceneGameOver()
    {
        Vector2 v2_TextSceneSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), s_TextScene, 50, 1);

        i_PXText = (int) ((Raylib.GetScreenWidth() - v2_TextSceneSize.X) / 2);
        i_PYText = (int) ((Raylib.GetScreenHeight() - v2_TextSceneSize.Y) / 2) - 40;

        i_PXButton = (Raylib.GetScreenWidth() - 200) / 2;
        i_PYButton = (Raylib.GetScreenHeight() - 40) / 2 + 100;
        
        backButton.Rect = new Rectangle(i_PXButton, i_PYButton, 200, 40);
        backButton.Text = s_TextButton;
        backButton.Color = Color.White;

        buttonsList.AddButton(backButton);
    }

    // Methode qui s'execute lorsque la scene vient à l'écran
    public override void Show()
    {
        base.Show();
    }

    // Méthode lorsque la scene est caché par une autre scene
    public override void Hide()
    {
        base.Hide();
    }

    // Game loop, Update variable
    public override void Update()
    {
        buttonsList.Update();

        if (backButton.IsClicked || Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            GameState.ChangeScene("menu");
        }

        base.Update();
    }

    // Game loop, Draw on the Screen
    public override void Draw()
    {
        Raylib.DrawText(s_TextScene, i_PXText, i_PYText, 50, Color.Black);

        buttonsList.Draw();

        base.Draw();
    }

    // When we close the window (Unload the prog)
    public override void Close()
    {
        Debug.WriteLine("Destruction de la scene win");
        base.Close();
    }
}