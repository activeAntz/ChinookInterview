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
    private List<PlaylistTrack> Tracks = new();
    private PlaylistTrack SelectedTrack = new();
    private string newPlayList = string.Empty;
    private long? existPlayList = null;
    private List<MessageDto> Message = new();
    private List<Playlists> PlayLists = new();

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

            if (state == 1)
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

            PlayLists = await playListService.GetFilterPlaylistsAsync(trackId);
            existPlayList = PlayLists.Select(c => c.playListId).FirstOrDefault();

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
            if (existPlayList != null)
                state = trackService.AddExistPlayList(SelectedTrack.TrackId, existPlayList);
            if (!string.IsNullOrEmpty(newPlayList))
            {
                var (isAdded, newPlayListId) = playListService.AddNewPlaylist(newPlayList);
                if (!isAdded)
                    globalErrorService.SetError($"The {newPlayList} Playlist already contains the playlists");
                state = trackService.AddExistPlayList(SelectedTrack.TrackId, newPlayListId);
            }

            if (state == 1)
                globalErrorService.SetInfo($"Track {Artist.Name} - {SelectedTrack.AlbumTitle} - {SelectedTrack.TrackName} added to playlist {newPlayList}.");
            else
                globalErrorService.SetError($"Track {Artist.Name} - {SelectedTrack.AlbumTitle} - {SelectedTrack.TrackName} can not added to playlist {newPlayList}.");

            newPlayList = string.Empty;
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