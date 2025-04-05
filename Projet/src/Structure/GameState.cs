
using System.Diagnostics;

public static class GameState
{
    private static Scene? currentScene;

    private static Dictionary<string, Scene> scenes = [];

    public static void RegisterScene(string name, Scene scene)
    {
        scenes[name] = scene;
        scene.name = name;
    }

    public static void RemoveScene(string name)
    {
        if (scenes.ContainsKey(name))
        {
            scenes[name].Close();
            scenes.Remove(name);
        }
    }

    public static void ChangeScene(string name)
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

    public static Scene RetrieveScene(string name)
    {
        return scenes[name];
    }

    public static void UpdateScene()
    {
        currentScene?.Update();
    }

    public static void DrawScene()
    {
        currentScene?.Draw();
    }

    public static void Close()
    {
        foreach (var item in scenes)
        {
            Debug.WriteLine($"La scene {item.Value.name} est détruite");
            item.Value.Close();
        }
    }
}