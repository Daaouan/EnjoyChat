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
            await Clients.Caller.SendAsync("UserConnected");
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

        public async Task CreatePrivateChat(MessageDto message)
        {
            string privateGroupName = GetPrivateGroupName(message.From, message.To);
            await Groups.AddToGroupAsync(Context.ConnectionId, privateGroupName);
            var toConnectionId = _chatService.GetConnectionIdByUser(message.To);
            await Groups.AddToGroupAsync(toConnectionId, privateGroupName);

            //open private chat for the other user
            await Clients.Client(toConnectionId).SendAsync("OpenPrivateChat", message);
        }

        public async Task RecivePrivateMessage(MessageDto message)
        {
            string privateGroupName = GetPrivateGroupName(message.From, message.To);
            await Clients.Group(privateGroupName).SendAsync("NewPrivateMessage", message);
        }

        public async Task RemovePrivateChat(string from, string to)
        {
            string privateGroupName = GetPrivateGroupName(from, to);
            await Clients.Group(privateGroupName).SendAsync("ClosePrivateChat");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, privateGroupName);
            var toConnectionId = _chatService.GetConnectionIdByUser(to);
            await Groups.RemoveFromGroupAsync(toConnectionId, privateGroupName);
        }

        private string GetPrivateGroupName(string from, string to) 
        {
            //user1 to user2 : user1-user2
            var stringCompare = string.CompareOrdinal(from, to) < 0;
            return stringCompare ? $"{from}-{to}" : $"{to}-{from}";
        }

        private async Task DisplayOnlineUsers()
        {
            var onlineUsers = _chatService.GetOnlineUsers();
            await Clients.Groups("EnjoyChat").SendAsync("OnlineUsers", onlineUsers);
        }

    }
}
