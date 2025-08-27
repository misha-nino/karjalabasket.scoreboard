using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using KarjalaBasket.Scoreboard.Models;
using KarjalaBasket.Scoreboard.Services;

namespace KarjalaBasket.Scoreboard.ViewModels;

public class TeamViewModel : INotifyPropertyChanged
{
    private readonly TeamService _teamService;
    
    public TeamModel Team { get; }

    public bool HasNextPossession => Team.HasNextPossession;
    
    public ushort Points
    {
        get => Team.Points; 
        set => _teamService.ChangePoints(Team, value);
    }

    public byte Timeouts 
    {
        get => Team.Timeouts; 
        set => _teamService.ChangeTimeouts(Team, value);
    }

    public byte Fouls 
    {
        get => Team.Fouls; 
        set => _teamService.ChangeFouls(Team, value);
    }

    public string Name 
    {
        get => Team.Name; 
        set => _teamService.ChangeName(Team, value);
    }
    
    public ObservableCollection<PlayerViewModel> Players { get; }
    
    public Command AddPlayerCommand { get; set; }

    public TeamViewModel(TeamModel team)
    {
        Players = [];
        
        _teamService = new TeamService();
        
        Team = team;

        Team.PropertyChanged += OnTeamChanged;
            
        Watch(Team.Players, Players);
        
        AddPlayerCommand = new Command
        (
            () => _teamService.AddPlayer(Team), 
            () => Team.Players.Count <= Team.GameSettings.MaxPlayersInTeam
        );
        
        Team.Players.CollectionChanged += (_, _) => AddPlayerCommand.ChangeCanExecute();
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
            case nameof(TeamModel.HasNextPossession):
                OnPropertyChanged(nameof(HasNextPossession));
                break;
        }
    }
}