using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UsuariosApi.Data.DTOs
{
    public class LoginUserDTO
    {
        [Required]
        public String UserName { get; set; }
        [Required]
        public string PassWord { get; set; }
        public List<string> Role { get; set; } = new List<string>();
    }
}
