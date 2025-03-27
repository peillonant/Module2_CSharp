using Raylib_cs;

public class _SceneVide : Scene
{

    public _SceneVide()
    {
    
    }

    // Methode qui s'execute lorsque la scene vient à l'écran
    public override void Show()
    {
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
        base.Update();
    }

    // Game loop, Draw on the Screen
    public override void Draw()
    {
        base.Draw();
    }

    // When we close the window (Unload the prog)
    public override void Close()
    {
        base.Close();
    }
}