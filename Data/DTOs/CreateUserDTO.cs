using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UsuariosApi.Data.DTOs
{
    public class CreateUserDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage ="Senhas não são iguais")]
        public string RePassword { get; set; }
    }
}
