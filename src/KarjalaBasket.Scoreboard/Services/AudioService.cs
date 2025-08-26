using Plugin.Maui.Audio;

namespace KarjalaBasket.Scoreboard.Services;

public class AudioService
{
    public async Task<AsyncAudioPlayer> CreatePlayerAsync(string filename)
    {
        if (!await FileSystem.AppPackageFileExistsAsync(filename))
        {
            throw new FileNotFoundException();
        }
        
        return AudioManager.Current.CreateAsyncPlayer(await FileSystem.OpenAppPackageFileAsync(filename));
    }
}