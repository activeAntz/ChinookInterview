namespace Chinook.ClientModels;

public class PlaylistTrackDto
{
    public long TrackId { get; set; } = 0;
    public string TrackName { get; set; } = string.Empty;
    public string AlbumTitle { get; set; } = string.Empty;
    public string ArtistName { get; set; } = string.Empty;
    public bool IsFavorite { get; set; } = new bool();

}