using System.Data.Common;
using System.Diagnostics;
using Raylib_cs;

public static class TableManager
{
    // Called at the initiation and then at the event when a character has lost
    private static Character[]? tabCharacter;
    private static int i_SizeTable = 17;
    private static bool b_StillTableManager = true;         // Boolean to know when we still manage the Table for Power or not
    private static int i_cptCharacterEliminated = 0;

    public static void InitTableManager(Character[] originListCharacter)
    {
        tabCharacter = new Character[originListCharacter.Length];

        for (int i = 0; i < originListCharacter.Length; i++)
        {
            tabCharacter[i] = originListCharacter[i];
        }

        GenerateRoundTable();
        GeneratePositionOnTable();

        // Subscription
        GameInfo.NbCharacterAliveDecreased += CheckTable;
    }

    // Method to change the display of the screen when the game has 9 and 3 Character Alived
    private static void CheckTable()
    {
        if (GameInfo.GetNbCharacterAlive() == 3 || GameInfo.GetNbCharacterAlive() == 9) // need to add the condition when the nbCharacter Alive is egal 5
        {
            i_SizeTable = GameInfo.GetNbCharacterAlive();
            HideAllLoser();
            GenerateRoundTable();
            GeneratePositionOnTable();
        }
        else if (GameInfo.GetNbCharacterAlive() == 17 || GameInfo.GetNbCharacterAlive() == 34)
        {
            HideAllLoser();
            GenerateRoundTable();
            GeneratePositionOnTable();
        }
        else if (GameInfo.GetNbCharacterAlive() > 17 && GameInfo.GetNbCharacterAlive() < 34)
        {
            b_StillTableManager = false;
            HideAllLoser();
            if (TablePlayerNeedToBeFilled())
                FillPlayerTable();
        }
        else if (GameInfo.GetNbCharacterAlive() >= 34)
        {
            i_cptCharacterEliminated++;

            if (i_cptCharacterEliminated >= 3 || CheckTableAliveCharacter())
            {
                HideAllLoser();
                GenerateRoundTable();
                GeneratePositionOnTable();
                if (TablePlayerNeedToBeFilled())
                    FillPlayerTable();

                i_cptCharacterEliminated = 0;
            }
        }
    }

    // Method to remove from the screen all character already eliminated
    private static void HideAllLoser()
    {
        if (tabCharacter == null)
            return;

        for (int i = 0; i < tabCharacter.Length; i++)
        {
            if (!tabCharacter[i].IsAlive() && (tabCharacter[i].IsDisplayed() || tabCharacter[i].CanBeDisplay()))
            {
                tabCharacter[i].SetIsDisplayed(false);
                tabCharacter[i].SetCanBeDisplay(false);
                tabCharacter[i].SetTable(-1);
            }
        }
    }

    // Method call by the update to generate the table for all character still alived
    private static void GenerateRoundTable()
    {
        if (tabCharacter == null)
            return;

        int i_NbTable = (int) Math.Ceiling((float) GameInfo.GetNbCharacterAlive() / i_SizeTable);       // Compute the number of table needed at the beginning of the game
        int[] tabTable = new int[i_NbTable];                                                           // Allow us to follow how many character are on each table

        for (int i = 0; i < tabCharacter.Length; i++)
        {
            if (!tabCharacter[i].IsAlive())
                continue;

            bool b_TableFilled;
            int indexTable;

            do
            {
                indexTable = Raylib.GetRandomValue(0, i_NbTable - 1);
                b_TableFilled = tabTable[indexTable] < i_SizeTable;
            } while (!b_TableFilled);

            tabTable[indexTable]++;
            tabCharacter[i].SetTable(indexTable);
        }
    }

    // Method call by the update to generate the table for all character still alived around the Player. We will always fill the player table
    private static void FillPlayerTable()
    {
        if (tabCharacter == null)
            return;

        int i_indexPlayerTable = RetrieveTableCharacter();

        for (int i = 0; i < tabCharacter.Length; i++)
        {
            if (tabCharacter[i].IsAlive() && tabCharacter[i].GetTable() != i_indexPlayerTable)
            {
                tabCharacter[i].SetTable(i_indexPlayerTable);
                tabCharacter[i].SetIsDisplayed(true);
                
                for (int j = 0; j < i_SizeTable; j++)
                {
                    if (IsPositionTableAvailable(tabCharacter.Length, i_indexPlayerTable, j))
                    {
                        tabCharacter[i].SetPositionOnTable(j);
                        return;
                    }    
                }
            }
        }
    }

