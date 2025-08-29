using abcRetail.Models;
using Microsoft.AspNetCore.Mvc;

namespace abcRetail.Controllers
{
    public class ProductController : Controller
    {
        private readonly IAzureStorageService _storageService;
        public ProductController(IAzureStorageService storageService) => _storageService = storageService;

        public async Task<IActionResult> Index_Product()
        {
            var products = await _storageService.GetAllEntitiesAsync<Product>();
            return View(products);
        }

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> NewProduct(Product product, IFormFile imageFile)
        {
            if (!ModelState.IsValid) return View(product);

            product.RowKey = Guid.NewGuid().ToString();
            product.PartitionKey = "Product";

            if (imageFile != null && imageFile.Length > 0)
            {
                product.ImageUrl = await _storageService.UploadBlobAsync(
                    "product-images", imageFile.FileName, imageFile.OpenReadStream());
            }

            await _storageService.AddEntityAsync(product);
            TempData["Success"] = "Product created successfully!";
            return RedirectToAction(nameof(Index_Product));
        }

        public async Task<IActionResult> UpdateProduct(string id)
        {
            var product = await _storageService.GetEntityAsync<Product>("Product", id);
            return product == null ? NotFound() : View(product);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct(Product product, IFormFile imageFile)
        {
            if (!ModelState.IsValid) return View(product);

            if (imageFile != null && imageFile.Length > 0)
            {
                product.ImageUrl = await _storageService.UploadBlobAsync(
                    "product-images", imageFile.FileName, imageFile.OpenReadStream());
            }

            await _storageService.UpdateEntityAsync(product);
            TempData["Success"] = "Product updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _storageService.DeleteEntityAsync<Product>("Product", id);
            TempData["Success"] = "Product deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}