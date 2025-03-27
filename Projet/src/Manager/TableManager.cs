using System.Diagnostics;

public class TableManager
{
    // Called at the initiation and then at the event when a character has lost
    
    private Character[] tabCharacter;

    public TableManager(Character[] originListCharacter)
    {
        tabCharacter = new Character[originListCharacter.Length];

        for (int i = 0; i < originListCharacter.Length; i ++ )
        {
            tabCharacter[i] = originListCharacter[i];
        }

        // Subscription
        GameInfo.Instance.NbPlayerAliveDecreased += CheckTable;
    }

    private void CheckTable()
    {
        if (GameInfo.Instance.GetNbPlayerAlive() == 3)
        {
            HideAllLoser();
            ChangePositionPlayerStillAlive();

            Debug.WriteLine("plop");
        }
        else if (GameInfo.Instance.GetNbPlayerAlive() == 9)
        {
            //HideAllLoser();
        }
        else
        {

        }
    }

    private void HideAllLoser()
    {
        for (int i = 0; i < tabCharacter.Length; i++)
        {
            if (!tabCharacter[i].IsAlive())
            {
                tabCharacter[i].SetTable(-1);
            }
        }
    }

    private void ChangePositionPlayerStillAlive()
    {
        for (int i = 0; i < tabCharacter.Length; i++)
        {
            if (tabCharacter[i].IsAlive())
            {
                tabCharacter[i].SetPositionOnTable(tabCharacter[i].GetRanking());
            }
        }
    }


}