namespace KarjalaBasket.Scoreboard.Events.PlayerFoulsChanged;

public class PlayerFoulsChangedEventArgs : EventArgs
{
    public PlayerFoulsChangedEventArgs(byte foulsDifference, byte fouls)
    {
        FoulsDifference = foulsDifference;
        Fouls = fouls;
    }

    public virtual byte FoulsDifference { get; }

    public virtual byte Fouls { get; }
    
    
}