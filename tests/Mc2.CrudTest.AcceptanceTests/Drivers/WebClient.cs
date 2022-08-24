using Mc2.CrudTest.AcceptanceTests.Models.DTOs;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Mc2.CrudTest.AcceptanceTests.Drivers
{
    public class WebClient
    {
        protected readonly HttpClient _httpClient;

        public WebClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateCustomer(CustomerInputRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Customer", request);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<bool> UpdateCustomer(CustomerInputRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/Customer/{request.Id}", request);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            var path = string.Format("/api/Customer/{0}", id);
            var response = await _httpClient.DeleteAsync(path);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<CustomerOutputResponse> GetCustomerCustomerByEmail(string email)
        {
            return await _httpClient.GetFromJsonAsync<CustomerOutputResponse>($"/api/Customer/email/{email}");
        }

        public async Task<IEnumerable<CustomerOutputResponse>> GetAllCustomer()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<CustomerOutputResponse>>($"/api/Customer/All");
        }

        private static async Task<T> GetResponseFromContent<T>(HttpResponseMessage response) where T : class
        {

            var contentString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(contentString);
        }
    }
}