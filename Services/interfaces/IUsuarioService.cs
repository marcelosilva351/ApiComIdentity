using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Data.DTOs;

namespace UsuariosApi.Services.interfaces
{
    public interface IUsuarioService
    {
        Task<Result> CadastrarUsuario(CreateUserDTO userDTO);
        Task<UserTokenDTO> LoginUsuario(LoginUserDTO loginUserDTO);
        string GenerateToken(LoginUserDTO user);




    }
}
