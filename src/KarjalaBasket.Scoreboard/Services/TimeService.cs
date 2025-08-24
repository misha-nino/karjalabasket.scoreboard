using KarjalaBasket.Scoreboard.Models;

namespace KarjalaBasket.Scoreboard.Services;

public class TimeService
{
    public event EventHandler? PeriodTimeIsUp;
    
    public void ChangePeriodTime(GameModel game, int minutes, int seconds = default, int milliseconds = default)
    {
        game.PeriodTime = new TimeSpan(0, 0, minutes, seconds, milliseconds);

        if (game.PeriodTime < game.PossessionTime)
        {
            game.PossessionTime = null;
        }
    }
    
    public void ChangePossessionTime(GameModel game, int? seconds, int? milliseconds = default)
    {
        game.PossessionTime = seconds.HasValue && seconds.Value < game.PeriodTime.TotalSeconds
            ? new TimeSpan(0, 0, 0, seconds.Value, milliseconds ?? 0)
            : null;
    }
    
    public void HandleTickForPeriodTime(GameModel game)
    {
        if (game.PeriodTime == TimeSpan.Zero)
        {
            return;
        }
        
        if (game.PeriodTime > game.Settings.TimerTick)
        {
            game.PeriodTime -= game.Settings.TimerTick;
            
            return;
        }

        game.PeriodTime = TimeSpan.Zero;

        OnPeriodTimeIsUp();
    }

    public void HandleTickForPossessionTime(GameModel game)
    {
        if (!game.PossessionTime.HasValue)
        {
            return;
        }
        
        if (game.PossessionTime == TimeSpan.Zero)
        {
            return;
        }
        
        if (game.PossessionTime > game.Settings.TimerTick)
        {
            game.PossessionTime -= game.Settings.TimerTick;
            
            return;
        }

        game.PossessionTime = TimeSpan.Zero;
    }

    private void OnPeriodTimeIsUp()
    {
        PeriodTimeIsUp?.Invoke(this, EventArgs.Empty);
    }
}