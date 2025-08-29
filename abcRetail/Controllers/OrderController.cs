using abcRetail.Models;
using abcRetail.Services;
using Microsoft.AspNetCore.Mvc;

namespace abcRetail.Controllers
{
    public class OrderController : Controller
    {
        private readonly IAzureStorageService _storageService;
        public OrderController(IAzureStorageService storageService) => _storageService = storageService;

        public async Task<IActionResult> Index_Order()
        {
            var orders = await _storageService.GetAllEntitiesAsync<Order>();
            return View(orders);
        }

        public async Task<IActionResult> NewOrder()
        {
            ViewBag.Customers = await _storageService.GetAllEntitiesAsync<Customer>();
            ViewBag.Products = await _storageService.GetAllEntitiesAsync<Product>();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> NewOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Customers = await _storageService.GetAllEntitiesAsync<Customer>();
                ViewBag.Products = await _storageService.GetAllEntitiesAsync<Product>();
                return View(order);
            }

            order.OrderID = Guid.NewGuid().ToString();
            order.PartitionKey = "Order";
            order.Status = "Pending";

            await _storageService.AddEntityAsync(order);
            await _storageService.SendQueueMessageAsync("orders-queue", $"New order placed: {order.RowKey}");

            TempData["Success"] = "Order created successfully!";
            return RedirectToAction(nameof(Index_Order));
        }

        public async Task<IActionResult> UpdateOrder(string id)
        {
            var order = await _storageService.GetEntityAsync<Order>("Order", id);
            return order == null ? NotFound() : View(order);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateOrder(Order order)
        {
            await _storageService.UpdateEntityAsync(order);
            TempData["Success"] = "Order updated successfully!";
            return RedirectToAction(nameof(Index_Order));
        }

        public async Task<IActionResult> CancelOrder(string id)
        {
            await _storageService.DeleteEntityAsync<Order>("Order", id);
            TempData["Success"] = "Order deleted.";
            return RedirectToAction(nameof(Index_Order));
        }
    }
}