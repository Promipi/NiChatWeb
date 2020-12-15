using Microsoft.Data.SqlClient;
using NiChatWeb.SERVER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using NiChatWeb.Data;

namespace NiChatWeb.SERVER.Repositories
{
    public class ChatRepository
    {
        private SqlConnection _connection = new SqlConnection(Direc.SqlConnection);
        public bool InsertChat( UserChat joinChat)
        {
            var query = @"INSERT INTO [dbo].[Chat] (Name,Creation) VALUES
                        (@Name,@Creation)"; //para insertar un chat

            var newChat = new Chat { Name = joinChat.FchatNavigation.Name, Creation = DateTime.Now };
            var result = _connection.Execute(query.ToString(), newChat); //ejecutamos el comando para crear el nuevo chat

            var ultimoRegistroChat = "SELECT * FROM [dbo].[Chat] WHERE id=(SELECT max(id) FROM [dbo].[Chat])";
            var ultimoChat = _connection.Query<Chat>(ultimoRegistroChat); //obtenemos el ultimo regstro

            joinChat.FChat = ultimoChat.First().Id;

            var queryJoin = @"INSERT INTO [dbo].[User_Chat] (FUser,FChat) VALUES
                            (@FUser,@FChat)";
            _connection.Execute(queryJoin, joinChat); //introducimos la union para que el usuario pertenezca a ese chat

            if (result > 0)
                return true;
            return false;
        }

        public bool DeleteChat(Chat chat = null, int? id = null)
        {
            if(id != null)
            {
                var query = @"Delete from [dbo].[User] where Id  = @Id";
                var result = _connection.Execute(query.ToString(),new { Id = id }); //ejecutamos el comando para eliminar un chat
                if (result > 0)
                    return true;
            }
            return false; 

        }

        public List<Chat> GetAllChats()
        {
            using(NiChatWebContext db = new NiChatWebContext() )
            {
                 return db.Chats.ToList(); //retornamos todos los chats              
            }
        }

        public List<Chat> GetAllChatsByUser(User user = null, int ? idUser = null) //obtener chats de un user en especifico
        {
            
            if(idUser != null)
            {
                using(NiChatWebContext db = new NiChatWebContext() )
                {
                    var chats = from chat in GetAllChats()
                                join _union in db.UserChats on chat.Id equals _union.FChat
                                join _user in db.Users on _union.FUser equals _user.Id
                                where _user.Id == idUser
                                select chat; //selccionamos todos los chats que pertencezcan al id del usuario
                    return chats.ToList(); //retornamos la lista de chats
                }
                
            }
            return null;
        }

        public Chat GetChatById(int id)
        {
           using(NiChatWebContext db = new NiChatWebContext() )
           {
                return db.Chats.First(x => x.Id == id); //retornamos el primer chat que tenga ese id       
           }
        }

       
        public bool UpdateChat(Chat chat)
        {
            using(NiChatWebContext db = new NiChatWebContext() )
            {
                db.Entry(chat).State = EntityState.Modified; //decimos que el estdo del chat cambio
                db.SaveChanges();
                return true;
            }
            
        }
    }
}
