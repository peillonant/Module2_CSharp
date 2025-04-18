using System.Numerics;
using Raylib_cs;

public class SceneMenu : Scene
{
    private int i_ButtonWidht = 200;
    private int i_ButtonHeight = 40;

    private Button? playButton, optionsButton, quitButton;
    private ButtonsList buttonsList_Menu = new ButtonsList();

    public override void Show()
    {
        // Managing the Main Menu
        playButton = new Button { Rect = new Rectangle((Raylib.GetScreenWidth() - i_ButtonWidht) /2 , ((Raylib.GetScreenHeight() - i_ButtonHeight) /2 ) - i_ButtonHeight - 5, i_ButtonWidht, i_ButtonHeight), Text = "Local Game", Color = Color.White };
        optionsButton = new Button { Rect = new Rectangle((Raylib.GetScreenWidth() - i_ButtonWidht) /2, (Raylib.GetScreenHeight() - i_ButtonHeight) /2 , i_ButtonWidht, i_ButtonHeight), Text = "Option", Color = Color.White };
        quitButton = new Button { Rect = new Rectangle((Raylib.GetScreenWidth() - i_ButtonWidht) /2, ((Raylib.GetScreenHeight() - i_ButtonHeight) /2 ) + i_ButtonHeight + 5, i_ButtonWidht, i_ButtonHeight), Text = "Quit", Color = Color.White };

        buttonsList_Menu.AddButton(playButton);
        buttonsList_Menu.AddButton(optionsButton);
        buttonsList_Menu.AddButton(quitButton);
    }

    public override void Hide()
    {
        playButton = null;
        optionsButton = null;
        quitButton = null;
        buttonsList_Menu.ClearList();
    }

    public override void Update()
    {
        if (playButton == null || optionsButton == null || quitButton == null)
            return;

        buttonsList_Menu.Update();

        if (playButton.IsClicked || Raylib.IsKeyPressed(KeyboardKey.Space))
            Services.Get<IScenesManager>().Show<SceneDifficulty>(); 

        else if (optionsButton.IsClicked || Raylib.IsKeyPressed(KeyboardKey.O))
            Services.Get<IScenesManager>().Show<SceneOptions>(); 

        else if (quitButton.IsClicked)
            Game.b_CloseWindow = true;
    }

     public override void Draw()
    {
        string s_textTitle = "MENU";
        Vector2 v2_sizeTitle = Raylib.MeasureTextEx(Raylib.GetFontDefault(), s_textTitle, 20, 1);
        int i_px = Raylib.GetScreenWidth() /2 - (int) (v2_sizeTitle.X / 2);
        int i_py = (Raylib.GetScreenHeight() - i_ButtonHeight) /2 - i_ButtonHeight * 2 - 5; 
        
        Raylib.DrawText(s_textTitle, i_px, i_py, 20, Color.Black);

        buttonsList_Menu.Draw();
    }

}