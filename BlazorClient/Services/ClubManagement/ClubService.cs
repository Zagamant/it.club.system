﻿using System.BLL.Models.ClubManagement;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorClient.Helpers;
using BlazorClient.Services.Helpers;
using Newtonsoft.Json;

namespace BlazorClient.Services.ClubManagement
{
    public class ClubService : ServiceBase<int, ClubModel, ClubModel, ClubModel>, IClubService
    {
        public ClubService(HttpClient http) : base(http)
        {
        }

        public async Task<ClubModel> AddRoomAsync(int clubId, int roomId)
        {
            var response = await _http.PutAsJsonAsync($"{_url}/AddRoom", new
            {
                clubId,
                roomId
            });

            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<ClubModel>(responseText);
                return responseObj;
            }

            throw new HttpRequestException("Smth goes wrong in repo");
        }

        public async Task<ClubModel> RemoveRoomAsync(int clubId, int roomId)
        {
            var response = await _http.PutAsJsonAsync($"{_url}/RemoveRoom", new
            {
                clubId,
                roomId
            });

            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<ClubModel>(responseText);
                return responseObj;
            }

            throw new HttpRequestException("Smth goes wrong in repo");
        }

        public async Task<IEnumerable<ClubModel>> GetClubsByUser(int userId)
        {
            var items = await _http.GetFromJsonAsync<IEnumerable<ClubModel>>(_url + $"/{userId}/clubs", MyOptions.JsonSerializerWebOptions);

            return items;
        }
    }
}