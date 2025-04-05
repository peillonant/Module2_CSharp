// Modify to class static

public static class GameInfo
{
    #region Variable Game
    private static int i_difficultyGame = 1;
    private static float f_SpeedSnake = 0.5f;
    private static int i_cptApple = 0;
    private static int i_NextLevelAppleNeeded = 2;
    private static int i_NbPlayer;
    private static int i_NbCharacterTotal;
    private static int i_NbCharacterAlive;
    #endregion

    #region ConstVariable
    public const int i_nbCol = 20;
    public const int i_nbLin = 20;
    public const int i_SizeCell = 8;
    public static float[] ratioCols_16 = [0.03f, 0.16f, 0.73f, 0.86f];
    public static float[] ratioLins_16 = [0.03f, 0.28f, 0.53f, 0.77f];
    public static float[] ratioCols_8 = [0.10f, 0.79f];
    public static float[] ratioLins_8 = [0.03f, 0.28f, 0.53f, 0.77f];
    public static float[] ratioCols_3 = [0.03f, 0.70f];
    public static float[] ratioLins_3 = [0.25f];
    #endregion

    #region Event
    public static event Action? SpeedIncreased;
    public static event Action? NbCharacterAliveDecreased;
    #endregion

    #region Encapsulation
    public static void SetDifficultyGame(int i_newDiffulty) => i_difficultyGame = i_newDiffulty;
    public static float GetSpeedSnake() => f_SpeedSnake;
    public static int GetDifficultyGame() => i_difficultyGame;
    public static int GetNbCharacterTotal() => i_NbCharacterTotal;
    public static int GetNbCharacterAlive() => i_NbCharacterAlive;
    public static int GetNbPlayer() => i_NbPlayer;
    #endregion

    public static void DecreaseNbCharacterAlive()
    {
        i_NbCharacterAlive--;

        if (i_NbCharacterAlive > 1)
            NbCharacterAliveDecreased?.Invoke();
        else
            GameState.ChangeScene("win");
    }

    public static void IncreaseCptApple()
    {
        i_cptApple++;
        if (i_cptApple == i_NextLevelAppleNeeded)
        {
            f_SpeedSnake *= 0.9f;
            i_NextLevelAppleNeeded *= 2;

            SpeedIncreased?.Invoke();
        }
    }

    public static void SetNbCharacterTotal(int i_newNBPlayer = 1)
    {
        i_NbPlayer = i_newNBPlayer;

        if (i_difficultyGame == 1)
            i_NbCharacterTotal = 17;        // 1 Table
        else if (i_difficultyGame == 2)
            i_NbCharacterTotal = 51;        // 3 Tables
        else if (i_difficultyGame == 3)
            i_NbCharacterTotal = 85;        // 5 tables
        else
            i_NbCharacterTotal = 170;       // 10 Tables

        i_NbCharacterAlive = i_NbCharacterTotal;
    }
}