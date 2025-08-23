using System.Diagnostics;
using KarjalaBasket.Scoreboard.Models;

namespace KarjalaBasket.Scoreboard.Services;

public class TimeService
{
    public void ChangePeriodTime(GameModel game, int minutes, int seconds = default, int milliseconds = default)
    {
        game.PeriodTime = new TimeSpan(0, 0, minutes, seconds, milliseconds);
    }
    
    public void ChangePossessionTime(GameModel game, int seconds, int milliseconds = default)
    {
        game.PossessionTime = new TimeSpan(0, 0, 0, seconds, milliseconds);
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
    }

    public void HandleTickForPossessionTime(GameModel game)
    {
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
}