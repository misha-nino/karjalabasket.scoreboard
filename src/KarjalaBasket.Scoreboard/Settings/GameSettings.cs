namespace KarjalaBasket.Scoreboard.Settings;

public class GameSettings
{
    public required string ExtraPeriodDisplayFormat { get; set; }
    
    public byte FirstHalfTimeouts { get; set; }
    
    public byte SecondHalfTimeouts { get; set; }
    
    public byte ExtraPeriodTimeouts { get; set; }
    
    public byte MaxPersonalFouls { get; set; }
    
    public byte MaxTeamFouls { get; set; }

    public TimeSpan PeriodTime { get; set; }
    
    public TimeSpan ExtraPeriodTime { get; set; }

    public TimeSpan TimerTick { get; set; }

    public TimeSpan PossessionTime { get; set; }

    public TimeSpan SecondPossessionTime { get; set; }

    public ushort FreeThrowPoints { get; set; }

    public ushort MiddleShotPoints { get; set; }
    
    public ushort LongShotPoints { get; set; }
    
    public byte MaxPlayersInTeam { get; set; }
}