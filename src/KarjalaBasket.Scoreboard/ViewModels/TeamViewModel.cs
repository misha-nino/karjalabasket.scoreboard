using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using KarjalaBasket.Scoreboard.Models;
using KarjalaBasket.Scoreboard.Services;

namespace KarjalaBasket.Scoreboard.ViewModels;

public class TeamViewModel : INotifyPropertyChanged
{
    private readonly TeamModel _team;
    private readonly TeamService _teamService;

    public ushort Points
    {
        get => _team.Points; 
        set => _teamService.ChangePoints(_team, value);
    }

    public byte Timeouts 
    {
        get => _team.Timeouts; 
        set => _teamService.ChangeTimeouts(_team, value);
    }

    public byte Fouls 
    {
        get => _team.Fouls; 
        set => _teamService.ChangeFouls(_team, value);
    }

    public string Name 
    {
        get => _team.Name; 
        set => _teamService.ChangeName(_team, value);
    }
    
    public ObservableCollection<PlayerViewModel> Players { get; }
    
    public Command AddPlayerCommand { get; set; }

    public TeamViewModel(TeamModel team)
    {
        Players = [];
        
        _teamService = new TeamService();
        
        _team = team;

        _team.PropertyChanged += OnTeamChanged;
            
        Watch(_team.Players, Players);
        
        AddPlayerCommand = new Command
        (
            () => _teamService.AddPlayer(_team), 
            () => _team.Players.Count <= _team.GameSettings.MaxPlayersInTeam
        );
        
        _team.Players.CollectionChanged += (_, _) => AddPlayerCommand.ChangeCanExecute();
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
    
    private static void Watch<T, T2> (ObservableCollection<T> sourceCollection, 
        ObservableCollection<T2> destinationCollection)
    {
        sourceCollection.CollectionChanged += (_, args) =>
        {
            if (args.NewItems?.Count == 1)
            {
                if (Activator.CreateInstance(typeof(T2), args.NewItems.OfType<T>().First()) is T2 newItem)
                {
                    destinationCollection.Add(newItem);
                }
            }

            if (args.OldItems?.Count == 1)
            {
                destinationCollection.RemoveAt(sourceCollection.IndexOf(args.OldItems.OfType<T>().First()));
            }
        };
    }
    
    private void OnTeamChanged(object? sender, PropertyChangedEventArgs args)
    {
        switch (args.PropertyName)
        {
            case nameof(TeamModel.Name):
                OnPropertyChanged(nameof(Name));
                break;
            case nameof(TeamModel.Points):
                OnPropertyChanged(nameof(Points));
                break;
            case nameof(TeamModel.Fouls):
                OnPropertyChanged(nameof(Fouls));
                break;
            case nameof(TeamModel.Timeouts):
                OnPropertyChanged(nameof(Timeouts));
                break;
        }
    }
}