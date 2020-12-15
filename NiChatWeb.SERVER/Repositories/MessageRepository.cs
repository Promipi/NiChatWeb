using Microsoft.Data.SqlClient;
using NiChatWeb.SERVER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NiChatWeb.SERVER.Models;
using NiChatWeb.Data;

namespace NiChatWeb.SERVER.Repositories
{
    public class MessageRepository
    {
        private SqlConnection _connection = new SqlConnection(Direc.SqlConnection);

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
                    return db.Messages.Where(x => x.FChat == idChat).ToList(); //retornamos los menmsajes de ese chat
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

            var messages = _connection.Query<Message>(query.ToString(), new { }); //obtenemos los mensajes del usaurio

            return messages.ToList(); //retornamos la lista de mensajes en el chat especifico de un usuario especificos
        }
        /**/

        public bool InsertMessage( Message newMessage,int? idChat)
        {
            var query = @"INSERT INTO [dbo].[Message]  (FUser,FChat,Body,Creation)
                          VALUES(@FUser,@FChat,@Body,@Creation)"; //comando para insertar el nuevo mensaaje
            int result = _connection.Execute(query, new Message {
                                FUser = newMessage.FUser, FChat = newMessage.FChat, Body = newMessage.Body, Creation = DateTime.Now });
            if (result > 0) //si se logro insertar  retornaos true
                return true;
            return false;    
        }
        /**/



    }
}
