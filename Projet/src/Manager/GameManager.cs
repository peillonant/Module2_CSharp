
public class GameManager
{
    #region Instance
    private static GameManager? instance;
    public static GameManager Instance
    {
        get
        {
            instance ??= new GameManager();
            return instance;
        }
    }
    #endregion
    
    private CharacterManager? characterManager;
    private NotificationManager? notificationManager;

    public void InitGame()
    {
        Console.WriteLine("Initialization New Game");
        characterManager = new();
        notificationManager = new();
    }

    public void UpdateGameManager()
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        characterManager.UpdateCharacters();
        notificationManager.UpdateNotification();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    public void GameLostTimer()
    {
        Console.WriteLine("Timer is down");
        GameState.Instance.ChangeScene("gameover");
    }

    public void GameLostCollision()
    {
        Console.WriteLine("Lost due to a collision");
        GameState.Instance.ChangeScene("gameover");
    }

    public void DrawGame()
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        characterManager.DrawCharacters();
        notificationManager.DrawNotification();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    public void Close()
    {
        
    }
}