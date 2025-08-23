using System.ComponentModel;
using System.Runtime.CompilerServices;
using KarjalaBasket.Scoreboard.Events.PlayerFoulsChanged;
using KarjalaBasket.Scoreboard.Events.PlayerPointsChanged;
using KarjalaBasket.Scoreboard.Settings;

namespace KarjalaBasket.Scoreboard.Models;

public class PlayerModel : INotifyPropertyChanged
{
    private ushort _points;
    private byte _fouls;
    private string? _name;
    private string? _number;
    private bool _isFoulOut;

    public event PropertyChangedEventHandler? PropertyChanged;
    
    public event PlayerPointsChangedEventHandler? PointsChanged;
    
    public event PlayerFoulsChangedEventHandler? FoulsChanged;

    public GameSettings GameSettings { get; }
    
    public string Number
    {
        get => _number ?? string.Empty;
        set
        {
            _number = value; 
            
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

    public byte Fouls
    {
        get => _fouls;
        set
        {
            var difference = value - _fouls;
            
            _fouls = value;

            if (difference > 0)
            {
                OnFoulsChanged((byte)difference, _fouls);
            }
            
            OnPropertyChanged();
        }
    }

    public bool IsFoulOut
    {
        get => _isFoulOut;
        set
        {
            _isFoulOut = value; 
            
            OnPropertyChanged();
        }
    }

    public ushort Points
    {
        get => _points;
        set
        {
            var difference = (short)(value - _points);
            
            _points = value;
            
            OnPointsChanged(difference, _points);
            
            OnPropertyChanged();
        }
    }

    public PlayerModel(GameSettings gameSettings)
    {
        GameSettings = gameSettings;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnFoulsChanged(byte foulsDifference, byte totalFouls)
    {
        FoulsChanged?.Invoke(this, new PlayerFoulsChangedEventArgs(foulsDifference, totalFouls));
    }

    protected virtual void OnPointsChanged(short pointsDifference, ushort totalPoints)
    {
        PointsChanged?.Invoke(this, new PlayerPointsChangedEventArgs(pointsDifference, totalPoints));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}