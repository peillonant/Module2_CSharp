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
        if (tabCharacter == null)
            return; 
        
        for (int i = 0; i < tabCharacter.Length; i++)
        {
            tabCharacter[i].UpdatePlayer();
        }

        RankingGame.UpdateRanking();
    }

    public void DrawCharacters()
    {   
        if (tabCharacter == null)
            return; 

        for (int i = 0; i < tabCharacter.Length; i++)
        {
            if (tabCharacter[i].IsDisplayed())
                UI_Board.DrawBoard(tabCharacter[i]);
        }
    }
}