using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.helpers;
using UsuariosApi.Models;

namespace UsuariosApi.Data.DTOs
{
    public class UserTokenDTO
    {
        public User Usuario { get; set; }
        public string Token { get; set; }
    }
}
