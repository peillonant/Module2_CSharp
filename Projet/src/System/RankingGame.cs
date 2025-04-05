using System.Diagnostics;
using Raylib_cs;

public static class RankingGame
{
    private static Character[]? tabCharacter;
    private static float f_TimerRanking = 2f;
    private static readonly float f_DelayTimerRanking = 1f;

    public static void InitRankingGame(Character[] originListCharacter)
    {
        tabCharacter = new Character[originListCharacter.Length];

        for (int i = 0; i < originListCharacter.Length; i++)
        {
            tabCharacter[i] = originListCharacter[i];
        }

        GameInfo.NbCharacterAliveDecreased += UpdateRanking;
    }

    public static void UpdateRanking()
    {
        f_TimerRanking += Raylib.GetFrameTime();

        if (f_TimerRanking >= f_DelayTimerRanking)
        {
            f_TimerRanking = 0;

            if (tabCharacter != null)
            {
                Array.Sort(tabCharacter, new CompareTimer());

                for (int i = 0; i < tabCharacter.Length; i++)
                {
                    tabCharacter[i].SetRanking(i + 1);
                }
            }
        }
    }
}


public class CompareTimer : IComparer<Character>
{
    public int Compare(Character? charA, Character? charB)
    {
        if (charA != null && charB != null)
        {
            int charATimer = charA.GetTimer().GetTimerLife();
            int charBTimer = charB.GetTimer().GetTimerLife();
            return charBTimer.CompareTo(charATimer);
        }
        else
            return (charA != null) ? -1 : 1;
    }
}