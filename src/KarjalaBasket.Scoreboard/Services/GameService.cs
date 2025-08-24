using KarjalaBasket.Scoreboard.Constants;
using KarjalaBasket.Scoreboard.Models;
using KarjalaBasket.Scoreboard.Settings;

namespace KarjalaBasket.Scoreboard.Services;

internal class GameService
{
    public GameModel CreateGame(GameSettings gameSettings) => new(gameSettings);

    public void NextPeriod(GameModel game)
    {
        game.Period++;

        if (game.IsExtraPeriod)
        {
            NextExtraPeriod(game);
            
            return;
        }
        
        game.PeriodTime = game.Settings.PeriodTime;
        
        game.TeamA.Fouls = game.TeamB.Fouls = 0;

        if (game.Period == BasketballNames.Period.StartOfFirstHalf)
        {
            game.TeamA.Timeouts = game.TeamB.Timeouts = game.Settings.FirstHalfTimeouts;
        }

        if (game.Period == BasketballNames.Period.StartOfSecondHalf)
        {
            game.TeamA.Timeouts = game.TeamB.Timeouts = game.Settings.SecondHalfTimeouts;
        }
        
        game.PossessionTime = game.Settings.PossessionTime;
    }

    public void ChangePeriod(GameModel game, byte period) => game.Period = period;

    private static void NextExtraPeriod(GameModel game)
    {
        game.PeriodTime = game.Settings.ExtraPeriodTime;
        game.TeamA.Timeouts = game.TeamB.Timeouts = game.Settings.ExtraPeriodTimeouts;
        game.PossessionTime = game.Settings.PossessionTime;
    }
}