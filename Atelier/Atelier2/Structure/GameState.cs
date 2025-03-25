
using System.Diagnostics;
using Raylib_cs;

public class GameState
{
    private Scene? currentScene;
    private static GameState? instance;
    public static GameState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameState();
            }
            return instance;
        }
    }

    public int i_ScreenWidth; 
    public int i_ScreenHeight;

    private Dictionary<string, Scene> scenes;

    public DebugMagic debugMagic = new DebugMagic();

    public float masterVolume { get; private set; }
    public bool fullScreen { get; private set; }

    OptionsFile optionsFile = new OptionsFile();

    public void SetVolume(float volume)
    {
        masterVolume = volume;
        Raylib.SetMasterVolume(masterVolume);
    }

    public GameState()
    {
        scenes = new Dictionary<string, Scene>();

        OptionsFile optionsFile = new OptionsFile();
        optionsFile.Load();

        if (optionsFile.IsOptionExists("volume"))
        {
            float volume = optionsFile.GetOptionFloat("volume");
            masterVolume = volume;
        }
        else
        {
            masterVolume = 0.8f;
        }

        fullScreen = optionsFile.GetOptionBool("fullscreen");
        if (fullScreen)
        {
            Debug.WriteLine("Passe en plein écran");
            //Raylib.ToggleFullscreen();
        }
    }

    public void RegisterScene(string name, Scene scene)
    {
        scenes[name] = scene;
        scene.name = name;
    }

    public void RemoveScene(string name)
    {
        if (scenes.ContainsKey(name))
        {
            scenes[name].Close();
            scenes.Remove(name);
        }
    }

    public void ChangeScene(string name)
    {
        if (scenes.ContainsKey(name))
        {
            if (currentScene != null)
            {
                Debug.WriteLine($"La scene {currentScene.name} se cache");
                currentScene.Hide();
            }
            currentScene = scenes[name];
            currentScene.Show();
            Debug.WriteLine("Change scene vers " + name);
        }
        else
        {
            string error = $"Scene {name} non trouvée dans le dictionnaire";
            Debug.WriteLine(error);
            throw new Exception(error);
        }
    }

    public void UpdateScene()
    {
        currentScene?.Update();
    }

    public void DrawScene()
    {
        currentScene?.Draw();
    }

    public void Close()
    {
        foreach (var item in scenes)
        {
            Debug.WriteLine($"La scene {item.Value.name} est détruite");
            item.Value.Close();
        }
    }
}