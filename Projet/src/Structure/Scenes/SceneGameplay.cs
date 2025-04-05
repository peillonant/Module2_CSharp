using Raylib_cs;

public class SceneGameplay : Scene
{
    private bool b_LaunchNewGame = true;
    
    public override void Show()
    {
        if (b_LaunchNewGame)
        {
            GameManager.InitGame();
            b_LaunchNewGame = false;
        }

        base.Show();
    }

    public override void Draw()
    {
        GameManager.DrawGame();
        base.Draw();
    }

    public override void Update()
    {
        GameManager.UpdateGameManager();
        base.Update();
    }
}