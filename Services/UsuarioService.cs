using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UsuariosApi.Data;
using UsuariosApi.Data.DTOs;
using UsuariosApi.helpers;
using UsuariosApi.Models;
using System.Web;
using UsuariosApi.Services.interfaces;

namespace UsuariosApi.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser<int>> _userManeger;
        private readonly SignInManager<IdentityUser<int>> _loginManeger;
        private readonly RoleManager<IdentityRole<int>> _roleManeger;
      

        public UsuarioService(Context context, IMapper mapper, UserManager<IdentityUser<int>> userManeger, SignInManager<IdentityUser<int>> signInManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _userManeger = userManeger;
            _loginManeger = signInManager;
            _roleManeger = roleManager;
        }

        public async Task<Result> CadastrarUsuario(CreateUserDTO userDTO)
        {
            var usuario = _mapper.Map<User>(userDTO);
            var identityUSER = _mapper.Map<IdentityUser<int>>(usuario);
            identityUSER.SecurityStamp = "dasdksivXMiXC";
            var resultIdentity = await _userManeger.CreateAsync(identityUSER, userDTO.Password);
           
            if (resultIdentity.Succeeded)
            {
                if (await _roleManeger.RoleExistsAsync(userDTO.Role))
                {
                    await _userManeger.AddToRoleAsync(identityUSER, userDTO.Role);

                }
                else
                {
                    var identityRole = new IdentityRole<int>();
                    identityRole.Name = userDTO.Role;
                    await _roleManeger.CreateAsync(identityRole);
                    await _userManeger.AddToRoleAsync(identityUSER, userDTO.Role);

                }

                return Result.Ok();
            }
            return Result.Fail("Falha ao cadastrar Usuario");

        } 

        public async Task<UserTokenDTO> LoginUsuario(LoginUserDTO loginUserDTO)
        {
           var resultLogin = await _loginManeger.PasswordSignInAsync(loginUserDTO.UserName,loginUserDTO.PassWord, false, false);
            if (resultLogin.Succeeded)
            {
               var userToken = new UserTokenDTO();
               var  identityuser  = await _userManeger.FindByNameAsync(loginUserDTO.UserName);
               var role = await _userManeger.GetRolesAsync(identityuser);
               var userReturnToken = new User();
               userReturnToken.Email = identityuser.Email;
               userReturnToken.UserName = identityuser.UserName;
               userToken.Usuario = userReturnToken;
               loginUserDTO.Role.AddRange(role);
               var token = GenerateToken(loginUserDTO);
               userToken.Token = token;
               return userToken;
            }
            return null;
        }

        public string GenerateToken(LoginUserDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(TokenKey.secretKey);


            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
               {
                   new Claim(ClaimTypes.Name, user.UserName.ToString()),

                   
                   new Claim(ClaimTypes.Role, user.Role[0].ToString())

               }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(TokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }
}
