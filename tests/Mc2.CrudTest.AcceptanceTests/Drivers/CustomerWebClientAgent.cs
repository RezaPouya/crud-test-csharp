using Mc2.CrudTest.AcceptanceTests.Models;
using Mc2.CrudTest.AcceptanceTests.Models.DTOs;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Mc2.CrudTest.AcceptanceTests.Drivers
{
    public class CustomerWebClientAgent
    {
        protected readonly HttpClient _httpClient;

        public CustomerWebClientAgent(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResult<CustomerOutputResponse>> CreateCustomer(CustomerInputRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Customer", request);
            return await GetResponseFromContent<ApiResult<CustomerOutputResponse>>(response);
        }

        public async Task<ApiResult<CustomerOutputResponse>> UpdateCustomer(CustomerInputRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/Customer/{request.Id}", request);

            return await GetResponseFromContent<ApiResult<CustomerOutputResponse>>(response);
        }

        public async Task<ApiResult<CustomerOutputResponse>> DeleteCustomer(int id)
        {
            var path = string.Format("/api/Customer/{0}", id);

            var response = await _httpClient.DeleteAsync(path);

            return await GetResponseFromContent<ApiResult<CustomerOutputResponse>>(response);
        }

        public async Task<ApiResult<CustomerOutputResponse>> GetCustomerCustomerByEmail(string email)
        {
            return await _httpClient.GetFromJsonAsync<ApiResult<CustomerOutputResponse>>($"/api/Customer/email/{email}");
        }

        public async Task<ApiResult<IEnumerable<CustomerOutputResponse>>> GetAllCustomer()
        {
            return await _httpClient.GetFromJsonAsync<ApiResult<IEnumerable<CustomerOutputResponse>>>($"/api/Customer/All");
        }

        private static async Task<T> GetResponseFromContent<T>(HttpResponseMessage response) where T : class
        {

            var contentString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(contentString);
        }
    }
}