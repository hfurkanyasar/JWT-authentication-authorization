using APIServiceWithMVC.UI.ApiServices;
using APIServiceWithMVC.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServiceWithMVC.UI.Controllers
{
    public class UserRegisterController : Controller
    {
        private readonly TokenApiServices _tokenApi;
        public UserRegisterController(TokenApiServices tokenApi)
        {
            _tokenApi = tokenApi;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var deger = await _tokenApi.UserSave(dto);
            return View();
        }
    }
}
