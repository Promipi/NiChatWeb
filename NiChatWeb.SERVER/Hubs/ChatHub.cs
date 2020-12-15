using Microsoft.AspNetCore.SignalR;
using NiChatWeb.SERVER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NiChatWeb.SERVER.Hubs
{
    public class ChatHub : Hub
    {
        public async void SendMessage(Message newMessage) //metodo para enviar un mensaje
        {
            await Clients.Others.SendAsync("ReceiveMessage",newMessage); //todos los que estaban a la espera del metodo lo ejecutan
          
        } //y que todos los usuarios lo reciban
    
        public async void CreateChat(Chat newChat)
        {
            await Clients.Caller.SendAsync("ReceiveCreation",newChat);
        }
    }
}
