using System.Collections.ObjectModel;
using KarjalaBasket.Scoreboard.Models;
using KarjalaBasket.Scoreboard.Settings;
using KarjalaBasket.Scoreboard.ViewModels;

namespace KarjalaBasket.Scoreboard.Views;

public partial class PlayersView : ContentView
{
    public static readonly BindableProperty PlayersProperty = 
        BindableProperty.Create(nameof(Players), typeof(ObservableCollection<PlayerViewModel>), typeof(PlayersView));

    public ObservableCollection<PlayerViewModel> Players
    {
        get => (ObservableCollection<PlayerViewModel>)GetValue(PlayersProperty);
        set => SetValue(PlayersProperty, value);
    }
    
    public static readonly BindableProperty AddPlayerCommandProperty = 
        BindableProperty.Create(nameof(AddPlayerCommand), typeof(Command), typeof(PlayersView));

    public Command AddPlayerCommand
    {
        get => (Command)GetValue(AddPlayerCommandProperty);
        set => SetValue(AddPlayerCommandProperty, value);
    }
    
    public static readonly BindableProperty GameSettingsProperty = 
        BindableProperty.Create(nameof(GameSettings), typeof(GameSettings), typeof(PlayersView));
    
    public GameSettings GameSettings
    {
        get => (GameSettings)GetValue(GameSettingsProperty);
        set => SetValue(GameSettingsProperty, value);
    }
    
    public PlayersView()
    {
        InitializeComponent();
    }
}