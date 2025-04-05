using System.Diagnostics;
using Raylib_cs;

public class SceneOptions : Scene
{

    Button backButton = new Button
    {
        Rect = new Rectangle(10, 90, 200, 40),
        Text = "Back",
        Color = Color.White
    };

    private ButtonsList buttonsList = new ButtonsList();

    private bool isFullScreen;

    public SceneOptions()
    {
        buttonsList.AddButton(backButton);
    }

    public override void Show()
    {
        base.Show();
    }

    public override void Update()
    {
        buttonsList.Update();

        // if (Raylib.IsKeyPressed(KeyboardKey.F))
        // {
        //     isFullScreen = !isFullScreen;
        // }

        if (backButton.IsClicked && b_FromMenu)
        {
            GameState.ChangeScene("menu");
        }
        
        if (backButton.IsClicked && !b_FromMenu)
        {
            GameState.ChangeScene("pause");
        }
        

        base.Update();
    }

    public override void Draw()
    {
        Raylib.DrawText("OPTIONS", 5, 5, 25, Color.Black);
        int screenWidth = Raylib.GetScreenWidth();
        Raylib.DrawLine(0, 30, screenWidth, 30, Color.Black);

        int pourcent = 100;
        Raylib.DrawText($"Volume : {pourcent} %", 10, 35, 20, Color.Black);
        
        buttonsList.Draw();

        base.Draw();
    }

    public override void Hide()
    {
        base.Hide();
    }

    public override void Close()
    {
        Debug.WriteLine("Destruction de la scene options");
        base.Close();
    }
}