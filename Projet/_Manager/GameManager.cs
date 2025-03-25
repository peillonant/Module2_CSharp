
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Raylib_cs;

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
    
    private CharacterManager characterManager;

    public void InitGame()
    {
        Console.WriteLine("Initialization New Game");
        characterManager = new CharacterManager();
    }

    public void UpdateGameManager()
    {
        characterManager.UpdateCharacters();
        GameInfo.Instance.IncreaseSpeed();
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
        characterManager.DrawCharacters();
    }

    public void Close()
    {
        
    }
}