using KarjalaBasket.Scoreboard.Models;

namespace KarjalaBasket.Scoreboard.Services;

public class PlayerService
{
    public void AddPoints(PlayerModel player, ushort points) => player.Points += points;
    
    public void ChangePoints(PlayerModel player, ushort points) => player.Points = points;

    public void ChangeFouls(PlayerModel player, byte fouls)
    {
        player.Fouls = fouls > player.GameSettings.MaxPersonalFouls 
            ? player.GameSettings.MaxPersonalFouls 
            : fouls;

        player.IsFoulOut = player.Fouls == player.GameSettings.MaxPersonalFouls;
    }

    public void ChangeName(PlayerModel player, string name) => player.Name = name;
    
    public void ChangeNumber(PlayerModel player, string number) => player.Number = number;
}