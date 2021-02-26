using System;
using System.BLL.Models.RoleManagement;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlazorClient.Services.RoleManagement
{
    public class RoleService : IRoleService
    {
        private readonly HttpClient _http;
        private readonly string _url = "Roles";
        public RoleService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            
        }


         public virtual async Task<IEnumerable<RoleModel>> GetAllAsync()
        {
            var items = await _http.GetFromJsonAsync<List<RoleModel>>($"{_url}");

            return items;
        }

        public virtual async Task<RoleModel> GetAsync(int id)
        {
            var item = await _http.GetFromJsonAsync<RoleModel>(_url + $"/{id}");

            return item;
        }

        public virtual async Task<RoleModel> AddAsync(RoleModel entity)
        {
            var response = await _http.PostAsJsonAsync(_url, entity);
            
            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<RoleModel>(responseText);
                return responseObj;
            }

            throw new HttpRequestException("Smth goes wrong in repo");
        }

        public virtual async Task<RoleModel> UpdateAsync(int id, RoleModel updatedGroup)
        {
            var response = await _http.PutAsJsonAsync($"{_url}/{id}", updatedGroup);
            
            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<RoleModel>(responseText);
                return responseObj;
            }

            throw new HttpRequestException("Smth goes wrong in repo");
        }

        public virtual async Task DeleteAsync(int id)
        {
            await _http.DeleteAsync($"{_url}/{id}");
        }

    }
}