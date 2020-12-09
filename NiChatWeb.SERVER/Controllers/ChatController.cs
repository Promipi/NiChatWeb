using Microsoft.AspNetCore.Mvc;
using NiChatWeb.SERVER.Models;
using NiChatWeb.SERVER.Models.Response;
using NiChatWeb.SERVER.Models;
using NiChatWeb.SERVER.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace NiChatWeb.SERVER.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private ChatRepository _chatRepository = new ChatRepository();
        #region HTTP GETS METODOS

        [HttpGet("/Chat")]
        public IActionResult GetAllChats()
        {
            
            return Ok(_chatRepository.GetAllChats() ); //obtenemos todos los chats);
        }
        [HttpGet("/Chat/idUser")]
        public IActionResult GetAllChatsByUser(int idUser)
        {

            return Ok(_chatRepository.GetAllChatsByUser(idUser:idUser)); //retornmao la lsita de chats del usuario
        }
        #endregion

        [HttpPost]
        public IActionResult InsertChat(Chat chat)
        {
            return Ok(_chatRepository.InsertChat(chat) ); //insertamos el chat
        }
        [HttpPut]
        public IActionResult UpdateChat(Chat chat)
        {    
            return Ok(_chatRepository.UpdateChat(chat) ); //modificamos el chat);
        }
        [HttpDelete]
        public IActionResult DeleteChat(int id)
        {
            return Ok(_chatRepository.DeleteChat(id: id) ); //eliminamos el chat especifico); //retornamos la respuesta
        }
    }
}
