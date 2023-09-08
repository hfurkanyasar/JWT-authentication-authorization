using APIServiceWithMVC.UI.ApiServices;
using APIServiceWithMVC.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServiceWithMVC.UI.Controllers
{
    public class UserLoginController : Controller
    {
       
        
        private readonly TokenApiServices _tokenApi;
        public UserLoginController(TokenApiServices tokenApi)
        {
            _tokenApi = tokenApi;
        }
        public IActionResult Login()
        {
            if (Request.Cookies["Giris"] != null)
            {
                string kaydedilmisCookie = Request.Cookies["token"];
            }
            return View(new LoginDTO());
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            // tokelal metodunu çalıştır.
            string data = await _tokenApi.TokenAl(dto.UserName, dto.Password);
            //cookie de tut.
            CookieOptions cookie = new CookieOptions();
            cookie.Domain = "Giris";
            cookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("token", data);

            return View();
        }
    }
}
