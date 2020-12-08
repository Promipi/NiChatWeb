using Microsoft.AspNetCore.Mvc;
using NiChatWeb.SERVER.Models;
using NiChatWeb.SERVER.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NiChatWeb.SERVER.Controllers
{
    [ApiController]
    [   Route("[controller]")  ]
    public class MessageController : ControllerBase
    {
        private MessageRepository _messageRepository = new MessageRepository();

        [HttpGet]
        public IActionResult GetAllMessages()
        {
            return Ok(_messageRepository.GetAllMessages());
        }

        [HttpGet("/Message/idChat")]
        public IActionResult GetAllMessagesByChat(int idChat)
        {
            return Ok(_messageRepository.GetAllMessagesByChat(idChat: idChat)); //retornamos todos los mensajes de un chat en especfico
        }

       
    }

}
