
using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace conexion
{
    class Program
    {
        static void Main(string[] args)
        {
            HubConnection connection;
            string _ulr = "https://localhost:5001/messagehub";
            connection = new HubConnectionBuilder().WithUrl(_ulr).Build();
            connection.StartAsync();
            connection.On<string>("RecibirMessage", (mensaje) =>
            {

            });
            connection.InvokeAsync("EnviarMessage");
            Console.WriteLine("SDa");
            Console.ReadKey();
        }
    }
}
