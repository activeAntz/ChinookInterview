using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chinook.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly AuthenticationState authenticationState;

        public AuthService(AuthenticationState authenticationState)
        {
            this.authenticationState = authenticationState;
        }

        public async Task<string> GetUserId()
        {
            var user = (await authenticationState).User;
            var userId = user.FindFirst(u => u.Type.Contains(ClaimTypes.NameIdentifier))?.Value;
            return userId;
        }
    }
}
