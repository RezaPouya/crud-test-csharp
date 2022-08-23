using Mc2.CrudTest.AcceptanceTests.Models.DTOs.InputDtos;
using Mc2.CrudTest.AcceptanceTests.Models.DTOs.OutputDtos;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Mc2.CrudTest.AcceptanceTests.Drivers
{
    public class WebClientDriver
    {
        protected readonly HttpClient _httpClient;

        public WebClientDriver(HttpClient httpClient)
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

        public async Task<bool> DeleteCustomer(string email)
        {
            var response = await _httpClient.DeleteAsync($"/api/Customer/${email}");

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<CustomerOutputResponse> GetCustomerCustomerByEmail(string email)
        {
            return await _httpClient.GetFromJsonAsync<CustomerOutputResponse>($"/api/Customer/email/${email}");
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