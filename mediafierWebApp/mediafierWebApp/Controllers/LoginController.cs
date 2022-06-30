using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mediafierWebApp.Models;
using mediafierWebApp.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace mediafierWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public static int uid;
        private IConfiguration _config;
        private readonly MediafierContext _mediafiercontext;

        public LoginController(IConfiguration config, MediafierContext a)
        {
            _config = config;
            _mediafiercontext = a;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();
            var user = Authenticate(login);

            if (user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString,id=uid });
                return response;
            }else
            return Unauthorized();
        }
        private string BuildToken(UserRequest user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private UserRequest Authenticate(LoginModel login)
        {
            UserRequest user = null;
            var ans = _mediafiercontext.Users.FirstOrDefault(o => o.UsersUsername == login.Username);

            try
            {
                if (ans.UsersUsername != null && ans.UsersPassword == login.Password)
                {
                    user = new UserRequest { UsersUsername = ans.UsersUsername, UsersPassword = ans.UsersPassword };
                    uid = ans.UsersId;
                }
                return user;
            }catch(Exception e)
            {
                return null;
            }
        }
        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

    }
}
