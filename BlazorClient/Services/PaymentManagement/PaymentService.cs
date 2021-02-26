using System;
using System.BLL.Models.PaymentManagement;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorClient.Services.Helpers;
using Newtonsoft.Json;

namespace BlazorClient.Services.PaymentManagement
{
    public class PaymentService : Repository<int, PaymentModel, PaymentModel, PaymentModel>,
        IPaymentService
    {
        public PaymentService(HttpClient http) : base(http)
        {
        }

        public async Task<PaymentModel> UpdatePaymentToUserAsync(int userId, DateTime month, decimal sum)
        {
            var response = await _http.PutAsJsonAsync($"{_url}/UpdatePaymentToUser", new
            {
                userId,
                month,
                sum
            });

            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<PaymentModel>(responseText);
                return responseObj;
            }

            throw new HttpRequestException("Smth goes wrong in repo");
        }
    }
}