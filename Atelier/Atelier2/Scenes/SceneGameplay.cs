using Raylib_cs;

public class SceneGameplay : Scene
{
    private float timer;
    public SceneGameplay()
    {
        GameState.Instance.debugMagic.Clear();
    }

    public override void Draw()
    {
        Raylib.DrawText("GAMEPLAY", 5, 5, 35, Color.Black);
        base.Draw();
    }

    public override void Update()
    {
        timer += 0.5f;

#if DEBUG
        GameState.Instance.debugMagic.AddOption("FPS", Raylib.GetFPS());
        GameState.Instance.debugMagic.AddOption("Le timer", timer);
        GameState.Instance.debugMagic.AddOption("Nom", this.name);
#endif

        if (Raylib.IsKeyPressed(KeyboardKey.Escape))
        {
            GameState.Instance.ChangeScene("menu");
        }
        base.Update();
    }
}