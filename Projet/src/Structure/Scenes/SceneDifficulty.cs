using Raylib_cs;
using System.Numerics;


public class SceneDifficulty : Scene
{
    private int i_ButtonWidht = 200;
    private int i_ButtonHeight = 40;

    private Button? easyButton, mediumButton;
    //private readonly Button hardButton;
    //private readonly Button extremButton;
    private Button? backButton;

    private ButtonsList buttonsList_Difficulty = new ButtonsList();

    // Methode qui s'execute lorsque la scene vient à l'écran
    public override void Show()
    {
        easyButton = new Button { Rect = new Rectangle((Raylib.GetScreenWidth() - i_ButtonWidht) / 2, ((Raylib.GetScreenHeight() - i_ButtonHeight) / 2) - i_ButtonHeight - 5, i_ButtonWidht, i_ButtonHeight), Text = "Easy", Color = Color.White };
        mediumButton = new Button { Rect = new Rectangle((Raylib.GetScreenWidth() - i_ButtonWidht) / 2, (Raylib.GetScreenHeight() - i_ButtonHeight) / 2, i_ButtonWidht, i_ButtonHeight), Text = "Medium", Color = Color.White };
        backButton = new Button { Rect = new Rectangle((Raylib.GetScreenWidth() - i_ButtonWidht) / 2, ((Raylib.GetScreenHeight() - i_ButtonHeight) / 2) + i_ButtonHeight + 5, i_ButtonWidht, i_ButtonHeight), Text = "Back", Color = Color.White };

        //hardButton = new Button { Rect = new Rectangle((Raylib.GetScreenWidth() - i_ButtonWidht) / 2, ((Raylib.GetScreenHeight() - i_ButtonHeight) / 2) + i_ButtonHeight + 5, i_ButtonWidht, i_ButtonHeight), Text = "Hard", Color = Color.White };
        //extremButton = new Button { Rect = new Rectangle((Raylib.GetScreenWidth() - i_ButtonWidht) / 2, ((Raylib.GetScreenHeight() - i_ButtonHeight) / 2) + (i_ButtonHeight + 5) * 2, i_ButtonWidht, i_ButtonHeight), Text = "Extrem", Color = Color.White };
        //backButton = new Button { Rect = new Rectangle((Raylib.GetScreenWidth() - i_ButtonWidht) / 2, ((Raylib.GetScreenHeight() - i_ButtonHeight) / 2) + (i_ButtonHeight + 5) * 3, i_ButtonWidht, i_ButtonHeight), Text = "Back", Color = Color.White };

        buttonsList_Difficulty.AddButton(easyButton);
        buttonsList_Difficulty.AddButton(mediumButton);
        //buttonsList_Difficulty.AddButton(hardButton);
        //buttonsList_Difficulty.AddButton(extremButton);
        buttonsList_Difficulty.AddButton(backButton);
    }

    // Méthode lorsque la scene est caché par une autre scene
    public override void Hide()
    {
        easyButton = null;
        mediumButton = null;
        backButton = null;
        buttonsList_Difficulty.ClearList();
    }

    // Game loop, Update variable
    public override void Update()
    {
        if (easyButton == null || mediumButton == null || backButton == null)
            return;

        buttonsList_Difficulty.Update();
        
        if (easyButton.IsClicked || Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            GameInfo.SetDifficultyGame(1);
            Services.Get<IScenesManager>().Show<SceneGameplay>(); 
        }
        else if (mediumButton.IsClicked)
        {
            GameInfo.SetDifficultyGame(2);
            Services.Get<IScenesManager>().Show<SceneGameplay>(); 
        }
        else if (backButton.IsClicked || Raylib.IsKeyPressed(KeyboardKey.Escape))
            Services.Get<IScenesManager>().Show<SceneMenu>(); 

    }

    // Game loop, Draw on the Screen
    public override void Draw()
    {
        string s_textTitle = "DIFFICULTY";
        Vector2 v2_sizeTitle = Raylib.MeasureTextEx(Raylib.GetFontDefault(), s_textTitle, 20, 1);
        int i_px = Raylib.GetScreenWidth() / 2 - (int)(v2_sizeTitle.X / 2);
        int i_py = (Raylib.GetScreenHeight() - i_ButtonHeight) / 2 - i_ButtonHeight * 2 - 5;

        Raylib.DrawText(s_textTitle, i_px, i_py, 20, Color.Black);

        buttonsList_Difficulty.Draw();
    }
}