using Microsoft.AspNetCore.Mvc;
using NiChatWeb.SERVER.Models;
using NiChatWeb.SERVER.Models.Response;
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

        [HttpGet]
        public IActionResult GetAllChats()
        {
            return Ok(_chatRepository.GetAllChats()); //obtenemos todos los chats);
        }

        [HttpGet("user")]
        public IActionResult GetChatsByUser(int id)
        {
            return Ok(_chatRepository.GetAllChatsByUser(idUser: id)); //retornamos todos los chats de el usuario
        }

        [HttpGet("specific")]
        public IActionResult GetChat(int id)
        {
            return Ok(_chatRepository.GetChatById(id)); //retornamos el chat con ese id en esepcifico
        }


        [HttpPost("add")]
        public IActionResult InsertChat( UserChat joinChat)
        {
            return Ok(_chatRepository.InsertChat(joinChat) ); //insertamos el chat
        }

        [HttpPut("update")]
        public IActionResult UpdateChat(Chat chat)
        {
            return Ok(_chatRepository.UpdateChat(chat)); //modificamos el chat
        }
        [HttpDelete]
        public IActionResult DeleteChat(int id)
        {
            return Ok(_chatRepository.DeleteChat(id: id)); //eliminamos el chat especifico); //retornamos la respuesta
        }   
    }
}
