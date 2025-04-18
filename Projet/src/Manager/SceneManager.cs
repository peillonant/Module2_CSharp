
public interface IScenesManager
{
    public void Show<T>() where T : Scene, new();
}

public class ScenesManager : IScenesManager
{
    private Scene? _currentScene;

    public ScenesManager()
    {
        Services.Register<IScenesManager>(this);
    }

    public void Show<T>() where T : Scene, new()
    {
        _currentScene?.Hide();
        _currentScene = new T();
        _currentScene.Show();
    }

    public void UpdateScene()
    {
        _currentScene?.Update();
    }

    public void DrawScene()
    {
        _currentScene?.Draw();
    }
}