using System.Data.Common;
using System.Diagnostics;
using Raylib_cs;

public static class TableManager
{
    // Called at the initiation and then at the event when a character has lost
    private static Character[]? tabCharacter;
    private static int i_SizeTable;
    private static bool b_StillTableManager;         // Boolean to know when we still manage the Table for Power or not
    private static int i_cptCharacterEliminated = 0;

    // First initialization of all table (linked to the number of Character in play)
    public static void InitTableManager(Character[] originListCharacter)
    {
        i_SizeTable = 17;
        b_StillTableManager = true;

        tabCharacter = new Character[originListCharacter.Length];

        for (int i = 0; i < originListCharacter.Length; i++)
            tabCharacter[i] = originListCharacter[i];


        GenerateRoundTable(false);
        GeneratePositionOnTable();

        // Subscription
        GameInfo.NbCharacterAliveDecreased += CheckTable;
    }

    // Method to change the display of the screen when the game has 9 and 3 Character Alived
    private static void CheckTable()
    {
        if (GameInfo.GetNbCharacterAlive() == 3 || GameInfo.GetNbCharacterAlive() == 5 || GameInfo.GetNbCharacterAlive() == 9)
        {
            i_SizeTable = GameInfo.GetNbCharacterAlive();
            HideAllLoser();
            GenerateRoundTable(false);
            GeneratePositionOnTable();
        }
        // else if (GameInfo.GetNbCharacterAlive() > 9 && GameInfo.GetNbCharacterAlive() < 17)
        // {
        //     i_SizeTable = 9;
        //     HideAllLoser();
        //     GenerateRoundTable();
        //     GeneratePositionOnTable();
        // }
        else if (GameInfo.GetNbCharacterAlive() == 17 || GameInfo.GetNbCharacterAlive() == 34)
        {
            HideAllLoser();
            GenerateRoundTable(false);
            GeneratePositionOnTable();
        }
        else if (GameInfo.GetNbCharacterAlive() > 17 && GameInfo.GetNbCharacterAlive() < 34)
        {
            b_StillTableManager = false;
            if (TablePlayerNeedToBeFilled(2))
            {
                HideAllLoser();
                FillPlayerTable();
            }
        }
        else if (GameInfo.GetNbCharacterAlive() >= 34)
        {
            i_cptCharacterEliminated++;
            
            // We made the Generation of The table and Position for all Character when the Player table has 2 eliminated
            if (TablePlayerNeedToBeFilled(2))
            {
                HideAllLoser();
                GenerateRoundTable(false);
                GeneratePositionOnTable();

                i_cptCharacterEliminated = 0;
            }
            // We made the Generation of the Table and Positon for all Character except the one that are on the Player table
            else if (i_cptCharacterEliminated >= 4 || CheckTableAliveCharacter())
            {
                GenerateRoundTable(true);
                i_cptCharacterEliminated = 0;
            }
        }
    }

    // Method to remove from the screen all character already eliminated
    private static void HideAllLoser()
    {
        for (int i = 0; i < tabCharacter?.Length; i++)
        {
            if (!tabCharacter[i].IsAlive() && tabCharacter[i].IsDisplayed())
            {
                tabCharacter[i].SetIsDisplayed(false);
                tabCharacter[i].SetTable(-1);
            }
        }
    }

