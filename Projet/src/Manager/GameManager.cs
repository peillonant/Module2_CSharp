
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
    }

    public static void ResetGame()
    {
        Console.WriteLine("Reset the Game, to be relaunch");
        GameInfo.ResetGameInfo();
        characterManager?.ResetCharacter();
        NotificationManager.UnsubscriptionEvent();
        RankingGame.ResetRanking();
    }

    public static void UpdateGameManager()
    {
        characterManager?.UpdateCharacters();
        NotificationManager.UpdateNotification();
    }

    public static void GameLostTimer()
    {
        Services.Get<IScenesManager>().Show<SceneGameOver>();
        // Called the method to reinit everything
    }

    public static void GameLostCollision()
    {
        Services.Get<IScenesManager>().Show<SceneGameOver>();
        // Called the method to reinit everything
    }

    public static void DrawGame()
    {
        characterManager?.DrawCharacters();
        NotificationManager.DrawNotification();
    }
}