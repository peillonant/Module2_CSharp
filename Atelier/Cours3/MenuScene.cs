using Raylib_cs;

class MenuScene : Scene
{
    public override void Draw()
    {
        Raylib.DrawText("MenuScene.Draw", 10, 10, 20, Color.Black);
    }

    public override void Load()
    {
        Console.WriteLine("MenuScene.Load");
    }

    public override void Unload()
    {
        Console.WriteLine("MenuScene.Unload");
    }

    public override void Update()
    {
       if (Raylib.IsKeyDown(KeyboardKey.Enter))
       {
            var sm = Services.Get<ScenesManager>();
            sm.Load<SceneTest>();
       }
    }
}