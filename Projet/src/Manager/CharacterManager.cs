public class CharacterManager
{
    private Character[]? tabCharacter;

    public CharacterManager()
    {
        CreateCharacters();
        if (tabCharacter != null)
        {
            RankingGame.InitRankingGame(tabCharacter);
            TableManager.InitTableManager(tabCharacter);
        }
    }

    private void CreateCharacters()
    {
        int i_NbCharact = GameInfo.GetNbCharacterTotal();
        int i_NbPlayer = GameInfo.GetNbPlayer();

        tabCharacter = new Character[i_NbCharact];

        for (int i = 0; i < i_NbCharact; i++)
        {
            tabCharacter[i] = (i < i_NbPlayer) ? new Player() : new Enemy();
        }
    }

    public void UpdateCharacters()
    {
        for (int i = 0; i < tabCharacter?.Length; i++)
        {
            tabCharacter[i].UpdatePlayer();
        }

        RankingGame.UpdateRanking();
    }

    public void DrawCharacters()
    {
        for (int i = 0; i < tabCharacter?.Length; i++)
        {
            if (tabCharacter[i].IsDisplayed())
                UIManager.DrawBoard(tabCharacter[i]);
        }
    }

    public void ResetCharacter()
    {
        if (tabCharacter != null)
            Array.Clear(tabCharacter);
    }
}