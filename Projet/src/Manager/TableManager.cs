using System.Diagnostics;
using Raylib_cs;

public class TableManager
{
    // Called at the initiation and then at the event when a character has lost
    private readonly Character[] tabCharacter;
    private int i_SizeTable = 16;

    public TableManager(Character[] originListCharacter)
    {
        tabCharacter = new Character[originListCharacter.Length];

        for (int i = 0; i < originListCharacter.Length; i++)
        {
            tabCharacter[i] = originListCharacter[i];
        }

        GenerateFirstRoundTable();
        GeneratePositionOnTable();

        // Subscription
        GameInfo.NbCharacterAliveDecreased += CheckTable;
    }

    private void CheckTable()
    {
        if (GameInfo.GetNbCharacterAlive() == 3 || GameInfo.GetNbCharacterAlive() == 9)
        {
            i_SizeTable = GameInfo.GetNbCharacterAlive() - 1;
            HideAllLoser();
            GeneratePositionOnTable();
        }
    }

    private void HideAllLoser()
    {
        for (int i = 0; i < tabCharacter.Length; i++)
        {
            if (!tabCharacter[i].IsAlive())
            {
                tabCharacter[i].SetIsDisplayed(false);
                tabCharacter[i].SetCanBeDisplay(false);
            }
        }
    }

    private void GenerateFirstRoundTable()
    {
        int i_NbTable = GameInfo.GetNbCharacterTotal() / 17;
        int[] tabTable = new int[i_NbTable];

        for (int i = 0; i < GameInfo.GetNbCharacterTotal(); i++)
        {
            bool b_TableFilled;
            int indexTable;
            do
            {
                indexTable = Raylib.GetRandomValue(0, i_NbTable - 1);
                b_TableFilled = tabTable[indexTable] < 16;

            } while (!b_TableFilled);

            tabTable[indexTable]++;
            tabCharacter[i].SetTable(indexTable);
        }
    }

    private void GeneratePositionOnTable()
    {
        int i_IndexTablePlayer = RetrieveTablePlayer();

        for (int i = 0; i < tabCharacter.Length; i++)
        {
            if (tabCharacter[i].IsPlayer())
                tabCharacter[i].SetPositionOnTable(i_SizeTable);
            else
            {
                if (tabCharacter[i].IsAlive())
                {
                    if (tabCharacter[i].GetTable() == i_IndexTablePlayer)
                    {
                        tabCharacter[i].SetIsDisplayed(true);

                        int indexPositionRandom;
                        bool b_PositionValid = false;

                        while (!b_PositionValid)
                        {
                            indexPositionRandom = Raylib.GetRandomValue(0, i_SizeTable - 1);
                            if (CheckPositionTable(i, i_IndexTablePlayer, indexPositionRandom))
                            {
                                tabCharacter[i].SetPositionOnTable(indexPositionRandom);
                                b_PositionValid = true;
                            }
                        }
                    }
                    else
                        tabCharacter[i].SetIsDisplayed(false);
                }
            }
        }
    }

    private bool CheckPositionTable(int i_IndexMax, int i_IndexTable, int i_IndexPosition)
    {
        for (int i = 0; i < i_IndexMax; i++)
        {
            if (tabCharacter[i].IsAlive() && tabCharacter[i].GetTable() == i_IndexTable && tabCharacter[i].GetPositionOnTable() == i_IndexPosition)
                return false;
        }
        return true;
    }

    private int RetrieveTablePlayer()
    {
        for (int i = 0; i < tabCharacter.Length; i++)
        {
            if (tabCharacter[i].IsPlayer())
                return tabCharacter[i].GetTable();
        }
        return -1;
    }

}