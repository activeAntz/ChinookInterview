namespace Chinook.ClientModels;

public class PlaylistDto
{
    public string Name { get; set; } = string.Empty;
    public List<PlaylistTrackDto> Tracks { get; set; } = new List<PlaylistTrackDto>();
}