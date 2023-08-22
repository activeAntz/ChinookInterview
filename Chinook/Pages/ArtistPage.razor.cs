using global::Microsoft.AspNetCore.Components;
using Chinook.Shared.Components;
using Chinook.ClientModels;
using Chinook.Utilities.Validation;
using Serilog;

namespace Chinook.Pages;
public partial class ArtistPage
{
    [Parameter]
    public long ArtistId { get; set; }

    private Modal PlaylistDialog { get; set; } = new Modal();
    private ArtistDto Artist = new();
    private List<PlaylistTrackDto> Tracks = new();
    private PlaylistTrackDto SelectedTrack = new();
    private string PlaylistName = string.Empty;
    private long ExistPlaylist = 0;
    private List<MessageDto> Message = new();
    private List<PlaylistsDto> Playlists = new();

    protected override async Task OnInitializedAsync()
    {
        await InvokeAsync(StateHasChanged);
        Artist = artistService.GetArtist(ArtistId);
        Tracks = trackService.GetPlaylistTracksByArtistId(ArtistId);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await OnInitializedAsync();
        Message = globalErrorService.GetAlertInfo();
    }

    private void FavoriteTrack(long trackId)
    {
        try
        {
            var track = Tracks.FirstOrDefault(t => t.TrackId == trackId);

            Guard.ThrowIfObjectNotFount(track);

            var state = trackService.AddFavoriteTrack(trackId);

            if (state > 0)
                globalErrorService.SetInfo($"Track {track.ArtistName} - {track.AlbumTitle} - {track.TrackName} added to playlist Favorites.");
            else
                globalErrorService.SetError($"Track {track.ArtistName} - {track.AlbumTitle} - {track.TrackName} can not added to playlist Favorites.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }

    private void UnfavoriteTrack(long trackId)
    {
        try
        {
            var track = Tracks.FirstOrDefault(t => t.TrackId == trackId);

            Guard.ThrowIfObjectNotFount(track);

            var (state, name) = trackService.RemoveTrack(trackId);

            if (state)
                globalErrorService.SetInfo($"Track {track.ArtistName} - {track.AlbumTitle} - {track.TrackName} removed from playlist Favorites.");
            else
                globalErrorService.SetInfo($"Track {track.ArtistName} - {track.AlbumTitle} - {track.TrackName} can not removed from playlist Favorites.");
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message, ex);
            throw;
        }
    }

    private async void OpenPlaylistDialog(long trackId)
    {
        try
        {
            CloseInfoMessage();
            SelectedTrack = Tracks.FirstOrDefault(t => t.TrackId == trackId);

            Guard.ThrowIfObjectNotFount(SelectedTrack);

            Playlists = await playListService.GetFilterPlaylistsByTrackIdAsync(trackId);
            ExistPlaylist = Playlists.Select(c => c.PlaylistId).FirstOrDefault();

            PlaylistDialog.Open();
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message, ex);
            throw;
        }
    }

    private void AddTrackToPlaylist()
    {
        try
        {
            CloseInfoMessage();
            var state = 0;

            if (ExistPlaylist != 0)
                state = trackService.AddExistPlayList(SelectedTrack.TrackId, ExistPlaylist);

            if (!string.IsNullOrEmpty(PlaylistName))
            {
                var (isAdded, playlistId) = playListService.AddPlaylist(PlaylistName);

                if (!isAdded)
                    globalErrorService.SetError($"The {PlaylistName} playlist already contains in the playlists");

                if (ExistPlaylist == 0) state = trackService.AddExistPlayList(SelectedTrack.TrackId, playlistId);
            }

            if (state > 0)
                globalErrorService.SetInfo($"Track {Artist.Name} - {SelectedTrack.AlbumTitle} - {SelectedTrack.TrackName} added to playlist {PlaylistName}.");
            else
                globalErrorService.SetError($"Track {Artist.Name} - {SelectedTrack.AlbumTitle} - {SelectedTrack.TrackName} can not added to playlist {PlaylistName}.");

            PlaylistName = string.Empty;
            PlaylistDialog.Close();
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message, ex);
            throw;
        }
    }

    public void CloseInfoMessage()
    {
        globalErrorService.ClearError();
    }
}