    // Method call by the update to check if the last character eliminated was on the player table
    private static bool TablePlayerNeedToBeFilled()
    {
        if (tabCharacter == null)
            return false;

        int i_cptCharacterOnTable = 0;
        int i_indexPlayerTable = RetrieveTableCharacter();

        for (int i = 0; i < tabCharacter.Length; i++)
        {
            if (tabCharacter[i].IsAlive() && tabCharacter[i].GetTable() == i_indexPlayerTable)
            {
                i_cptCharacterOnTable++;
            }
        }

        return i_cptCharacterOnTable != i_SizeTable;
    }
    // Method to Generate for each Character a position on the table assigned
    private static void GeneratePositionOnTable()
    {
        if (tabCharacter == null)
            return;

        int i_IndexTablePlayer = RetrieveTableCharacter();
        
        for (int i = 0; i < tabCharacter.Length; i++)
        {
            if (tabCharacter[i].IsPlayer())
                tabCharacter[i].SetPositionOnTable(i_SizeTable - 1);                                // Minus 1 because the size of the table is 17 and the first position is set a 0
            else
            {
                if (tabCharacter[i].IsAlive())
                {
                    if (tabCharacter[i].GetTable() == i_IndexTablePlayer)
                    {
                        tabCharacter[i].SetIsDisplayed(true);

                        int indexPositionRandom = 0;
                        bool b_PositionValid = false;

                        while (!b_PositionValid)
                        {
                            indexPositionRandom = Raylib.GetRandomValue(0, i_SizeTable - 2);        // Minus 2 because the last position is reserved to the Player
                            b_PositionValid = IsPositionTableAvailable(i, i_IndexTablePlayer, indexPositionRandom);
                        }

                        tabCharacter[i].SetPositionOnTable(indexPositionRandom);
                    }
                    else
                        tabCharacter[i].SetIsDisplayed(false);
                }
            }
        }
    }

    // Method to check if the Position on the table is already assign to a character already on position
    private static bool IsPositionTableAvailable(int i_IndexMax, int i_IndexTable, int i_IndexPosition)
    {
        if (tabCharacter == null)
            return false;

        for (int i = 0; i < i_IndexMax; i++)
        {
            if (tabCharacter[i].IsAlive() && tabCharacter[i].GetTable() == i_IndexTable && tabCharacter[i].GetPositionOnTable() == i_IndexPosition)
                return false;
        }

        return true;
    }

    // Method to retrieve the index of the table for the character
    private static int RetrieveTableCharacter(Character? character = null)
    {
        if (tabCharacter != null)
        {
            for (int i = 0; i < tabCharacter.Length; i++)
            {
                if (character == null && tabCharacter[i].IsPlayer())
                        return tabCharacter[i].GetTable();

                if (tabCharacter[i] == character)
                    return tabCharacter[i].GetTable();
            }
            return -1;
        }
        return -1;
    }

    // Method to check if a table has more than 3 character not alive on it. If yes return true
    private static bool CheckTableAliveCharacter()
    {
        if (tabCharacter == null)
            return false;
        
        int i_NbTable = (int) Math.Ceiling((float) GameInfo.GetNbCharacterAlive() / i_SizeTable);       // Compute the number of table needed at the beginning of the game
        int[] tabTableNbNotAlive = new int[i_NbTable];                                                            // Allow us to follow how many character are on each table

        for (int i = 0; i < tabCharacter.Length; i++)
        {
            if (!tabCharacter[i].IsAlive() && tabCharacter[i].GetTable() >= 0 && tabCharacter[i].GetTable() < i_NbTable)
            {
                tabTableNbNotAlive[tabCharacter[i].GetTable()]++;

                if (tabTableNbNotAlive[tabCharacter[i].GetTable()] >= 3)
                    return true; 
            }
        }
        
        return false;
    }

    // Method called by Power System to retrieve the list of Character that can be target by a Power
    public static List<Character> RetrieveListCharacter(Character character)
    {
        if (tabCharacter == null)
            return [];   

        List<Character> listCharacterTarget = [];
        
        if (b_StillTableManager)
        {
            int i_IndexTable = character.GetTable();

            for (int i = 0; i < tabCharacter.Length; i++)
            {
                if (tabCharacter[i].GetTable() == i_IndexTable)
                {
                    if (tabCharacter[i] == character)
                        continue;
                    
                    if (tabCharacter[i].IsAlive())
                        listCharacterTarget.Add(tabCharacter[i]);
                }
            }
        }
        else
        {
            for (int i = 0; i < tabCharacter.Length; i++)
            {
                if (tabCharacter[i] == character)
                    continue;
                
                if (tabCharacter[i].IsAlive())
                    listCharacterTarget.Add(tabCharacter[i]);
            }
        }

        return listCharacterTarget;
    }

}