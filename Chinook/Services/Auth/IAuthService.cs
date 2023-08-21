namespace Chinook.Services.Auth
{
    public interface IAuthService
    {
        Task<string> GetUserId();
    }
}