    // Method call by the update to generate the table for all character still alived around the Player. We will always fill the player table
    private static void FillPlayerTable()
    {
        int i_indexPlayerTable = RetrieveTableCharacter();

        for (int i = 0; i < tabCharacter?.Length; i++)
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
    private static bool TablePlayerNeedToBeFilled(int i_NbBeforeChanged)
    {
        int i_cptCharacterNotAliveOnTable = 0;
        int i_indexPlayerTable = RetrieveTableCharacter();

        for (int i = 0; i < tabCharacter?.Length; i++)
        {
            if (!tabCharacter[i].IsAlive() && tabCharacter[i].GetTable() == i_indexPlayerTable)
                i_cptCharacterNotAliveOnTable++;
        }

        return i_cptCharacterNotAliveOnTable >= i_NbBeforeChanged;
    }

    // Method call by the update to generate the table for all character still alived
    // Parameter: true => We have to generate table for all character except the one on the table of the player
    // Parameter: false => We have to generete a new Table for all character
    private static void GenerateRoundTable(bool b_PlayerTableHasToNotBeUpdate)
    {
        int indexPlayerTable = RetrieveTableCharacter();
        int tmpIndexPlayerTable = -1;

        int i_NbTable = (int) Math.Ceiling((float) GameInfo.GetNbCharacterAlive() / i_SizeTable);       // Compute the number of table needed
        int[] tabTable = new int[i_NbTable];                                                            // Allow us to follow how many character are on each table

        bool b_TableIsNotFilled;
        int indexTable = 0;
        int cpt;
        
        // If we have to not modify the PlayerTable then we check if the table of the Player is above the nb of table available
        if (b_PlayerTableHasToNotBeUpdate)
            tmpIndexPlayerTable = (indexPlayerTable >= i_NbTable) ? 0 : indexPlayerTable;

        // Now we check for all Character, to set the table
        for (int i = 0; i < tabCharacter?.Length; i++)
        {
            cpt = 0;

            // The character is not alive, then we move on
            if (!tabCharacter[i].IsAlive())
                continue;

            // The character is on the same table of the Player and we do not want to update it
            if (b_PlayerTableHasToNotBeUpdate && tabCharacter[i].GetTable() == indexPlayerTable)
            {
                tabTable[tmpIndexPlayerTable]++;
                tabCharacter[i].SetTable(tmpIndexPlayerTable);
            }
            else
            {
                b_TableIsNotFilled = false;

                while (!b_TableIsNotFilled)
                {
                    indexTable = Raylib.GetRandomValue(0, i_NbTable - 1);

                    // If the character has the same position of the table and it was not ther before then we relaunch an iteration of the loop
                    if (indexTable == tmpIndexPlayerTable && b_PlayerTableHasToNotBeUpdate)
                    {
                        b_TableIsNotFilled = false;
                        continue;
                    }

                    b_TableIsNotFilled = tabTable[indexTable] < i_SizeTable;

                    cpt++;

                    if (cpt > 10)
                    {
                        indexTable = ForceIndexTable(tabTable);
                        b_TableIsNotFilled = true;
                    }
                }

                tabTable[indexTable]++;
                tabCharacter[i].SetTable(indexTable);
            }
        }
    }

    // Method to Generate for each Character a position on the table assigned
    private static void GeneratePositionOnTable()
    {
        int i_IndexTablePlayer = RetrieveTableCharacter();
        int indexPositionRandom = 0;
        bool b_PositionValid;

        for (int i = 0; i < tabCharacter?.Length; i++)
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

                        b_PositionValid = false;

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
            return true;

        for (int i = 0; i < i_IndexMax; i++)
        {
            if (tabCharacter[i].IsAlive() && tabCharacter[i].GetTable() == i_IndexTable && tabCharacter[i].GetPositionOnTable() == i_IndexPosition)
                return false;
        }

        return true;
    }

    private static int ForceIndexTable(int[] tabTable)
    {
        for(int i = 0; i < tabTable.Length; i++)
        {
            if (tabTable[i] < i_SizeTable)
                return i;
        }
        return -1;
    }

    // Method to retrieve the index of the table for the character
    private static int RetrieveTableCharacter(Character? character = null)
    {
        for (int i = 0; i < tabCharacter?.Length; i++)
        {
            if (character == null && tabCharacter[i].IsPlayer())
                    return tabCharacter[i].GetTable();

            if (tabCharacter[i] == character)
                return tabCharacter[i].GetTable();
        }
        return -1;
    }

    // Method to check if a table has more than 3 character not alive on it. If yes return true
    private static bool CheckTableAliveCharacter()
    {       
        int i_NbTable = (int) Math.Ceiling((float) GameInfo.GetNbCharacterAlive() / i_SizeTable);       // Compute the number of table needed
        int[] tabTableNbNotAlive = new int[i_NbTable];                                                  // Allow us to follow how many character are on each table

        for (int i = 0; i < tabCharacter?.Length; i++)
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
        List<Character> listCharacterTarget = [];
        
        if (b_StillTableManager)
        {
            int i_IndexTable = character.GetTable();

            for (int i = 0; i < tabCharacter?.Length; i++)
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
            for (int i = 0; i < tabCharacter?.Length; i++)
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