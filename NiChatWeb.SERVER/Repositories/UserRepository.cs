using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using NiChatWeb.Data;
using NiChatWeb.SERVER.Models;

namespace NiChatWeb.SERVER.Repositories
{
    public class UserRepository 
    {
        private SqlConnection _connection = new SqlConnection(Direc.SqlConnection);

        public bool InsertUser(User user) //para introducir un nuevo usuario o cambiar sus datos
        {
            var query = @"INSERT INTO [dbo].[User] (Username,Gmail,Password)
                        VALUES(@Username,@Gmail,@Password)"; //creamos el comando 
            var result = _connection.Execute(query.ToString(),
                new User { Username = user.Username, Gmail = user.Gmail, Password = user.Password }); //lo ejecutamos

            if (result > 0) //si pudo introducir el registro
                return true;
            return false; //si no retornamos false
        }

        public bool DeleteUser(User user = null, int? id = null)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            using(NiChatWebContext db = new NiChatWebContext() )
            {
                return db.Users.ToList();
            }
        }

        public List<User> GetUsersByChat(Chat chat = null, int? id = null)
        {
            if(id != null)
            {
                var query = string.Format(@"SELECT Id,Username,Gmail,Password FROM [dbo].[User_Chat] uc
                            INNER JOIN [dbo].[User] u ON u.Id =  uc.FUser WHERE u.Id = '{0}'
                            ",id); //seleccionamos todos los usuarios que pertenzecan a ese id de chat

                return _connection.Query<User>(query.ToString(), new { }).ToList(); //retornamos los usuarios              
            }
            return null;
        }

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        
    }
}
