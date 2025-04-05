public interface ISceneManager
{
    public void Load<T>() where T : Scene, new();
}

class ScenesManager : ISceneManager
{
    private Scene? _currentScene;

    public ScenesManager()
    {
        Services.Register<ISceneManager>(this);
    }

    public void Load<T>() where T : Scene, new()
    {
        _currentScene?.Unload();
        _currentScene = new T();
        _currentScene.Load();
    }

    public void Update() => _currentScene?.Update();

    public void Draw() => _currentScene?.Draw();
}
