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
        //Raylib.DrawText(GameManager.Instance.GetDifficultyGame().ToString(), 10, 10, 30, Color.Black);
        GameManager.Instance.DrawGame();
        base.Draw();
    }

    public override void Update()
    {
        GameManager.Instance.UpdateGameManager();
        base.Update();
    }
}