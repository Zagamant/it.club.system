using System;
using System.BLL.Models.UserManagement;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorClient.Services.UserManagement
{
    /// <summary>
    /// Represent a user service
    /// </summary>
    public class UserService : IUserService
    {
        private readonly HttpClient _http;
        private readonly string _url = "Users";

        public UserService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        /// <inheritdoc/>
        public async Task<UserModel> AuthenticateAsync(string username, string password)
        {
            var response = await _http.PostAsJsonAsync($"{_url}/authenticate", new {username, password});
            
            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                var responseObj = JsonSerializer.Deserialize<UserModel>(responseText);
                return responseObj;
            }

            return null;
        }

        public async Task LogoutAsync()
        {
            await _http.PostAsync($"{_url}/logout", null!);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            var items = await _http.GetFromJsonAsync<IEnumerable<UserModel>>($"{_url}");

            return items;
        }

        /// <inheritdoc/>
        public async Task<UserModel> GetAsync(int id)
        {
            var items = await _http.GetFromJsonAsync<UserModel>($"{_url}/{id}");

            return items;
        }

        /// <inheritdoc/>
        public async Task<UserModel> AddAsync(UserRegister user)
        {
            var response = await _http.PostAsJsonAsync($"{_url}", user);

            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                var responseObj = JsonSerializer.Deserialize<UserModel>(responseText);
                return responseObj;
            }

            return null;
        }

        /// <inheritdoc/>
        public async Task<UserModel> UpdateAsync(int id, UserModel model, string password = null)
        {
            var response = await _http.PutAsJsonAsync($"{_url}/{id}", model);

            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                var responseObj = JsonSerializer.Deserialize<UserModel>(responseText);
                return responseObj;
            }

            return null;
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"{_url}/{id}");
        }

        /// <inheritdoc/>
        public async Task ConfirmEmailAsync(ConfirmEmailModel model)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_url}/ConfirmEmail"),
                Content  = JsonContent.Create(model),
            };

            await _http.SendAsync(request);
        }

        /// <inheritdoc/>
        public async Task ForgotPasswordAsync(ForgotPasswordModel userModel)
        {
            await _http.PostAsJsonAsync($"{_url}/forgotpassword", userModel);
        }

        public async Task<bool> AddRole(int userId, int roleId)
        {
            var responce = await _http.PostAsJsonAsync($"{_url}/AddRole", new {userId, roleId});
            return JsonSerializer.Deserialize<bool>(await responce.Content.ReadAsStringAsync());
        }

        public async Task<bool> RemoveRole(int userId, int roleId)
        {
            var responce = await _http.PostAsJsonAsync($"{_url}/RemoveRole", new {userId, roleId});
            return JsonSerializer.Deserialize<bool>(await responce.Content.ReadAsStringAsync());
        }
        
    }
}