using System.ComponentModel;

public struct SnakeBrain
{
    #region Boolean
    public bool b_FocusApple;
    public bool b_FocusBonus;
    public bool b_CanSeeBorder;
    public bool b_CanSeeObstacle;
    public bool b_CanSeeOwnBody;
    #endregion

    #region Value & Threshold
    private const float f_ThresholdFactor = 2.5f;
    private const float f_ValueFactor = 2.25f;
    public int i_ValueMax = 10;
    public int i_ThresholdApple = 3;
    public int i_ThresholdBonus = 2;
    public int i_ThresholdBorder = 1;
    public int i_ThresholdObstacle = 2;
    public int i_ThresholdOwnBody = 4;
    public int i_ThresholdChangeDirection = 2;
    public int i_ThresholdChangeDirectionBonusTargeted = 5;
    #endregion

    public SnakeBrain(int i_EnemyLevel)
    {
        ComputeValuePriorisation(i_EnemyLevel);
        ComputeValueObstacle(i_EnemyLevel);
        ComputeValueChangeDirection(i_EnemyLevel);
    }

    // Methode that will compute the threshold and the ValueMax for the Random of SnakePriorisation
    // Rule: threshold are multiply by 2.5 and the Value Max by 2.25
    private void ComputeValuePriorisation(int i_EnemyLevel)
    {
        for (int i = 1; i < i_EnemyLevel; i++)
        {
            i_ValueMax = (int) (i_ValueMax * f_ValueFactor);
            i_ThresholdApple = (int) (i_ThresholdApple * f_ThresholdFactor);
            i_ThresholdBonus = (int) (i_ThresholdBonus * f_ThresholdFactor);
        }
    }

    private void ComputeValueObstacle(int i_EnemyLevel)
    {
        for (int i = 1; i < i_EnemyLevel; i++)
        {
            i_ThresholdObstacle = (int) (i_ThresholdObstacle * f_ThresholdFactor);
            i_ThresholdBorder = (int) (i_ThresholdBorder * f_ThresholdFactor);
            i_ThresholdOwnBody = (int) (i_ThresholdOwnBody * f_ThresholdFactor);
        }
    }

    private void ComputeValueChangeDirection(int i_EnemyLevel)
    {       
        for (int i = 1; i < i_EnemyLevel; i++)
        {
            i_ThresholdChangeDirection = Math.Min( (int) (i_ThresholdChangeDirection * f_ThresholdFactor), i_ValueMax);
            i_ThresholdChangeDirectionBonusTargeted = Math.Min( (int) (i_ThresholdChangeDirectionBonusTargeted * f_ThresholdFactor), i_ValueMax);
        }
    }
}