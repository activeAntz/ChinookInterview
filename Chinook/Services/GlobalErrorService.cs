using Chinook.ClientModels;
using Chinook.Core.Helper;

namespace Chinook.Services
{
    public class GlobalErrorService : IGlobalErrorService
    {
        public List<MessageDto> ErrorMessage { get;  set; } = new List<MessageDto>();

        public List<MessageDto> GetAlertInfo()
        {
            return ErrorMessage;
        }

        public void SetInfo(string errorMessage)
        {
            var alert = SetAlert(ErrorType.Info, errorMessage);
            ErrorMessage.Add(alert);
        }

        public void SetError(string errorMessage)
        {
            ErrorMessage.Add(SetAlert(ErrorType.Danger, errorMessage));
        }

        private MessageDto SetAlert(string type, string message)
        {
            return new MessageDto
            {
                Type = type,
                Message = message
            };
        }

        public void ClearError()
        {
            ErrorMessage = new List<MessageDto>();
        }
    }
}
