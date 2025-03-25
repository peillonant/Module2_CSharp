using Raylib_cs;
using System.Numerics;


public class SceneDifficulty : Scene
{
    private int i_ButtonWidht = 200;
    private int i_ButtonHeight = 40;

    private readonly Button easyButton;
    private readonly Button mediumButton;
    private readonly Button hardButton;
    private readonly Button extremButton;
    private readonly Button backButton;

    private ButtonsList buttonsList_Difficulty = new ButtonsList();

    public SceneDifficulty()
    {
        easyButton = new Button { Rect = new Rectangle((Raylib.GetScreenWidth() - i_ButtonWidht) /2 , ((Raylib.GetScreenHeight() - i_ButtonHeight) /2 ) - i_ButtonHeight - 5, i_ButtonWidht, i_ButtonHeight), Text = "Easy", Color = Color.White };
        mediumButton = new Button { Rect = new Rectangle((Raylib.GetScreenWidth() - i_ButtonWidht) /2, (Raylib.GetScreenHeight() - i_ButtonHeight) /2 , i_ButtonWidht, i_ButtonHeight), Text = "Medium", Color = Color.White };
        hardButton = new Button { Rect = new Rectangle((Raylib.GetScreenWidth() - i_ButtonWidht) /2, ((Raylib.GetScreenHeight() - i_ButtonHeight) /2 ) + i_ButtonHeight + 5, i_ButtonWidht, i_ButtonHeight), Text = "Hard", Color = Color.White };
        extremButton = new Button { Rect = new Rectangle((Raylib.GetScreenWidth() - i_ButtonWidht) /2, ((Raylib.GetScreenHeight() - i_ButtonHeight) /2 ) + (i_ButtonHeight + 5) * 2, i_ButtonWidht, i_ButtonHeight), Text = "Extrem", Color = Color.White };
        backButton = new Button { Rect = new Rectangle((Raylib.GetScreenWidth() - i_ButtonWidht) /2, ((Raylib.GetScreenHeight() - i_ButtonHeight) /2 ) + (i_ButtonHeight + 5) *3, i_ButtonWidht, i_ButtonHeight), Text = "Back", Color = Color.White };

        buttonsList_Difficulty.AddButton(easyButton);
        buttonsList_Difficulty.AddButton(mediumButton);
        buttonsList_Difficulty.AddButton(hardButton);
        buttonsList_Difficulty.AddButton(extremButton);
        buttonsList_Difficulty.AddButton(backButton);
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
        buttonsList_Difficulty.Update();
        if (easyButton.IsClicked || Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            GameInfo.Instance.SetDifficultyGame(1);
            GameState.Instance.ChangeScene("gameplay");
        }
        else if (mediumButton.IsClicked)
        {
            GameInfo.Instance.SetDifficultyGame(2);
            Console.WriteLine("Medium Button has been clicked");
            GameState.Instance.ChangeScene("gameplay");
        }
        else if (hardButton.IsClicked)
        {
            GameInfo.Instance.SetDifficultyGame(3);
            Console.WriteLine("Hard Button has been clicked");
            GameState.Instance.ChangeScene("gameplay");
        }
        else if (extremButton.IsClicked)
        {
            GameInfo.Instance.SetDifficultyGame(4);
            Console.WriteLine("Extrem Button has been clicked");
            GameState.Instance.ChangeScene("gameplay");
        }
        else if (backButton.IsClicked || Raylib.IsKeyPressed(KeyboardKey.Escape))
        {
            GameState.Instance.ChangeScene("menu");
        }
        base.Update();
    }

    // Game loop, Draw on the Screen
    public override void Draw()
    {
        string s_textTitle = "DIFFICULTY";
        Vector2 v2_sizeTitle = Raylib.MeasureTextEx(Raylib.GetFontDefault(), s_textTitle, 20, 1);
        int i_px = Raylib.GetScreenWidth() /2 - (int) (v2_sizeTitle.X / 2);
        int i_py = (Raylib.GetScreenHeight() - i_ButtonHeight) /2 - i_ButtonHeight * 2 - 5; 
        
        Raylib.DrawText(s_textTitle, i_px, i_py, 20, Color.Black);

        buttonsList_Difficulty.Draw();
        base.Draw();
    }

    // When we close the window (Unload the prog)
    public override void Close()
    {
        base.Close();
    }
}