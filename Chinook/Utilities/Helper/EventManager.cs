using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chinook.Utilities.Helper
{
    public class EventManager
    {
        public IndexEventArgs currentEventArgs = new IndexEventArgs();
        public event EventHandler<IndexEventArgs> changeEvent;
        public class IndexEventArgs : EventArgs { }

        public void Invoke()
        {
            if(currentEventArgs != null)
            {
                changeEvent.Invoke(this, currentEventArgs);
            }
        }
    }

}
