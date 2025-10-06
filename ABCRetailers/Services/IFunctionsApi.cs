using ABCRetailers.Models;
using ABCRetailers.Models.ViewModels;

namespace ABCRetailers.Services
{
    public interface IFunctionsApi
    {
        // 👤 Customers
        Task<List<Customer>> GetCustomersAsync();
        Task<Customer?> GetCustomerAsync(string id);
        Task CreateCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(string id, Customer customer);
        Task DeleteCustomerAsync(string id);

        // 📦 Products
        Task<List<Product>> GetProductsAsync();
        Task<Product?> GetProductAsync(string id);
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(string id, Product product);
        Task DeleteProductAsync(string id);

        // 🧾 Orders
        Task<List<Order>> GetOrdersAsync();
        Task<Order?> GetOrderAsync(string id);
        Task CreateOrderAsync(OrderCreateViewModel order);
        Task UpdateOrderStatusAsync(string id, string status);
        Task DeleteOrderAsync(string id);

        // 📁 Contracts
        Task UploadContractAsync(FileUploadModel fileModel);
        Task<List<string>> GetContractsAsync();
    }
}