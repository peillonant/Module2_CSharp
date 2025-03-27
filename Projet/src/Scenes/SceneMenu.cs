using System.Numerics;
using Raylib_cs;

public class SceneMenu : Scene
{
    private int i_ButtonWidht = 200;
    private int i_ButtonHeight = 40;

    private Button playButton, optionsButton, quitButton;
    private ButtonsList buttonsList_Menu = new ButtonsList();

    public SceneMenu()
    {
        // Managing the Main Menu
        playButton = new Button { Rect = new Rectangle((Raylib.GetScreenWidth() - i_ButtonWidht) /2 , ((Raylib.GetScreenHeight() - i_ButtonHeight) /2 ) - i_ButtonHeight - 5, i_ButtonWidht, i_ButtonHeight), Text = "Jouer", Color = Color.White };
        optionsButton = new Button { Rect = new Rectangle((Raylib.GetScreenWidth() - i_ButtonWidht) /2, (Raylib.GetScreenHeight() - i_ButtonHeight) /2 , i_ButtonWidht, i_ButtonHeight), Text = "Options", Color = Color.White };
        quitButton = new Button { Rect = new Rectangle((Raylib.GetScreenWidth() - i_ButtonWidht) /2, ((Raylib.GetScreenHeight() - i_ButtonHeight) /2 ) + i_ButtonHeight + 5, i_ButtonWidht, i_ButtonHeight), Text = "Quitter", Color = Color.White };

        buttonsList_Menu.AddButton(playButton);
        buttonsList_Menu.AddButton(optionsButton);
        buttonsList_Menu.AddButton(quitButton);
    }
    public override void Update()
    {
        buttonsList_Menu.Update();
        if (playButton.IsClicked || Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            GameState.Instance.ChangeScene("difficulty");
        }
        else if (optionsButton.IsClicked || Raylib.IsKeyPressed(KeyboardKey.O))
        {
            GameState.Instance.ChangeScene("options");
        }
        else if (quitButton.IsClicked)
        {
            Raylib.CloseWindow();
        }
        base.Update();
    }

     public override void Draw()
    {
        string s_textTitle = "MENU";
        Vector2 v2_sizeTitle = Raylib.MeasureTextEx(Raylib.GetFontDefault(), s_textTitle, 20, 1);
        int i_px = Raylib.GetScreenWidth() /2 - (int) (v2_sizeTitle.X / 2);
        int i_py = (Raylib.GetScreenHeight() - i_ButtonHeight) /2 - i_ButtonHeight * 2 - 5; 
        
        Raylib.DrawText(s_textTitle, i_px, i_py, 20, Color.Black);

        buttonsList_Menu.Draw();
        base.Draw();
    }

}