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
        CreateCharacters();
#pragma warning disable CS8604 // Possible null reference argument.
        rankingGame = new RankingGame(tabCharacter);
        tableManager = new TableManager(tabCharacter);
#pragma warning restore CS8604 // Possible null reference argument.
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
        for (int i = 0; i < tabCharacter.Length; i++)
        {
            tabCharacter[i].UpdatePlayer();
        }

        rankingGame.UpdateRanking();

        // if (tabCharacter[0].GetTimer().GetTimerLife() < 59)
        //     Debug.WriteLine("Plop");
    }

    public void DrawCharacters()
    {
        for (int i = 0; i < tabCharacter.Length; i++)
        {
            tabCharacter[i].DrawBoard();
        }
    }
}