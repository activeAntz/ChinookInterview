namespace Chinook.ClientModels;

public class Playlist
{
    public string Name { get; set; } = string.Empty;
    public List<PlaylistTrack> Tracks { get; set; } = new List<PlaylistTrack>();
}