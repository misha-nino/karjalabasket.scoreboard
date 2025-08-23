using System.ComponentModel;
using System.Runtime.CompilerServices;
using KarjalaBasket.Scoreboard.Models;
using KarjalaBasket.Scoreboard.Services;

namespace KarjalaBasket.Scoreboard.ViewModels;

public class PlayerViewModel : INotifyPropertyChanged
{
    private readonly PlayerService _playerService;
    private readonly PlayerModel _player;
    
    public string Number
    {
        get => _player.Number; 
        set => _playerService.ChangeNumber(_player, value);
    }

    public string Name
    {
        get => _player.Name; 
        set => _playerService.ChangeName(_player, value);
    }

    public byte Fouls
    {
        get => _player.Fouls; 
        set => _playerService.ChangeFouls(_player, value);
    }

    public bool IsFoulOut => _player.IsFoulOut;

    public ushort Points
    {
        get => _player.Points; 
        set => _playerService.ChangePoints(_player, value);
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    
    public Command<ushort> AddPointsCommand { get; set; }

    public PlayerViewModel(PlayerModel player)
    {
        _playerService = new PlayerService();
        
        _player = player;

        _player.PropertyChanged += OnPlayerChanged;

        AddPointsCommand = new Command<ushort>(p => _playerService.AddPoints(_player, p));
    }
    
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
    
    private void OnPlayerChanged(object? sender, PropertyChangedEventArgs args)
    {
        switch (args.PropertyName)
        {
            case nameof(PlayerModel.Number):
                OnPropertyChanged(nameof(Number));
                break;
            case nameof(PlayerModel.Name):
                OnPropertyChanged(nameof(Name));
                break;
            case nameof(PlayerModel.Points):
                OnPropertyChanged(nameof(Points));
                break;
            case nameof(PlayerModel.Fouls):
                OnPropertyChanged(nameof(Fouls));
                break;
            case nameof(PlayerModel.IsFoulOut):
                OnPropertyChanged(nameof(IsFoulOut));
                break;
        }
    }
}