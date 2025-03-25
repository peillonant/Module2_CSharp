using System.Diagnostics;
using System.Runtime.CompilerServices;
using Raylib_cs;

public class CharacterManager
{
    private List<Character> listCharacter;      // VOIR POUR LE PASSER EN TABLEAU
    private RankingGame rankingGame;
    
    public CharacterManager()
    {
        listCharacter = [];
        CreateCharacters(1);
        rankingGame = new RankingGame(listCharacter);
    }

    private void CreateCharacters(int i_NbPlayer)
    {
        for (int i = 0; i < i_NbPlayer; i++)
        {
            listCharacter.Add(new Player());
        }

        int i_NbEnemies = -i_NbPlayer;

        if (GameInfo.Instance.GetDifficultyGame() == 1)
            i_NbEnemies += 9;
        else if (GameInfo.Instance.GetDifficultyGame() == 2)
            i_NbEnemies += 25;
        else if (GameInfo.Instance.GetDifficultyGame() == 3)
            i_NbEnemies += 51;
        else
            i_NbEnemies += 99;

        GameInfo.Instance.SetNbPlayerAlive(i_NbEnemies + i_NbPlayer);
        GameInfo.Instance.SetNbPlayerTotal(i_NbEnemies + i_NbPlayer);

        for (int i = 0; i < i_NbEnemies; i++)
        {
            Enemy localEnemy;
            localEnemy = (i < 8) ? new(i) : new(-1);        // ATTENTION MAGIC NUMBER !!!!!
            listCharacter.Add(localEnemy);
        }
    }

    public void UpdateCharacters()
    {
        for (int i = 0; i < listCharacter.Count; i++)
        {
            listCharacter[i].UpdatePlayer();

            if (listCharacter[i].GetTimer().GetTimerLife() < 19)
                Debug.WriteLine("Plop");
        }
        
        rankingGame.UpdateRanking();
    }


    public void DrawCharacters()
    {
        for (int i = 0; i < listCharacter.Count; i++)
        {
            listCharacter[i].DrawPlayerInfo();
            listCharacter[i].DrawBoard();
        }
    }    
}