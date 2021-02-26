using System;
using System.BLL.Helpers;
using System.BLL.Models.Helpers;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlazorClient.Services.Helpers
{
    public abstract class Repository<TId,  TAddModel, TUpdateModel, TModel> : IRepository<TId, TAddModel, TUpdateModel, TModel>
        where TAddModel : BaseModel
        where TUpdateModel : BaseModel
        where TModel : BaseModel
    {
        protected readonly HttpClient _http;
        protected readonly string _url;
            
        public Repository(HttpClient http/*, string url*/)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _url = this.GetType().Name.Replace("Service", "s");
            //_url = url ?? throw new ArgumentNullException(nameof(http));     
        }

        public virtual async Task<IEnumerable<TModel>> GetAllAsync(string sort = "",string page = "",string pageSize = "", string filter = "")
        {
            var items = await _http.GetFromJsonAsync<List<TModel>>($"{_url}?" +
                                                                   $"{(string.IsNullOrEmpty(sort) ? "" : $"sort={sort}")}" +
                                                                   $"{(string.IsNullOrEmpty(page) ? "" : $"page={page}")}" +
                                                                   $"{(string.IsNullOrEmpty(pageSize) ? "" : $"pageSize={pageSize}")}" +
                                                                   $"{(string.IsNullOrEmpty(filter) ? "" : $"filter={filter}")}");

            return items;
        }

        public virtual async Task<TModel> GetAsync(TId id)
        {
            var item = await _http.GetFromJsonAsync<TModel>(_url + $"/{id}");

            return item;
        }

        public virtual async Task<TModel> AddAsync(TAddModel entity)
        {
            var response = await _http.PostAsJsonAsync(_url, entity);
            
            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<TModel>(responseText);
                return responseObj;
            }

            throw new HttpRequestException("Smth goes wrong in repo");
        }

        public virtual async Task<TModel> UpdateAsync(TId id, TUpdateModel updatedGroup)
        {
            var response = await _http.PutAsJsonAsync($"{_url}/{id}", updatedGroup);
            
            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<TModel>(responseText);
                return responseObj;
            }

            throw new HttpRequestException("Smth goes wrong in repo");
        }

        public virtual async Task DeleteAsync(TId id, bool isDelete = false)
        {
            var httpResponseMessage = await _http.DeleteAsync($"{_url}/{id}");
        }
    }
}