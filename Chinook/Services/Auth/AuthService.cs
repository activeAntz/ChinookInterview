using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chinook.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly AuthenticationStateProvider AuthenticationStateProvider;

        public string? CurrentUserId { get; set; }

        public AuthService(AuthenticationStateProvider AuthenticationStateProvider)
        {
            this.AuthenticationStateProvider = AuthenticationStateProvider;
            CurrentUserId = GetUserId().Result;
        }

        private async Task<string?> GetUserId()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.FindFirst(u => u.Type.Contains(ClaimTypes.NameIdentifier))?.Value;
        }
    }
}
