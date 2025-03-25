using Raylib_cs;

public class Timer
{
    private int i_TimerLife = Raylib.GetRandomValue(20,60);
    private float f_Timer = 0;

    public event Action? TimerDown;

    public int GetTimerLife() => i_TimerLife;

    public void IncreaseTimer() => i_TimerLife += 10;

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