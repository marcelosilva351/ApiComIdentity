using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("api/autorizar")]
    public class AutenticarController : ControllerBase
    {


       [Authorize(Roles ="Gerente")]
       [HttpGet("Gerente")]
       public ActionResult<string> autorizaGerente()
        {
       
            return "Autorizado!";
            
        }



    }
}
