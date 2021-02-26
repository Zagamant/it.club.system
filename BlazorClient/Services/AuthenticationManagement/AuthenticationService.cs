using System.BLL.Models.UserManagement;
using System.DAL.Entities;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorClient.Services.LocalStorageManagement;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace BlazorClient.Services.AuthenticationManagement
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorageService;
        private readonly string _url = "users";

        public UserModel User { get; private set; }

        public AuthenticationService(
            HttpClient http,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService
        )
        {
            _http = http;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public async Task Initialize()
        {
            User = await _localStorageService.GetItem<UserModel>("user");
        }

        public async Task Login(string username, string password)
        {

            var response = await _http.PostAsJsonAsync($"{_url}/authenticate", new {username, password});

            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<UserModel>(responseText);
                User = responseObj;
            }

            await _localStorageService.SetItem("user", User);
        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("login");
        }

    }
}