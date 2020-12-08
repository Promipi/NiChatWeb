using NiChatWeb.SERVER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NiChatWeb.SERVER.Services
{
    public interface IUserRepository
    {
        public bool SaveUser(User user);

        public bool DeleteUser(User user = null,int? id = null); //para eliminar un usuario

        public List<User> GetAllUsers(); //obtenemos los usarios en la base de datos

        public List<User> GetUsersByChat(Chat chat = null,int? id = null); //para obtener todos los usuarios de un chat

        public bool UpdateUser(User user);
    }
}
