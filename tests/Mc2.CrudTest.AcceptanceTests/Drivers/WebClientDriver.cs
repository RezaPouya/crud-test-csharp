using System;
using System.Net.Http.Json;
using Mc2.CrudTest.AcceptanceTests.Models.DTOs.InputDtos;
using Mc2.CrudTest.AcceptanceTests.Models.DTOs.OutputDtos;
using Newtonsoft.Json;

namespace Mc2.CrudTest.AcceptanceTests.Drivers
{
    public class WebClientDriver
    {
        protected readonly HttpClient _httpClient;

        public WebClientDriver(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CustomerOutputResponse> CreateCustomer(CustomerInputRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Customer", request);

            if (response.IsSuccessStatusCode)
                return await GetResponseModelFromContent<CustomerOutputResponse>(response);

            return null;
        }

        public async Task<CustomerOutputResponse> UpdateCustomer(CustomerInputRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync("/api/Customer", request);

            if (response.IsSuccessStatusCode)
                return await GetResponseModelFromContent<CustomerOutputResponse>(response);

            return null;
        }

        public async Task<CustomerOutputResponse> DeleteCustomer(string email)
        {
            var response = await _httpClient.DeleteAsync($"/api/Customer/${email}");

            if (response.IsSuccessStatusCode)
                return await GetResponseModelFromContent<CustomerOutputResponse>(response);

            return null;
        }

        private static async Task<T> GetResponseModelFromContent<T>(HttpResponseMessage response) where T : class
        {
            var contentJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}