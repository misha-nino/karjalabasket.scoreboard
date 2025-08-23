using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using KarjalaBasket.Scoreboard.Settings;

namespace KarjalaBasket.Scoreboard.Models;

public class TeamModel : INotifyPropertyChanged
{
    private string? _name;
    private ushort _points;
    private byte _timeouts;
    private byte _fouls;
    private bool _hasNextPossession;
    
    public GameSettings GameSettings { get; }

    public bool HasNextPossession
    {
        get => _hasNextPossession;
        set
        {
            _hasNextPossession = value;
            
            OnPropertyChanged();
        }
    }

    public string Name
    {
        get => _name ?? string.Empty;
        set
        {
            _name = value;
            
            OnPropertyChanged();
        }
    }

    public ObservableCollection<PlayerModel> Players { get; set; }

    public ushort Points
    {
        get => _points;
        set
        {
            _points = value;
            
            OnPropertyChanged();
        }
    }

    public byte Timeouts
    {
        get => _timeouts;
        set
        {
            _timeouts = value;
            
            OnPropertyChanged();
        }
    }

    public byte Fouls
    {
        get => _fouls;
        set
        {
            _fouls = value;
            
            OnPropertyChanged();
        }
    }

    public TeamModel(GameSettings gameSettings)
    {
        GameSettings = gameSettings;
        Players = [];
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