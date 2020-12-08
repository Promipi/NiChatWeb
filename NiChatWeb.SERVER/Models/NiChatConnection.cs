using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NiChatWeb.SERVER.Models
{
    public class NiChatConnection : IDisposable 
    {
        public SqlConnection mySql;
        public string _chainConnection;
        public NiChatConnection(string chainConnection) =>
                                _chainConnection = chainConnection;
        public SqlConnection dbConnection() //para regresar una conexion con la base de datos
        {
            if(_chainConnection == null)
            {
                return new SqlConnection(_chainConnection);
            }
            throw new NotImplementedException("Cadena de conexion no establecida");
            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
