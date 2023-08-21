namespace Chinook.ClientModels;

public class PlaylistTrack
{
    public long TrackId { get; set; } = new long();
    public string TrackName { get; set; } = string.Empty;
    public string AlbumTitle { get; set; } = string.Empty;
    public string ArtistName { get; set; } = string.Empty;
    public bool IsFavorite { get; set; } = new bool();

}