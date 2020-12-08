﻿using Microsoft.Data.SqlClient;
using NiChatWeb.SERVER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace NiChatWeb.SERVER.Repositories
{
    public class MessageRepository
    {
        private SqlConnection _connection = new SqlConnection("Data Source=PROMIPC\\GESTIONPROMIPI;Initial Catalog=NiChatWeb;User ID=sa;Password=750timalta;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public List<Message> GetAllMessages()
        {
            using(NiChatWebContext db = new NiChatWebContext() )
            {
                return db.Messages.ToList(); //retornamos toda la lista de mensajes
            }
        }

        public List<Message> GetAllMessagesByChat(Chat chat = null, int? idChat = null)
        {
            if(idChat != null)
            {
                using(NiChatWebContext db = new NiChatWebContext() )
                {
                    var a = db.Messages.Where(x => x.Fchat == idChat).ToList();
                    return db.Messages.Where(x => x.Fchat == idChat).ToList(); //retornamos los menmsajes de ese chat
                }
            }
            return null;
        }

        public List<Message> GetAllMessagesByUser(User user = null,int? idUser = null) //obtener mensjaes de un user especifico
        {
            using(NiChatWebContext db = new NiChatWebContext() )
            {
                if(user != null)
                {
                    return db.Messages.Where(x => x.FuserNavigation == user).ToList();
                }
                if(idUser != null)
                {
                    return db.Messages.Where(x => x.FUser == idUser).ToList(); //retornamos todos los mensajes de un usario especifico
                }
                
            }
            return null;
        }

        public List<Message> GetAllMessagesByChatByUser(int idChat,int idUser)
        {
            var query = @"Select Body,Creation from [dbo].[Messsage] 
                          WHERE Fchat = @idChat and FUser = @idUser";
            var messages = _connection.Query<Message>(query.ToString(), new { });
            return messages.ToList(); //retornamos la lista de mensajes en el chat especifico de un usuario especificos
        }
        /**/

        public bool InsertMessage(User from,Message newMessage,int idChat)
        {
            using (NiChatWebContext db = new NiChatWebContext())
            {
                var chat = db.Chats.First(x => x.Id == idChat); //obtenemos el chat al que meteremos el mensaje
                newMessage.FUser = from.Id; newMessage.FuserNavigation = from; //establecemos el propietario
                newMessage.Fchat = chat.Id; newMessage.FchatNavigation = chat; //establecemos en que chat estara
                chat.Messages.Add(newMessage);
                db.Messages.Add(newMessage); //anadmos el nuevo mensaje a la base de datos

                db.SaveChanges(); //guardamos cambios
                return true;
            }
        }
        /**/



    }
}