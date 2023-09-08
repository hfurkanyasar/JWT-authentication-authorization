using APIServiceWithMVC.myAPI.DAL.Interfaces;
using APIServiceWithMVC.myAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APIServiceWithMVC.myAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthDal _authDAL;
        private readonly IConfiguration _conf;
        public AuthController(IAuthDal authDAL, IConfiguration conf)
        {
            _authDAL = authDAL;
            _conf = conf;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            if (await _authDAL.UserExists(dto.UserName))
            {
                ModelState.AddModelError("Not Valid", "Zaten bu bilgiler ile kullanıcı mevcut");
            }

            if (ModelState.IsValid)
            {
                return BadRequest();
            }
            var kisi = await _authDAL.Register(new DAL.Entities.User()
            { UserName = dto.UserName }, dto.Password);

            return StatusCode(201);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] RegisterDTO dto)
        {
            var user = await _authDAL.Login(dto.UserName, dto.Password);
            if (user == null)
            {
                return BadRequest();
            }
            else
            {
                var desc = new SecurityTokenDescriptor()
                {
                    Expires = DateTime.Now.AddDays(1),
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier,user.UserID.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName)

                    }),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_conf.GetSection("AppSettings:Token").Value)), SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(desc);
                var feedbackToken = tokenHandler.WriteToken(token);
                return Ok(feedbackToken);
            }


        }
    }
}
