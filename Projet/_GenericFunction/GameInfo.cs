public class GameInfo
{
    #region Instanciation
    private static GameInfo? instance;
    public static GameInfo Instance
    {
        get
        {
            instance ??= new GameInfo();
            return instance;
        }
    }
    #endregion

    #region Variable
    private int i_difficultyGame = 1;
    private float f_SpeedSnake = 0.5f;
    private int i_cptApple = 0;
    private int i_NextLevelAppleNeeded = 2;
    private int i_NbPlayerTotal;
    private int i_NbPlayerAlive;
    private int i_nbCol = 20;
    private int i_nbLin = 20;
    #endregion

    #region Encapsulation
    public void SetDifficultyGame (int i_newDiffulty) => i_difficultyGame = i_newDiffulty;
    public void SetNbPlayerTotal (int i_newNbPlayerTotal) => i_NbPlayerTotal = i_newNbPlayerTotal;
    public void SetNbPlayerAlive (int i_newNbPlayerAlive) => i_NbPlayerAlive = i_newNbPlayerAlive;
    public void IncreaseCptApple () => i_cptApple++;
    public float GetSpeedSnake() => f_SpeedSnake;
    public int GetDifficultyGame () => i_difficultyGame;
    public int GetNbPlayerTotal() => i_NbPlayerTotal;
    public int GetNbPlayerAlive() => i_NbPlayerAlive;
    public int GetNbCol() => i_nbCol;
    public int GetNbLin() => i_nbLin;
    #endregion

    public void IncreaseSpeed()
    {
        if (i_cptApple == i_NextLevelAppleNeeded)
        {
            Console.WriteLine("Increase Speed");
            f_SpeedSnake *= 0.9f;
            i_NextLevelAppleNeeded *= 2;
        }
    }
}