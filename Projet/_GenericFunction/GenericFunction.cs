using System.Numerics;

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
}