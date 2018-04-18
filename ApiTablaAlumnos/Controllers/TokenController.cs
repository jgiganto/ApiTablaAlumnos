using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;

namespace ApiTablaAlumnos.Controllers
{
    public class TokenController : ApiController
    {
        CloudTable tabla;
        public TokenController()
        {
            String claves =
                CloudConfigurationManager.GetSetting("cuentastorage");
            CloudStorageAccount cuenta = CloudStorageAccount.Parse(claves);
            CloudTableClient cliente =
                cuenta.CreateCloudTableClient();
            this.tabla = cliente.GetTableReference("alumnos");
        }

        [Route("api/RecuperarToken/{curso}")]
        public string GetSas(string curso)
        {
            SharedAccessTablePolicy permisos =
                new SharedAccessTablePolicy
                {
                    SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(30),
                    Permissions = SharedAccessTablePermissions.Query

                };
            string token = tabla.GetSharedAccessSignature(permisos,
                null,
                curso, //incicio , rango alfabetico
                null,
                curso, //fin 
                null);
            return token;
        }
    }
   
}
