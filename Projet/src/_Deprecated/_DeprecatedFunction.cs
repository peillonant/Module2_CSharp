public class Deprecated
{
    private int GenerateNbCellSeen()
    {
        int i_EnemyLevel = 0;
        int result = 3;
        if (i_EnemyLevel == 1) return result;

        for (int i = 2; i <= i_EnemyLevel; i++)
        {
            result += 12 + (4 * (i - 3));
        }

        return result;
    }

    private void DrawPathfinding()
    {
        // if (!characterOrigin.IsPlayer())
        // {
        //     for (int x = 0; x < characterOrigin.GetMovementAlgorithm().GetPathfinder().GetCurrentPath().Count; x++)
        //     {
        //         if (characterOrigin.GetMovementAlgorithm().GetPathfinder().GetCurrentPath()[x].GetCellPosition() == v2_Pos)
        //         {
        //             Raylib.DrawRectangleLinesEx(recCell, 1f, Color.Green);
        //         }
        //     }
        // }
    }
}
