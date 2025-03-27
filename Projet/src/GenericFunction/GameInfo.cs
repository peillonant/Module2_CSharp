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

    #region Variable Game
    private int i_difficultyGame = 1;
    private float f_SpeedSnake = 0.5f;
    private int i_cptApple = 0;
    private int i_NextLevelAppleNeeded = 2;
    private int i_NbPlayerTotal;
    private int i_NbPlayerAlive;
    #endregion

    #region ConstVariable
    public const int i_nbCol = 20;
    public const int i_nbLin = 20;
    public const int i_SizeCell = 8;
    public readonly float[] ratioCols_16 = [ 0.03f, 0.16f, 0.73f, 0.86f ];
    public readonly float[] ratioLins_16 = [ 0.03f, 0.28f, 0.53f, 0.77f ];
    public readonly float[] ratioCols_8 = [ 0.01f, 0.16f, 0.70f, 0.85f ];
    public readonly float[] ratioLins_8 = [ 0.03f, 0.25f, 0.50f, 0.72f ];
    public readonly float[] ratioCols_3 = [ 0.03f, 0.70f ];
    public readonly float[] ratioLins_3 = [ 0.25f ];
    #endregion

    #region Event
    public event Action? SpeedIncreased;
    public event Action? NbPlayerAliveDecreased;
    #endregion

    #region Encapsulation
    public void SetDifficultyGame (int i_newDiffulty) => i_difficultyGame = i_newDiffulty;
    public void SetNbPlayerTotal (int i_newNbPlayerTotal) => i_NbPlayerTotal = i_newNbPlayerTotal;
    public void SetNbPlayerAlive (int i_newNbPlayerAlive) => i_NbPlayerAlive = i_newNbPlayerAlive;
    public float GetSpeedSnake() => f_SpeedSnake;
    public int GetDifficultyGame () => i_difficultyGame;
    public int GetNbPlayerTotal() => i_NbPlayerTotal;
    public int GetNbPlayerAlive() => i_NbPlayerAlive;
    #endregion

    public void DecreaseNbPlayerAlive()
    {
        i_NbPlayerAlive--;
        NbPlayerAliveDecreased?.Invoke();
    }

    public void IncreaseCptApple ()
    {
        i_cptApple++;
        if (i_cptApple == i_NextLevelAppleNeeded)
        {
            f_SpeedSnake *= 0.9f;
            i_NextLevelAppleNeeded *= 2;

            SpeedIncreased?.Invoke();
        }
    } 
}