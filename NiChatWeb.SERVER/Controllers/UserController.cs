using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NiChatWeb.SERVER.Models;
using NiChatWeb.SERVER.Services;
using NiChatWeb.SERVER.Models.Response;

namespace NiChatWeb.SERVER.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {
        private UserRepository _userRepository = new UserRepository();

        
        [HttpPost]
        public void InsertUser(User user) //para insertar un usuario
        {
            Response oRespuesta = new Response(); //creamos la respuesta del metodo

            try
            {
                _userRepository.SaveUser(user); //insertamos el usuario
                oRespuesta.Success = 1;
            }
            catch(Exception ex)
            {
                oRespuesta.Message = ex.Message;
            }

            
        }

        [HttpGet]
        public List<User> GetAllUsers()
        {
            Response oRespuesta = new Response(); //creamos la respuesta del metodo
            try
            {
                oRespuesta.Data = _userRepository.GetAllUsers(); //obtenemos la lista
                oRespuesta.Success = 1;
                return (List<User>)oRespuesta.Data; //devolvemos la lista de usuarios
            }
            catch (Exception ex)
            {
                oRespuesta.Message = ex.Message;
            }
            return null;
        }

        [HttpGet]
        public List<User> GetUserByChat(int id)
        {
            Response oRespuesta = new Response(); //creamos la respuesta del metodo
            try
            {
                oRespuesta.Data = _userRepository.GetUsersByChat(id:id); //obtenemos la lista de usuarios en un chat determinado
                oRespuesta.Success = 1;
                return (List<User>)oRespuesta.Data; //devolvemos la lista de usuarios
            }
            catch (Exception ex)
            {
                oRespuesta.Message = ex.Message;
            }
            return null;
        }
    }
}
