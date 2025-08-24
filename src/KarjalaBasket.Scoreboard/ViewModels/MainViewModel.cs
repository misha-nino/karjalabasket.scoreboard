using System.ComponentModel;
using System.Runtime.CompilerServices;
using KarjalaBasket.Scoreboard.Constants;
using KarjalaBasket.Scoreboard.Models;
using KarjalaBasket.Scoreboard.Services;
using KarjalaBasket.Scoreboard.Settings;

namespace KarjalaBasket.Scoreboard.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly IDispatcherTimer _timer;
    private readonly GameService _gameService;
    private readonly TimeService _timeService;
    private readonly GameModel _game;

    public event PropertyChangedEventHandler? PropertyChanged;

    public bool CanEdit => !_timer.IsRunning;

    public TeamViewModel TeamA { get; }
    
    public TeamViewModel TeamB { get; }

    public byte Period
    {
        get => _game.Period; 
        set => _gameService.ChangePeriod(_game, value);
    }
    
    public int PeriodMinutes
    {
        get => _game.PeriodTime.Minutes;
        set => _timeService.ChangePeriodTime(_game, value, _game.PeriodTime.Seconds, _game.PeriodTime.Milliseconds);
    }

    public int PeriodSeconds
    {
        get => _game.PeriodTime.Seconds;
        set => _timeService.ChangePeriodTime(_game, _game.PeriodTime.Minutes, value, _game.PeriodTime.Milliseconds);
    }

    public int PeriodMilliseconds
    {
        get => _game.PeriodTime.Milliseconds;
        set => _timeService.ChangePeriodTime(_game, _game.PeriodTime.Minutes, _game.PeriodTime.Seconds, value);
    }

    public int? PossessionSeconds
    {
        get => _game.PossessionTime?.Seconds;
        set => _timeService.ChangePossessionTime(_game, value, _game.PossessionTime?.Milliseconds);
    }

    public int? PossessionMilliseconds
    {
        get => _game.PossessionTime?.Milliseconds;
        set => _timeService.ChangePossessionTime(_game, _game.PossessionTime?.Seconds, value);
    }

    public string PlayImageButtonSource => _timer.IsRunning ? "pause.png" : "play.png";

    public GameSettings GameSettings => _game.Settings;
    
    public Command PlayCommand { get; set; }

    public Command<int?> UpdatePossessionTimeCommand { get; set; }
    
    public MainViewModel()
    {
        _gameService = new GameService();
        _timeService = new TimeService();
        _timer = Application.Current!.Dispatcher.CreateTimer();
        
        _game = _gameService.CreateGame(GameKinds.FibaStandart);

        _game.PropertyChanged += OnGameChanged;
        _game.PeriodTimeIsUp += OnPeriodTimeIsUp;
        
        _timer.Interval = _game.Settings.TimerTick;
        _timer.Tick += (_, _) => _timeService.HandleTickForPeriodTime(_game);
        _timer.Tick += (_, _) => _timeService.HandleTickForPossessionTime(_game);
        
        TeamA = new TeamViewModel(_game.TeamA);
        TeamB = new TeamViewModel(_game.TeamB);

        PlayCommand = new Command(OnPlay);
        
        UpdatePossessionTimeCommand = new Command<int?>(s => _timeService.ChangePossessionTime(_game, s));
        
        _gameService.NextPeriod(_game);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return false;
        }

        field = value;
        
        OnPropertyChanged(propertyName);
        
        return true;

    }

    private void OnPlay() => StartStopTimer();

    private void StartStopTimer(bool? start = null)
    {
        if ((start.HasValue && !start.Value) || _timer.IsRunning)
        {
            _timer.Stop();
        }   
        else if ((start.HasValue && start.Value) || !_timer.IsRunning)
        {
            _timer.Start();
        }
        
        OnPropertyChanged(nameof(PlayImageButtonSource));
        OnPropertyChanged(nameof(CanEdit));
    }

    private void OnGameChanged(object? sender, PropertyChangedEventArgs args)
    {
        switch (args.PropertyName)
        {
            case nameof(GameModel.PeriodTime):
                OnPropertyChanged(nameof(PeriodMinutes));
                OnPropertyChanged(nameof(PeriodSeconds));
                OnPropertyChanged(nameof(PeriodMilliseconds));
                break;
            case nameof(GameModel.PossessionTime):
                OnPropertyChanged(nameof(PossessionSeconds));
                OnPropertyChanged(nameof(PossessionMilliseconds));
                break;
            case nameof(GameModel.Period):
                OnPropertyChanged(nameof(Period));
                break;
        }
    }

    private void OnPeriodTimeIsUp(object? sender, EventArgs args)
    {
        StartStopTimer(false);
    }
}