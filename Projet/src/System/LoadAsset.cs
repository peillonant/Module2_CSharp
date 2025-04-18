using Raylib_cs;

public static class LoadAsset
{
    public static void Load(AssetsManager assetsManager)
    {
        assetsManager.AddTextureSet("Snake", new List<(int id, string path)>
        {
            (1,"Assets/Images/Snake/Snake8px.png"),
            (2,"Assets/Images/Snake/Snake16px.png"),
            (3,"Assets/Images/Snake/Snake24px.png")
        });

        assetsManager.AddTextureSet("BonusUI", new List<(int id, string path)>
        {
            (1,"Assets/Images/Power/Icon/Sprite/Shrink.png"),
            (2,"Assets/Images/Power/Icon/Sprite/TimerUp.png"),
            (3,"Assets/Images/Power/Icon/Sprite/Metal.png"),
            (4,"Assets/Images/Power/Icon/Sprite/Bouncing.png"),
            (5,"Assets/Images/Power/Icon/Sprite/Shield.png"),
            (6,"Assets/Images/Power/Icon/Sprite/Freeze.png")
        });

        assetsManager.AddTextureSet("MalusUI", new List<(int id, string path)>
        {
            (1,"Assets/Images/Power/Icon/Sprite/Stun.png"),
            (2,"Assets/Images/Power/Icon/Sprite/Obstacle.png"),
            (3,"Assets/Images/Power/Icon/Sprite/TimerDown.png"),
            (4,"Assets/Images/Power/Icon/Sprite/Extend.png"),
            (5,"Assets/Images/Power/Icon/Sprite/Bomb.png"),
            (6,"Assets/Images/Power/Icon/Sprite/Border.png")
        });

        assetsManager.AddTextureSet("BonusGrid", new List<(int id, string path)>
        {
            (1,"Assets/Images/Grid/Bonus/Bonus8pxV2.png"),
            (2,"Assets/Images/Grid/Bonus/Bonus16pxV2.png"),
        });

        assetsManager.AddTextureSet("MalusGrid", new List<(int id, string path)>
        {
            (1,"Assets/Images/Grid/Malus/Malus8pxV2.png"),
            (2,"Assets/Images/Grid/Malus/Malus16pxV2.png"),
        });

        assetsManager.AddTextureSet("AppleGrid", new List<(int id, string path)>
        {
            (1,"Assets/Images/Grid/Apple/Apple8px.png"),
            (2,"Assets/Images/Grid/Apple/Apple16px.png"),
            (3, "Assets/Images/Grid/Apple/Apple32px.png")
        });

        assetsManager.AddTextureSet("BombGrid", new List<(int id, string path)>
        {
            (1,"Assets/Images/Grid/Bomb/Bomb8px.png"),
            (2,"Assets/Images/Grid/Bomb/Bomb16px.png"),
            (3, "Assets/Images/Grid/Bomb/Bomb32px.png")
        });

        assetsManager.AddTextureSet("ObstacleGrid", new List<(int id, string path)>
        {
            (1,"Assets/Images/Grid/Obstacle/Obstacle8px.png"),
            (2,"Assets/Images/Grid/Obstacle/Obstacle16px.png"),
        });

        assetsManager.AddTextureSet("BorderGrid", new List<(int id, string path)>
        {
            (1,"Assets/Images/Grid/Border/Border8px.png"),
            (2,"Assets/Images/Grid/Border/Border16px.png"),
        });

        assetsManager.AddTextureSet("FloorGrid", new List<(int id, string path)>
        {
            (1,"Assets/Images/Grid/Floor/FloorSand8px.png"),
            (2,"Assets/Images/Grid/Floor/FloorSand16px.png"),
        });
    }
}