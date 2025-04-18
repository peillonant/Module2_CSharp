using Raylib_cs;

public interface IAssetsManager
{
    public T Get<T>(string name);
    public Texture2D GetTextureFromSet(string textureSetName, int id);
}

public class AssetsManager : IAssetsManager
{
    private Dictionary<string, Texture2D> textures = new();
    private Dictionary<string, Font> fonts = new();
    private Dictionary<string, TextureSet> textureSets = new();


    public AssetsManager()
    {
        Services.Register<IAssetsManager>(this);
    }

    public void Load<T>(string name, string path)
    {
        Type assetType = typeof(T);

        switch (assetType)
        {
            case Type _ when assetType == typeof(Texture2D):
                textures[name] = Raylib.LoadTexture(path);
                break;
            case Type _ when assetType == typeof(Font):
                fonts[name] = Raylib.LoadFont(path);
                break;
            default:
                throw new ArgumentException($"Type {assetType} not supported.");
        }
    }

    public void AddTextureSet(string textureSetName, List<(int id, string path)> texturesInfos)
    {
        TextureSet textureSet = new TextureSet();
        textureSet.texturesInfos = new Dictionary<int, Texture2D>();
        foreach (var (id, path) in texturesInfos)
        {
            textureSet.texturesInfos[id] = Raylib.LoadTexture(path);
        }
        textureSets.Add(textureSetName, textureSet);
    }

    public Texture2D GetTextureFromSet(string textureSetName, int id)
    {
        return textureSets[textureSetName].texturesInfos[id];
    }

    public T Get<T>(string name)
    {
        Type type = typeof(T);
        return type switch
        {
            Type _ when type == typeof(Texture2D) => (T)(object)textures[name],
            Type _ when type == typeof(Font) => (T)(object)fonts[name],
            _ => throw new KeyNotFoundException($"Asset '{name}' of type {type} not found.")
        };
    }

    private struct TextureSet
    {
        public Dictionary<int, Texture2D> texturesInfos;
    }
}