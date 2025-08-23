using KarjalaBasket.Scoreboard.Events.PlayerPointsChanged;
using KarjalaBasket.Scoreboard.Models;

namespace KarjalaBasket.Scoreboard.Services;

public class TeamService
{
    public void ChangePoints(TeamModel team, ushort points) => team.Points = points;

    public void ChangeTimeouts(TeamModel team, byte timeouts) => team.Timeouts = timeouts;

    public void ChangeFouls(TeamModel team, byte fouls) =>
        team.Fouls = fouls > team.GameSettings.MaxTeamFouls 
            ? team.GameSettings.MaxTeamFouls 
            : fouls;

    public void ChangeName(TeamModel team, string name) => team.Name = name;
    
    public void AddPlayer(TeamModel team)
    {
        var player = new PlayerModel(team.GameSettings);

        player.PointsChanged += (_, args) => OnPlayerPointsChanged(team, args.PointsDifference);
        player.FoulsChanged += (_, args) => OnPlayerFoulsChanged(team, args.FoulsDifference);
        
        team.Players.Add(player);
    }
    
    private void OnPlayerPointsChanged(TeamModel team, short pointsDifference) =>
        ChangePoints(team, (ushort)(pointsDifference + team.Points));
    
    private void OnPlayerFoulsChanged(TeamModel team, byte foulsDifference) =>
        ChangeFouls(team, (byte)(foulsDifference + team.Fouls));
}