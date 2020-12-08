using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NiChatWeb.SERVER.Models.Response
{
    public class Response
    {
        public bool Success { get; set; } //para saber si se completo

        public string Message { get; set; }

        public object Data { get; set; } //los datos enviados

        public Response() => Success = false; //establecemos como predeterminado 
    }
}
