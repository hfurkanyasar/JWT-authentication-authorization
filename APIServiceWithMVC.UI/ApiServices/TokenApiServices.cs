using APIServiceWithMVC.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace APIServiceWithMVC.UI.ApiServices
{
    public class TokenApiServices
    {
        private readonly HttpClient _client;
        public TokenApiServices(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> TokenAl(string kullaniciAdi, string sifre)
        {
            LoginDTO dto = new LoginDTO()
            {
                UserName = kullaniciAdi,
                Password = sifre

            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(dto));
            content.Headers.ContentType=new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var apiData = await _client.PostAsync("api/Auth/Login", content);
            string token = "";
            if (apiData.IsSuccessStatusCode)
            {
                token = await apiData.Content.ReadAsStringAsync();
            }
            return token;
        }

        public async Task<string> UserSave(RegisterDTO dto)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(dto));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var apiData = await _client.PostAsync("api/Auth/Register", content);

           

            if (apiData.IsSuccessStatusCode)
            {
                return "Giris Yapıldı";

            }
            return "";
        
        }


    }
}
