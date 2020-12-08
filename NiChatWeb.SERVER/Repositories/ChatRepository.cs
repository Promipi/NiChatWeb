﻿using Microsoft.Data.SqlClient;
using NiChatWeb.SERVER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace NiChatWeb.SERVER.Repositories
{
    public class ChatRepository
    {
        private SqlConnection _connection = new SqlConnection("Data Source=PROMIPC\\GESTIONPROMIPI;Initial Catalog=NiChatWeb;User ID=sa;Password=750timalta;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public bool InsertChat(Chat chat)
        {
            var query = @"INSERT INTO [dbo].[Chat] (Name,Creation) VALUES
                        (@Name,@Creation)";
            var result = _connection.Execute(query.ToString(),
                            new Chat {Name = chat.Name,Creation = chat.Creation }); //ejecutamos el comando
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
                                join _union in db.UserChats on chat.Id equals _union.Fchat
                                join _user in db.Users on _union.FUser equals _user.Id
                                where _user.Id == idUser
                                select chat; //selccionamos todos los chats que pertencezcan al id del usuario
                    return chats.ToList(); //retornamos la lista de chats
                }
                
            }
            return null;
        }

        public Chat GetChat(int id)
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
