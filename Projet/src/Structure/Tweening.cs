using System.Numerics;
using Raylib_cs;

public static class Tweening
{
    public static float OutSin(float f_Time, float f_Value, float f_Distance, float f_Duration)
    {
        return f_Distance * MathF.Sin(f_Time / f_Duration * (MathF.PI / 2)) + f_Value;
    }

    public static float Lerp(float f_start, float f_end, float f_timer)
    {
        return f_start + (f_end - f_start) * f_timer;
    }

    public static Vector2 LerpVector2 (Vector2 v2_Start, Vector2 v2_end, float f_timer)
    {
        return v2_Start + (v2_end - v2_Start) * f_timer;
    }


}