using System.Numerics;
using Raylib_cs;

public class ScenePause : Scene
{
    private int i_ButtonWidht = 200;
    private int i_ButtonHeight = 40;

    private Button? resumeButton, optionsButton, backMenu, quitButton;
    private ButtonsList buttonsList_Menu = new ButtonsList();

    public override void Show()
    {
        // Managing the Main Menu
        resumeButton = new Button { Rect = new Rectangle((Raylib.GetScreenWidth() - i_ButtonWidht) /2 , ((Raylib.GetScreenHeight() - i_ButtonHeight) /2 ) - i_ButtonHeight - 5, i_ButtonWidht, i_ButtonHeight), Text = "Resume", Color = Color.White };
        optionsButton = new Button { Rect = new Rectangle((Raylib.GetScreenWidth() - i_ButtonWidht) /2, (Raylib.GetScreenHeight() - i_ButtonHeight) /2 , i_ButtonWidht, i_ButtonHeight), Text = "Option", Color = Color.White };
        backMenu = new Button { Rect = new Rectangle((Raylib.GetScreenWidth() - i_ButtonWidht) /2, ((Raylib.GetScreenHeight() - i_ButtonHeight) /2 ) + i_ButtonHeight + 5, i_ButtonWidht, i_ButtonHeight), Text = "Back Menu", Color = Color.White };
        quitButton = new Button { Rect = new Rectangle((Raylib.GetScreenWidth() - i_ButtonWidht) /2, ((Raylib.GetScreenHeight() - i_ButtonHeight) /2 ) + (i_ButtonHeight + 5) * 2, i_ButtonWidht, i_ButtonHeight), Text = "Quit", Color = Color.White };

        buttonsList_Menu.AddButton(resumeButton);
        buttonsList_Menu.AddButton(optionsButton);
        buttonsList_Menu.AddButton(backMenu);
        buttonsList_Menu.AddButton(quitButton);
    }

    public override void Hide()
    {
        resumeButton = null;
        optionsButton = null;
        backMenu = null; 
        quitButton = null;

        buttonsList_Menu.ClearList();
    }

    public override void Update()
    {
        if ( resumeButton == null || optionsButton == null || backMenu == null || quitButton == null) return;

        buttonsList_Menu.Update();
        if (resumeButton.IsClicked || Raylib.IsKeyPressed(KeyboardKey.Space))
            Services.Get<IScenesManager>().Show<SceneGameplay>(); 

        else if (optionsButton.IsClicked || Raylib.IsKeyPressed(KeyboardKey.O))
            Services.Get<IScenesManager>().Show<SceneOptions>(); 

        else if (backMenu.IsClicked || Raylib.IsKeyPressed(KeyboardKey.O))
        {
            Services.Get<IScenesManager>().Show<SceneMenu>();
            GameInfo.SetLaunchNewGame(true);
        }
        else if (quitButton.IsClicked)
            Game.b_CloseWindow = true;
    }

     public override void Draw()
    {
        string s_textTitle = "Pause";
        Vector2 v2_sizeTitle = Raylib.MeasureTextEx(Raylib.GetFontDefault(), s_textTitle, 20, 1);
        int i_px = Raylib.GetScreenWidth() /2 - (int) (v2_sizeTitle.X / 2);
        int i_py = (Raylib.GetScreenHeight() - i_ButtonHeight) /2 - i_ButtonHeight * 2 - 5; 
        
        Raylib.DrawText(s_textTitle, i_px, i_py, 20, Color.Black);

        buttonsList_Menu.Draw();
    }
}