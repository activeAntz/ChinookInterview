using Chinook.ClientModels;

namespace Chinook.Services
{
    public interface IGlobalErrorService
    {
        List<MessageDto> GetAlertInfo();
        void SetInfo(string errorMessage);
        void SetError(string errorMessage);
        void ClearError();
    }
}
