using Raylib_cs;

public class SceneGameplay : Scene
{    
    public override void Show()
    {
        if (GameInfo.GetLaunchNewGame())
        {
            GameManager.ResetGame();
            GameManager.InitGame();
            GameInfo.SetLaunchNewGame(false);
        }
    }

    public override void Hide()
    {
    }

    public override void Draw()
    {
        GameManager.DrawGame();
    }

    public override void Update()
    {
        GameManager.UpdateGameManager();
    }
}