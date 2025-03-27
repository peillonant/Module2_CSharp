using Raylib_cs;

public class SceneGameplay : Scene
{
    private bool b_LaunchNewGame = true;

    public SceneGameplay()
    {
        GameState.Instance.debugMagic.Clear();
    }

    public override void Show()
    {
        if (b_LaunchNewGame)
        {
            GameManager.Instance.InitGame();
        }
        
        base.Show();
    }

    public override void Draw()
    {
        GameManager.Instance.DrawGame();
        base.Draw();
    }

    public override void Update()
    {
        GameManager.Instance.UpdateGameManager();
        base.Update();
    }
}