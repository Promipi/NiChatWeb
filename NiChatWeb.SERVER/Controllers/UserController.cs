using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NiChatWeb.SERVER.Models;
using NiChatWeb.SERVER.Models.Response;
using NiChatWeb.SERVER.Repositories;

namespace NiChatWeb.SERVER.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {
        private UserRepository _userRepository = new UserRepository();

        
        [HttpPost]
        public IActionResult InsertUser(User user) //para insertar un usuario
        {
            return Ok(_userRepository.InsertUser(user)); //insertamos el usuario  
        }



        [HttpGet]
        public IActionResult GetAllUsers()
        { 
            return Ok(_userRepository.GetAllUsers() ); //obtenemos la lista);
        }

        
    }
}
