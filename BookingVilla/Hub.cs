using Microsoft.AspNetCore.SignalR;

namespace BookingVilla
{
    public class NewsHub : Hub
    {
        public async Task SendNewsUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveNewsUpdate", message);
        }
    }
}
