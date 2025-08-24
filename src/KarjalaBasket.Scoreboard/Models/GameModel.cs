using System.ComponentModel;
using System.Runtime.CompilerServices;
using KarjalaBasket.Scoreboard.Helpers;
using KarjalaBasket.Scoreboard.Settings;

namespace KarjalaBasket.Scoreboard.Models;

public class GameModel : INotifyPropertyChanged
{
    private TimeSpan _periodTime;
    private TimeSpan? _possessionTime;
    private TeamModel _teamA;
    private TeamModel _teamB;
    private byte _period;
    
    public GameSettings Settings { get; }
    
    public byte Period
    {
        get => _period;
        set
        {
            _period = value;
            
            OnPropertyChanged();
            OnPropertyChanged(nameof(PeriodDisplay));
            OnPropertyChanged(nameof(IsExtraPeriod));
        }
    }

    public bool IsExtraPeriod => 
        PeriodHelper.IsExtraPeriod(Period, Settings.Periods);

    public string PeriodDisplay => 
        PeriodHelper.PeriodToString(Period, Settings.Periods, Settings.PeriodDisplayFormat);

    public TimeSpan PeriodTime
    {
        get => _periodTime;
        set
        {
            _periodTime = value;
            
            OnPropertyChanged();
        }
    }

    public TimeSpan? PossessionTime
    {
        get => _possessionTime;
        set
        {
            _possessionTime = value;
            
            OnPropertyChanged();
        }
    }

    public TeamModel TeamA
    {
        get => _teamA;
        set
        {
            _teamA = value;
            
            OnPropertyChanged();
        }
    }

    public TeamModel TeamB
    {
        get => _teamB;
        set
        {
            _teamB = value;
            
            OnPropertyChanged();
        }
    }

    public GameModel(GameSettings settings)
    {
        Settings = settings;
        TeamA = new TeamModel(settings);
        TeamB = new TeamModel(settings);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}