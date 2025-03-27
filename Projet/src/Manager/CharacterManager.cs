using System.Diagnostics;
using System.Runtime.CompilerServices;
using Raylib_cs;

public class CharacterManager
{
    private Character[] tabCharacter;
    private RankingGame rankingGame;
    private TableManager tableManager;

    
    public CharacterManager()
    {
        CreateCharacters(1);
        #pragma warning disable CS8604 // Possible null reference argument.
        rankingGame = new RankingGame(tabCharacter);
        tableManager = new TableManager(tabCharacter);
        #pragma warning restore CS8604 // Possible null reference argument.
    }

    private void CreateCharacters(int i_NbPlayer)
    {
        int i_NbCharact;

        if (GameInfo.Instance.GetDifficultyGame() == 1)
            i_NbCharact = 9;
        else if (GameInfo.Instance.GetDifficultyGame() == 2)
            i_NbCharact = 25;
        else if (GameInfo.Instance.GetDifficultyGame() == 3)
            i_NbCharact = 51;
        else
            i_NbCharact = 99;

        tabCharacter = new Character[i_NbCharact];

        for (int i = 0; i < i_NbCharact; i++)
        {
            if (i < i_NbPlayer)
               tabCharacter[i] = new Player();
            else
            {
                Enemy localEnemy;
                localEnemy = (i - i_NbPlayer < 9) ? new(i - i_NbPlayer) : new(-1);        // ATTENTION MAGIC NUMBER !!!!!
                tabCharacter[i] = localEnemy;
            }
        }        

        GameInfo.Instance.SetNbPlayerAlive(i_NbCharact);
        GameInfo.Instance.SetNbPlayerTotal(i_NbCharact);
    }

    public void UpdateCharacters()
    {
        for (int i = 0; i < tabCharacter.Length; i++)
        {
            tabCharacter[i].UpdatePlayer();
        }
        
        rankingGame.UpdateRanking();

        if (tabCharacter[0].GetTimer().GetTimerLife() < 59)
            Debug.WriteLine("Plop");
    }

    public void DrawCharacters()
    {
        for (int i = 0; i < tabCharacter.Length; i++)
        {
            tabCharacter[i].DrawBoard();
        }
    }    
}