using System.Diagnostics;
using Raylib_cs;

public class SceneOptions : Scene
{
    Button? backButton;

    private ButtonsList buttonsList = new ButtonsList();

    public override void Show()
    {
        backButton = new Button
        {
            Rect = new Rectangle(10, 90, 200, 40),
            Text = "Back",
            Color = Color.White
        };

        buttonsList.AddButton(backButton);
    }

    public override void Hide()
    {
        backButton = null;
        buttonsList.ClearList();
    }

    public override void Update()
    {
        if (backButton == null) return;

        buttonsList.Update();

        if (backButton.IsClicked && GameInfo.GetLaunchNewGame())
            Services.Get<IScenesManager>().Show<SceneMenu>(); 
                
        if (backButton.IsClicked && !GameInfo.GetLaunchNewGame())
            Services.Get<IScenesManager>().Show<ScenePause>(); 
        
    }

    public override void Draw()
    {
        Raylib.DrawText("OPTIONS", 5, 5, 25, Color.Black);
        int screenWidth = Raylib.GetScreenWidth();
        Raylib.DrawLine(0, 30, screenWidth, 30, Color.Black);

        int pourcent = 100;
        Raylib.DrawText($"Volume : {pourcent} %", 10, 35, 20, Color.Black);
        
        buttonsList.Draw();
    }
}