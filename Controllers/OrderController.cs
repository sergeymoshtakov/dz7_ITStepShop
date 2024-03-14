using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ITStepShop.Models;
using ITStepShop.Data;
using Microsoft.EntityFrameworkCore;

namespace ITStepShop.Controllers
{
    [Authorize(Roles = $"{WC.ManagerRole}, {WC.AdminRole}")]
    public class OrderController : Controller
    {
        private readonly UserManager<ShopUser> _userManager;
        private readonly ApplicationDbContext _context;

        public OrderController(UserManager<ShopUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            var orders = _context.Order.ToList();
            return View(orders);
        }

        // GET: Order/Create
        public IActionResult Create(int productId)
        {
            var product = _context.Product.Find(productId);
            var user = _userManager.GetUserAsync(User).Result;

            // Перевірка наявності товару та користувача
            if (product == null || user == null)
            {
                return NotFound();
            }

            var order = new Order
            {
                CustomerId = user.Id, 
                ProductId = product.Id, 
                Quantity = 1,
                OrderDate = DateTime.Now
            };

            return View(order);
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerName,ProductName,Quantity,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}