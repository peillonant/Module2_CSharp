using System;
using System.Numerics;
using Raylib_cs;

public class GenericFunction
{
    private static GenericFunction? instance;
    public static GenericFunction Instance
    {
        get
        {
            instance ??= new GenericFunction();
            return instance;
        }
    }

    public void ChangePosition (ref Vector2 v2_Position, int i_Direction)
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
    }

    // Method to alow the enemy snake to check if it can change direction or not
    public bool UpdateTimer(ref float f_Timer)
    {
        f_Timer += Raylib.GetFrameTime();
        
        // The timerMove will be change with the variable of the current level of the game to speed the movement
        if (f_Timer > GameInfo.Instance.GetSpeedSnake())
        {
            f_Timer = 0;
            return true;
        }

        return false;
    }

    // Tweening section
    public float Tweening_OutSin(float f_Time, float f_Value, float f_Distance, float f_Duration)
    {
        return f_Distance * MathF.Sin(f_Time/f_Duration * (MathF.PI/2)) + f_Value;
    }

}