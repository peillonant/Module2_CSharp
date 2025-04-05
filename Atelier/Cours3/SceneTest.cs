using Raylib_cs;

class SceneTest : Scene
{
    public override void Draw()
    {
        Raylib.DrawText("SceneTest", 10 , 10, 20, Color.Black);
    }

    public override void Load()
    {
        Console.WriteLine("SceneTest.Load");
    }

    public override void Unload()
    {
        Console.WriteLine("SceneTest.Unload");
    }

    public override void Update()
    {
        if (Raylib.IsKeyDown(KeyboardKey.Enter))
       {
            var sm = Services.Get<ISceneManager>();
            sm.Load<MenuScene>();

            // On peut pas appeler l'update car c'est l'interface qui nous donne accès la méthode       sm.Update<MenuScene>(); 
       }
    }
}