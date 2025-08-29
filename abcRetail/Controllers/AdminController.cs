using abcRetail.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace abcRetail.Controllers
{
    public class AdminController : Controller
    {
        private readonly DBContext _context;

        public AdminController(DBContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index_Admin()
        {
            return View(await _context.Administrators.ToListAsync());
        }


       
        private bool AdministratorExists(int id)
        {
            return _context.Administrators.Any(e => e.AdminID == id);
        }
    }
}
