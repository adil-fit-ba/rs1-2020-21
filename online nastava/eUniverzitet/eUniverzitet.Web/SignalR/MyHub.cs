using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace eUniverzitet.Web.SignalR
{
  //  [Authorize]
    public class MyHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            if (Context.User.Identity.Name != null)
                await Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
            await base.OnConnectedAsync();
        }
    }
}
