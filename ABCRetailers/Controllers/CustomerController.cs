using ABCRetailers.Models;
using ABCRetailers.Models.ViewModels;
using ABCRetailers.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace ABCRetailers.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IAzureStorageService _storage;
        public CustomerController(IAzureStorageService storage)
        {
            _storage = storage;
        }

        public async Task<IActionResult> Index_Customer() 
            {
            var customer = await _storage.GetAllEntitiesAsync<Customer>();
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
                    await _storage.AddEntityAsync(customer);
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
            var customer = await _storage.GetAllEntitiesAsync<Customer>();
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> EditCustomer(Customer customer)
        {
            await _storage.UpdateEntityAsync(customer);
            return RedirectToAction(nameof(Index_Customer));
        }

        public async Task<IActionResult> DeleteCustomer(string id)
        {
            await _storage.DeleteEntityAsync< Customer>("Customer", id);
            return RedirectToAction(nameof(Index_Customer));
        }
    }
}