using ABCRetailers.Models;
using ABCRetailers.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABCRetailers.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IAzureStorageService _storageService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(IAzureStorageService storageService, ILogger<CustomerController> logger)
        {
            _storageService = storageService;
            _logger = logger;
        }

        public async Task<IActionResult> Index_Customer() 
            {
            var customer = await _storageService.GetAllEntitiesAsync<Customer>();
            return View(customer);
            }

        public IActionResult NewCustomer() => View();

        [HttpPost]
        public async Task<IActionResult> NewCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    await _storageService.AddEntityAsync(customer);
                    TempData["Success"] = "Customer created successfully!";
                    return RedirectToAction(nameof(Index_Customer));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"Failed to create customer: {ex.Message}";

                }

            }
          
            return RedirectToAction(nameof(Index_Customer));
        }

        public async Task<IActionResult> EditCustomer(string id)
        {
            var customer = await _storageService.GetAllEntitiesAsync<Customer>();
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> EditCustomer(Customer customer)
        {
            await _storageService.UpdateEntityAsync(customer);
            return RedirectToAction(nameof(Index_Customer));
        }

        public async Task<IActionResult> DeleteCustomer(string id)
        {
            await _storageService.DeleteEntityAsync< Customer>("Customer", id);
            return RedirectToAction(nameof(Index_Customer));
        }
    }
}