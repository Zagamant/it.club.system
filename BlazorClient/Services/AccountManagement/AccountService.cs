using System;
using System.BLL.Models.UserManagement;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorClient.Helpers;
using BlazorClient.Models;
using BlazorClient.Models.Account;
using BlazorClient.Services.LocalStorageManagement;
using Microsoft.AspNetCore.Components;

namespace BlazorClient.Services.AccountManagement
{
    public class AccountService : IAccountService
    {
        private HttpClient _http;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _userKey = "user";

        public User User { get; private set; }

        public AccountService(
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
            User = await _localStorageService.GetItem<User>(_userKey);
        }

        public async Task<UserLoginData> Login(Login model)
        {
            var response = await _http.PostAsJsonAsync($"users/authenticate", model);

            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                var responseObj = JsonSerializer.Deserialize<UserLoginData>(responseText, MyOptions.JsonSerializerWebOptions);
                await _localStorageService.SetItem(_userKey, responseObj);
                return responseObj;
            }

            return null;
        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItem(_userKey);
            await _http.PostAsync($"users/logout", null!);
            _navigationManager.NavigateTo("account/login");
        }

        public async Task<UserModel> Register(AddUser model)
        {
            var response = await _http.PostAsJsonAsync($"users", model);

            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                var responseObj = JsonSerializer.Deserialize<UserModel>(responseText, MyOptions.JsonSerializerWebOptions);
                return responseObj;
            }

            return null;
        }

        public async Task<IEnumerable<UserModel>> GetAll()
        {
            var items = await _http.GetFromJsonAsync<IEnumerable<UserModel>>($"users");

            return items;
        }

        public async Task<UserModel> GetById(int id)
        {
            var items = await _http.GetFromJsonAsync<UserModel>($"users/{id}");

            return items;
        }

        public async Task<UserModel> Update(int id, UserModel model, string password = null)
        {
            var response = await _http.PutAsJsonAsync($"users/{id}", new {model, password});

            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                var responseObj = JsonSerializer.Deserialize<UserModel>(responseText, MyOptions.JsonSerializerWebOptions);
                return responseObj;
            }

            return null;
        }

        public async Task Delete(int id)
        {
            await _http.DeleteAsync($"users/{id}");

            // auto logout if the logged in user deleted their own record
            if (id == User.Id)
                await Logout();
        }
    }
}