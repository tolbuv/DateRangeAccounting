using DateRangeAccounting.API.Models.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace ConsoleTestApp.Services
{
    public class UserService
    {
        private readonly string _appPath;
        private readonly HttpClientService _service;

        public UserService(string appPath, HttpClientService service)
        {
            _appPath = appPath;
            _service = service;
        }

        public Dictionary<string, string> GetTokenDictionary(string userName, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>( "grant_type", "password" ),
                new KeyValuePair<string, string>( "username", userName ),
                new KeyValuePair<string, string> ( "Password", password )
            };
            var content = new FormUrlEncodedContent(pairs);

            using (var client = new HttpClient())
            {
                var response =
                    client.PostAsync(_appPath + "/Token", content).Result;
                var result = response.Content.ReadAsStringAsync().Result;

                var tokenDictionary =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

                return tokenDictionary;
            }
        }

        public string GetToken(string userName, string password)
            => GetTokenDictionary(userName, password)["access_token"];

        public string Register(string email, string password)
        {
            var registerModel = new RegisterBindingModel
            {
                Email = email,
                Password = password,
                ConfirmPassword = password
            };
            using (var client = new HttpClient())
            {
                var response = client.PostAsJsonAsync(_appPath + "/api/Account/Register", registerModel).Result;
                return response.StatusCode.ToString();
            }
        }

        public UserInfoViewModel GetUserInfo(string token)
        {
            using (var client = _service.CreateClient(token))
            {
                var response = client.GetAsync(_appPath + "/api/Account/UserInfo").Result;
                var json = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<UserInfoViewModel>(json);
            }
        }
    }
}
