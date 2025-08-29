using abcRetail.Models;
using abcRetail.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace abcRetail.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IAzureStorageService _storage;
        public CustomerController(IAzureStorageService storage) => _storage = storage;

        public async Task<IActionResult> Index_Customer() =>
            View(await _storage.GetAllEntitiesAsync<Customer>("Customers"));

        public IActionResult NewCustomer() => View();

        [HttpPost]
        public async Task<IActionResult> NewCustomer(Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);
            customer.RowKey = Guid.NewGuid().ToString();
            await _storage.AddOrUpdateEntityAsync(customer, "Customers");
            return RedirectToAction(nameof(Index_Customer));
        }

        public async Task<IActionResult> EditCustomer(string id)
        {
            var customer = await _storage.GetEntityAsync<Customer>("Customer", id, "Customers");
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> EditCustomer(Customer customer)
        {
            await _storage.AddOrUpdateEntityAsync(customer, "Customers");
            return RedirectToAction(nameof(Index_Customer));
        }

        public async Task<IActionResult> DeleteCustomer(string id)
        {
            await _storage.DeleteEntityAsync("Customer", id, "Customers");
            return RedirectToAction(nameof(Index_Customer));
        }
    }
}