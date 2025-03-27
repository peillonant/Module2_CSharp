using Raylib_cs;

public class Timer
{
    private int i_TimerLife = 60;
    private float f_Timer = 0;

    public event Action? TimerDown;

    public int GetTimerLife() => i_TimerLife;
    public void TimerLoose() => i_TimerLife = -GameInfo.Instance.GetNbPlayerAlive();

    public void IncreaseTimer(int i_nbIncrease) => i_TimerLife += i_nbIncrease;

    public void UpdateTimer()
    {
        if (i_TimerLife > 0)
            DecreaseTimer();
        else
            TimerDown?.Invoke();
    }

    private void DecreaseTimer() 
    {   
        f_Timer += Raylib.GetFrameTime();

        if (f_Timer >= 1.0f)
        {
            i_TimerLife--;
            f_Timer = 0;
        }
    }
}