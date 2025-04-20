using System.ComponentModel.Design;
using System.Diagnostics;
using Raylib_cs;

public class PowerSystem
{
    public readonly Character characterOrigin;
    private List<Cell> cellAvailables = [];
    private Cell? cellBonus;
    private Cell? cellMalus;

    private readonly Power?[] bonusAvailable = new Power[(int)TypeBonus.Count - 1];
    private readonly Power?[] malusAvailable = new Power[(int)TypeMalus.Count - 1];
    private int[] bonusSequence = new int[(int)TypeBonus.Count - 1];
    private int[] malusSequence = new int[(int)TypeMalus.Count - 1];
    private int i_IndexBonusSequence = 0;
    private int i_IndexMalusSequence = 0;

    private Power? bonusDisplayed;
    private Power? malusDisplayed;

    public Power? GetBonusDisplayed() => bonusDisplayed;
    public Power? GetMalusDisplayed() => malusDisplayed;


    private float f_Timer = 0;
    private float f_DelaySpawn = 2f;
    private readonly float f_DelayReroll = 10f;
    private readonly float f_DelayPowerPicked = 15f;
    private bool b_PowerHasBeenPicked = true;

    public PowerSystem(Character characterOrigin)
    {
        this.characterOrigin = characterOrigin;

        InitPowerAvailable();
    }

    // Method to initialize the Array Power Available and trigger the first round for selected power
    private void InitPowerAvailable()
    {
        // Bonus Manager
        bonusAvailable[0] = new Shrink(this);
        bonusAvailable[1] = new UpTimer(this);
        bonusAvailable[2] = new Metal(this);
        bonusAvailable[3] = new Bouncing(this);
        bonusAvailable[4] = new Shield(this);
        bonusAvailable[5] = new Freeze(this);

        InitBonusSequence();
        GenerateRandomSequence(TypePower.Bonus);

        // Malus Manager
        malusAvailable[0] = new Stun(this);
        malusAvailable[1] = new Obstacle(this);
        malusAvailable[2] = new DownTimer(this);
        malusAvailable[3] = new Extend(this);
        malusAvailable[4] = new Bomb(this);
        malusAvailable[5] = new Border(this);

        InitMalusSequence();
        GenerateRandomSequence(TypePower.Malus);
    }

    // Method to initialize the Array that contain the Sequence of Power selection
    private void InitBonusSequence()
    {
        i_IndexBonusSequence = 0;

        for (int i = 0; i < bonusSequence.Length; i++)
            bonusSequence[i] = -1;
    }
    private void InitMalusSequence()
    {
        i_IndexMalusSequence = 0;

        for (int i = 0; i < malusSequence.Length; i++)
            malusSequence[i] = -1;
    }

    // Method to Generate the RandomSequence for the Power
    private void GenerateRandomSequence(TypePower typePower)
    {
        int i_IndexRandom;

        if (typePower == TypePower.Bonus)
        {
            for (int i = 0; i < bonusSequence.Length; i++)
            {
                do
                {
                    i_IndexRandom = Raylib.GetRandomValue(0, bonusAvailable.Length - 1);
                } while (CheckValueAlreadyInSequence(i_IndexRandom, bonusSequence, i));

                bonusSequence[i] = i_IndexRandom;
            }
        }
        else
        {
            for (int i = 0; i < malusSequence.Length; i++)
            {
                do
                {
                    i_IndexRandom = Raylib.GetRandomValue(0, malusAvailable.Length - 1);
                } while (CheckValueAlreadyInSequence(i_IndexRandom, malusSequence, i));

                malusSequence[i] = i_IndexRandom;
            }
        }
    }

    private bool CheckValueAlreadyInSequence(int i_IndexToCheck, int[] arrayToCheck, int i_CurrentIndex)
    {
        if (i_CurrentIndex == 0)
            return false;

        for (int i = 0; i < i_CurrentIndex; i++)
        {
            if (arrayToCheck[i] == i_IndexToCheck)
            {
                return true;
            }
        }

        return false;
    }

