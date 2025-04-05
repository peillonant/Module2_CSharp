
// Passer en classe static

public static class GameManager
{
    private static CharacterManager? characterManager;

    public static void InitGame()
    {
        Console.WriteLine("Initialization New Game");
        GameInfo.SetNbCharacterTotal();
        characterManager = new();
        NotificationManager.SubscriptionEvent();
        UI_Board_Sprite.InitUI_Board_Sprite();
        UI_Notification_Color.InitUI_Notification_Color();
    }

    public static void UpdateGameManager()
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        characterManager.UpdateCharacters();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        NotificationManager.UpdateNotification();
    }

    public static void GameLostTimer()
    {
        Console.WriteLine("Timer is down");
        GameState.ChangeScene("gameover");
    }

    public static void GameLostCollision()
    {
        Console.WriteLine("Lost due to a collision");
        GameState.ChangeScene("gameover");
    }

    public static void DrawGame()
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        characterManager.DrawCharacters();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        NotificationManager.DrawNotification();
        UI_Board.DrawAppleEaten();
    }
}