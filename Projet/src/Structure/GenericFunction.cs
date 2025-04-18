using System;
using System.Numerics;
using Raylib_cs;

public static class GenericFunction
{
    public static Vector2 ChangePosition(Vector2 v2_Position, int i_Direction)
    {
        switch (i_Direction)
        {
            case 1:
                v2_Position.Y -= 1;
                break;
            case 2:
                v2_Position.X += 1;
                break;
            case 3:
                v2_Position.Y += 1;
                break;
            case 4:
                v2_Position.X -= 1;
                break;
        }

        return v2_Position;
    }

    // Method to alow the enemy snake to check if it can change direction or not
    public static bool UpdateTimer(ref float f_Timer)
    {
        f_Timer += Raylib.GetFrameTime();

        // The timerMove will be change with the variable of the current level of the game to speed the movement
        if (f_Timer > GameInfo.GetSpeedSnake())
        {
            f_Timer = 0;
            return true;
        }

        return false;
    }
}