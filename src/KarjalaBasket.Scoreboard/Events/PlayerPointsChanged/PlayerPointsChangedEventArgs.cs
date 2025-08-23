namespace KarjalaBasket.Scoreboard.Events.PlayerPointsChanged;

public class PlayerPointsChangedEventArgs : EventArgs
{
    public PlayerPointsChangedEventArgs(short pointsDifference, ushort points)
    {
        PointsDifference = pointsDifference;
        Points = points;
    }

    public virtual short PointsDifference { get; }

    public virtual ushort Points { get; }
    
    
}