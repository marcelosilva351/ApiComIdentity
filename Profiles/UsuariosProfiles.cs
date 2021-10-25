using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Data.DTOs;
using UsuariosApi.Models;

namespace UsuariosApi.Profiles
{
    public class UsuariosProfiles : Profile
    {
        public UsuariosProfiles()
        {
            CreateMap<CreateUserDTO, User>();
            CreateMap<User, IdentityUser<int>>();
            CreateMap<LoginUserDTO, IdentityUser<int>>();
        }
    }
}
