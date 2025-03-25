public class RankingGame
{
    private List<Character> listCharacter = [];

    public RankingGame(List<Character> originListCharacter)
    {   
        foreach (Character character in originListCharacter)
        {
            listCharacter.Add(character);
        }
    }

    public void UpdateRanking()
    {
        listCharacter.Sort(0, GameInfo.Instance.GetNbPlayerAlive()-1, new CompareTimer());

        for (int i = 0; i < listCharacter.Count; i++)
        {
            listCharacter[i].SetRanking(i + 1);
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