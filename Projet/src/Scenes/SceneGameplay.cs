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
            GameManager.InitGame();
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