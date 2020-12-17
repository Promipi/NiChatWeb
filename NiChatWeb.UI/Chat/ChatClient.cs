using Microsoft.AspNetCore.SignalR.Client;
using NiChatWeb.Data;
using NiChatWeb.SERVER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net.Http;

namespace NiChatWeb.UI.Chat
{
    public class ChatClient
    { 
        public User User;
        public HubConnection _connection; //para establecer la conexion con el hub
        private HttpClient _http;          //la conexion con mi servicio rest

        public delegate void MessageEventHandler(Message message); //nuestro dlegado para crear un evento
        public event MessageEventHandler MessageReceivedEvent; //para cuando reicbimos un mensaje
        public event MessageEventHandler MessageDeletedEvent; //apra cuando eliminan un mensaje

        public ChatClient(User user,out bool ready)
        {
            this.User = user;
            _http = new HttpClient()
                        { BaseAddress = new Uri(Direc.ASP) }; //creamos la conexion   
            ready = true;
        }

        public async void StartAsync() //para comenzar la conexion y los metodos de espera
        {
            try
            {
                _connection = new HubConnectionBuilder()
                                 .WithUrl(Direc.HubConnection).Build(); //establecemenmos la nueva conexion

                await _connection.StartAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            _connection.On<Message>(Methods.RECEIVE, (message) => //nos ponemos a la espera del metodo recibir
            {
                if(MessageReceivedEvent != null)
                {
                    MessageReceivedEvent(message);
                }
            });

            _connection.On<Message>("ReceiveDelete", (message) =>
            {
                if(MessageDeletedEvent != null)
                {
                    MessageDeletedEvent(message); //llamamos al etodo que apunta
                }
            });
        }

        public async void SendMessage(Message newMessage)
        {
            try
            {
                var sendMessageByChat = string.Format("/Message/send"); //para decir que mandaremos un mensaje 
                var result = await _http.PostAsJsonAsync(sendMessageByChat, newMessage); //enviamos el nuevo Mensaje
                await _connection.SendAsync(Methods.SEND,newMessage); //decimos que se envio un mensaje
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
