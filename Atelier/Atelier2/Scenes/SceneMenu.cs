using Raylib_cs;

public class SceneMenu : Scene
{
    private Button playButton;
    private Button optionsButton;
    private Button quitButton;
    private ButtonsList buttonsList = new ButtonsList();
    public SceneMenu()
    {
        playButton = new Button { Rect = new Rectangle(10, 40, 200, 40), Text = "Jouer", Color = Color.White };
        optionsButton = new Button { Rect = new Rectangle(10, playButton.Rect.Y + playButton.Rect.Height + 5, 200, 40), Text = "Options", Color = Color.White };
        quitButton = new Button { Rect = new Rectangle(10, optionsButton.Rect.Y + optionsButton.Rect.Height + 5, 200, 40), Text = "Quitter", Color = Color.White };
        buttonsList.AddButton(playButton);
        buttonsList.AddButton(optionsButton);
        buttonsList.AddButton(quitButton);
    }
    public override void Draw()
    {
        Raylib.DrawText("MENU", 5, 5, 20, Color.Black);

        buttonsList.Draw();

        base.Draw();
    }

    public override void Update()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            GameState.Instance.ChangeScene("gameplay");
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.O))
        {
            GameState.Instance.ChangeScene("options");
        }

        buttonsList.Update();
        if (playButton.IsClicked)
        {
            GameState.Instance.ChangeScene("gameplay");
        }
        else if (optionsButton.IsClicked)
        {
            GameState.Instance.ChangeScene("options");
        }
        else if (quitButton.IsClicked)
        {
            Raylib.CloseWindow();
        }
        base.Update();
    }
}