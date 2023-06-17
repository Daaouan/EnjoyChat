using System;
using System.Threading.Tasks;
using Chat_application.Dtos;
using Chat_application.Services;
using Microsoft.AspNetCore.SignalR;

namespace Chat_application.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatService _chatService;

        public ChatHub(ChatService chatService)
        {
            _chatService = chatService; 
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "EnjoyChat");
            await Clients.Caller.SendAsync("USerConnected");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "EnjoyChat");
            var user = _chatService.GetUserByConnectionId(Context.ConnectionId);
            _chatService.RemoveUserFromList(user);
            await DisplayOnlineUsers();

            await base.OnDisconnectedAsync(exception);
        }

        public async Task AddUserConnectionId(string name)
        {
            _chatService.AddUserConnectionId(name, Context.ConnectionId);
            await DisplayOnlineUsers();
        }

        public async Task ReceiveMessage(MessageDto message)
        {
            await Clients.Group("EnjoyChat").SendAsync("NewMessage", message);
        }

        private async Task DisplayOnlineUsers()
        {
            var onlineUsers = _chatService.GetOnlineUsers();
            await Clients.Groups("EnjoyChat").SendAsync("OnlineUsers", onlineUsers);
        }

    }
}