    // Update Methode called by Character Update
    public void UpdatePowerManager()
    {
        f_Timer += Raylib.GetFrameTime();

        if (f_Timer >= f_DelaySpawn && b_PowerHasBeenPicked)
        {
            TriggerSpawnPower();
            f_Timer = 0;
            b_PowerHasBeenPicked = false;
        }

        if (f_Timer >= f_DelayReroll && !b_PowerHasBeenPicked)
        {
            TriggerRerollPower();
            f_Timer = 0;
        }

        if (f_Timer >= f_DelayPowerPicked && b_PowerHasBeenPicked && (bonusDisplayed != null || malusDisplayed != null))
        {
            bonusDisplayed = null;
            malusDisplayed = null;
        }
    }

    // Method to trigger the spawn of the Power Bonus and Malus on the Board
    private void TriggerSpawnPower()
    {
        cellAvailables = characterOrigin.GetBoard().GetCellAvailable();

        if (cellAvailables.Count > 10)
        {
            // Managing the position of the Bonus and Malus
            AddPowerOnBoard(TypeCell.Bonus);
            AddPowerOnBoard(TypeCell.Malus);
        }

        cellAvailables.Clear();
    }

    // Method that Add the TypeCell on the correct cell of the Board and trigger the method to select the first element from the Sequence
    private void AddPowerOnBoard(TypeCell typeCell)
    {
        int indexCell = Raylib.GetRandomValue(0, cellAvailables.Count - 1);
        cellAvailables[indexCell].UpdateCell(typeCell);

        if (typeCell == TypeCell.Bonus)
            cellBonus = cellAvailables[indexCell];
        else
            cellMalus = cellAvailables[indexCell];

        cellAvailables.Remove(cellAvailables[indexCell]);


        SelectPowerSpawn(typeCell);
    }

    // Change the current Power displayed. We added a condition to avoid having the same power two times displayed when a full reroll has been triggered
    private void SelectPowerSpawn(TypeCell typeCell)
    {
        if (typeCell == TypeCell.Bonus)
        {
            if (bonusDisplayed != bonusAvailable[bonusSequence[i_IndexBonusSequence]])
            {
                bonusDisplayed = bonusAvailable[bonusSequence[i_IndexBonusSequence]];
                i_IndexBonusSequence++;
            }
            else
            {
                bonusDisplayed = bonusAvailable[bonusSequence[i_IndexBonusSequence + 1]];
                i_IndexBonusSequence += 2;
            }
        }
        else
        {
            if (malusDisplayed != malusAvailable[malusSequence[i_IndexMalusSequence]])
            {
                malusDisplayed = malusAvailable[malusSequence[i_IndexMalusSequence]];
                i_IndexMalusSequence++;
            }
            else
            {
                malusDisplayed = malusAvailable[malusSequence[i_IndexMalusSequence + 1]];
                i_IndexMalusSequence += 2;
            }
        }
    }

    // Change the current Power displayed by going to the next item on the Power Sequence. If we are at the end, relaunch a new sequence
    private void TriggerRerollPower()
    {
        if (i_IndexMalusSequence >= bonusSequence.Length)
        {
            InitBonusSequence();
            GenerateRandomSequence(TypePower.Bonus);
        }

        if (i_IndexMalusSequence >= malusSequence.Length)
        {
            InitMalusSequence();
            GenerateRandomSequence(TypePower.Malus);
        }

        SelectPowerSpawn(TypeCell.Bonus);
        SelectPowerSpawn(TypeCell.Malus);
    }

    // Method to retrieve a list of potential Target for the Malus, then return the character targetted
    public Character PickCharacterOpponent()
    {
        List<Character> listCharacterTarget = TableManager.RetrieveListCharacter(characterOrigin);

        int i_IndexRandom = Raylib.GetRandomValue(0, listCharacterTarget.Count - 1);

        return listCharacterTarget[i_IndexRandom];
    }

    // Function trigger by the collision with the cell
    public void TriggerBonusPower()
    {
        bonusDisplayed?.UsePower();

        malusDisplayed = null;
        cellMalus?.UpdateCell(TypeCell.None);

        b_PowerHasBeenPicked = true;

        f_DelaySpawn = 75;
        f_Timer = 0;
    }

    public void TriggerMalusPower()
    {
        malusDisplayed?.UsePower();

        bonusDisplayed = null;
        cellBonus?.UpdateCell(TypeCell.None);

        b_PowerHasBeenPicked = true;

        f_DelaySpawn = 75;
        f_Timer = 0;
    }
}