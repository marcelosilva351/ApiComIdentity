using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Data.DTOs;
using UsuariosApi.Services;
using UsuariosApi.Services.interfaces;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("api/cadastros")]
    public class UserController : ControllerBase
    {
        private readonly IUsuarioService _userService;
        public UserController(IUsuarioService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> CadastrarUsuario([FromBody] CreateUserDTO User)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            Result result = await _userService.CadastrarUsuario(User);
            if (result.IsFailed)
            {
                return StatusCode(500, result);
            }
            return Ok(User);
        }

       
        
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<UserTokenDTO>> loginUser([FromBody] LoginUserDTO userLoginInfo)
        {
            try
            {
                var userToken = await _userService.LoginUsuario(userLoginInfo);
                if(userToken == null)
                {
                    return NotFound("Usuario ou senha invalidos");

                }
                return Ok(userToken);



            }
            catch (Exception)
            {

                return StatusCode(500, "Servidor falhou");
            }
            
        }




    }
}
