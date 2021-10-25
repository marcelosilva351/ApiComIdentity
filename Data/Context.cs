using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsuariosApi.Data
{
    public class Context : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public Context(DbContextOptions options) : base(options)
        {
        }
    }
}
