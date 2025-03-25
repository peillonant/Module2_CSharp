using Raylib_cs;

public class SceneSplash : Scene
{
    Texture2D tex_Logo;
    float f_Timer = 0;

    public SceneSplash()
    {
        tex_Logo = Raylib.LoadTexture("Asset/images/player_1.png");
    }

    // Methode qui s'execute lorsque la scene vient à l'écran
    public override void Show()
    {
        f_Timer = 0;
        base.Show();
    }

    // Méthode lorsque la scene est caché par une autre scene
    public override void Hide()
    {
        base.Hide();
    }

    // Game loop, Update variable
    public override void Update()
    {
        f_Timer += Raylib.GetFrameTime();

        if (f_Timer >= 3)
        {
            GameState.Instance.ChangeScene("menu");
        }

        base.Update();
    }

    // Game loop, Draw on the Screen
    public override void Draw()
    {
        int i_ox = (GameState.Instance.i_ScreenWidth - tex_Logo.Width) / 2;
        int i_oy = (GameState.Instance.i_ScreenHeight - tex_Logo.Height) / 2;
        Raylib.DrawTexture(tex_Logo, i_ox, i_oy, Color.White);
        base.Draw();
    }

    // When we close the window (Unload the prog)
    public override void Close()
    {
        base.Close();
    }
}