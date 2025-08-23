using KarjalaBasket.Scoreboard.Settings;

namespace KarjalaBasket.Scoreboard.Constants;

public static class GameKinds
{
    public static readonly GameSettings FibaStandart = new()
    {
        MaxPersonalFouls = 5,
        MaxTeamFouls = 5,
        PeriodTime = TimeSpan.FromMinutes(10),
        ExtraPeriodTime = TimeSpan.FromMinutes(5),
        PossessionTime = TimeSpan.FromSeconds(24),
        SecondPossessionTime = TimeSpan.FromSeconds(14),
        TimerTick = TimeSpan.FromMilliseconds(100),
        Timeouts = 3,
        ExtraPeriodTimeouts = 1,
        Periods = 4,
        PeriodDisplayFormat = "{0}ОТ",
        FreeThrowPoints = 1,
        MiddleShotPoints = 2,
        LongShotPoints = 3,
        MaxPlayersInTeam = 12
    };
}