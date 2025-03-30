
// Passer en classe static

public static class GameManager
{
    private static CharacterManager? characterManager;
    private static NotificationManager? notificationManager;

    public static void InitGame()
    {
        Console.WriteLine("Initialization New Game");
        GameInfo.SetNbCharacterTotal();
        characterManager = new();
        notificationManager = new();
    }

    public static void UpdateGameManager()
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        characterManager.UpdateCharacters();
        notificationManager.UpdateNotification();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    public static void GameLostTimer()
    {
        Console.WriteLine("Timer is down");
        GameState.Instance.ChangeScene("gameover");
    }

    public static void GameLostCollision()
    {
        Console.WriteLine("Lost due to a collision");
        GameState.Instance.ChangeScene("gameover");
    }

    public static void DrawGame()
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        characterManager.DrawCharacters();
        notificationManager.DrawNotification();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
}