using ABCRetailers.Models;
using ABCRetailers.Models.ViewModels;
using System.Text.Json;

namespace ABCRetailers.Services
{
    public class FunctionsApiClient : IFunctionsApi
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _jsonOptions;

        public FunctionsApiClient(HttpClient http)
        {
            _http = http;
            _jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        }

        // 🧑 Customers
        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _http.GetFromJsonAsync<List<Customer>>("customers", _jsonOptions);
        }

        public async Task<Customer?> GetCustomerAsync(string id)
        {
            return await _http.GetFromJsonAsync<Customer>($"customers/{id}", _jsonOptions);
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            await _http.PostAsJsonAsync("customers", customer, _jsonOptions);
        }

        public async Task UpdateCustomerAsync(string id, Customer customer)
        {
            await _http.PutAsJsonAsync($"customers/{id}", customer, _jsonOptions);
        }

        public async Task DeleteCustomerAsync(string id)
        {
            await _http.DeleteAsync($"customers/{id}");
        }

        // 📦 Products
        public async Task<List<Product>> GetProductsAsync()
        {
            return await _http.GetFromJsonAsync<List<Product>>("products", _jsonOptions);
        }

        public async Task<Product?> GetProductAsync(string id)
        {
            return await _http.GetFromJsonAsync<Product>($"products/{id}", _jsonOptions);
        }

        public async Task CreateProductAsync(Product product)
        {
            await _http.PostAsJsonAsync("products", product, _jsonOptions);
        }

        public async Task UpdateProductAsync(string id, Product product)
        {
            await _http.PutAsJsonAsync($"products/{id}", product, _jsonOptions);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _http.DeleteAsync($"products/{id}");
        }

        // 🧾 Orders
        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _http.GetFromJsonAsync<List<Order>>("orders", _jsonOptions);
        }

        public async Task<Order?> GetOrderAsync(string id)
        {
            return await _http.GetFromJsonAsync<Order>($"orders/{id}", _jsonOptions);
        }

        public async Task CreateOrderAsync(OrderCreateViewModel order)
        {
            await _http.PostAsJsonAsync("orders", order, _jsonOptions);
        }

        public async Task UpdateOrderStatusAsync(string id, string status)
        {
            var payload = new { status };
            await _http.PutAsJsonAsync($"orders/{id}/status", payload, _jsonOptions);
        }

        public async Task DeleteOrderAsync(string id)
        {
            await _http.DeleteAsync($"orders/{id}");
        }

        // 📁 Contracts
        public async Task UploadContractAsync(FileUploadModel fileModel)
        {
            using var content = new MultipartFormDataContent();
            content.Add(new StreamContent(fileModel.File.OpenReadStream()), "file", fileModel.File.FileName);
            await _http.PostAsync("contracts/upload", content);
        }

        public async Task<List<string>> GetContractsAsync()
        {
            return await _http.GetFromJsonAsync<List<string>>("contracts", _jsonOptions);
        }
    }
}