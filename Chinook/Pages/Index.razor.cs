using Chinook.ClientModels;
using Chinook.Core.Data.Models;
using Serilog;

namespace Chinook.Pages
{
    public partial class Index
    {
        private List<ArtistsDto> Artists = new();
        protected override async Task OnInitializedAsync()
        {
            await InvokeAsync(StateHasChanged);
            await GetArtists();
        }

        public async Task GetArtists()
        {
            Artists = await artistService.GetArtistsAsync();
        }

        public async void SearchArtistByName(string artistName)
        {
            try
            {
                await GetArtists();
                if (!string.IsNullOrEmpty(artistName))
                    Artists = Artists.Where(c => c.Name.Contains(artistName)).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }

        }
    }
}