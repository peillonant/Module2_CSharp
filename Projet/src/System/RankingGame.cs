using System.Diagnostics;

public class RankingGame
{
    private Character[] tabCharacter;

    public RankingGame(Character[] originListCharacter)
    {   
        tabCharacter = new Character[originListCharacter.Length];

        for (int i = 0; i < originListCharacter.Length; i ++ )
        {
            tabCharacter[i] = originListCharacter[i];
        }
    }

    public void UpdateRanking()
    {
        Array.Sort(tabCharacter, new CompareTimer());

        //listCharacter.Sort(0, GameInfo.Instance.GetNbPlayerTotal(), new CompareTimer());

        for (int i = 0; i < tabCharacter.Length; i++)
        {
            tabCharacter[i].SetRanking(i + 1);
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